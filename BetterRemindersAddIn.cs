using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;


// Copyright (c) 2016-2017 Ben Spiller. 

/*
 * Futures:
 * NotifyIcon with balloons
 * Executing a command line or playing a sound on first reminder
 * Subject regex for includes/excludes
 * Configure different default reminder times based on location, organiser
 * Use a user-defined property in an appointment (and maybe Outlook's reminder) to override the default reminder time
 * Allow remembering reminder time for recurring appointments in settings
 * Include meetings already in-progress when Outlook is started (not sure how useful this is) 
 * 
 */
namespace BetterReminders
{

	/// <summary>
	/// A plug-in that uses a timer to regularly check for meetings starting in the next few minutes, and 
	/// for each one displays a form from which user can open the item or snooze. 
	/// </summary>
	/// <remarks>
	/// Theading: everything happens on the UI thread (there isn't enough work to justify a background thread), 
	/// therefore no locking is required for our data structures, Outlook's can be accessed without locking overheads. 
	/// </remarks>
	public partial class BetterRemindersAddIn
	{
		#region fields
		private static Logger logger = Logger.GetLogger();

		private DateTime nextPlannedWakeup;
		private System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

		private System.Media.SoundPlayer soundPlayer;

		/// <summary>
		/// A list of upcoming meetings we know about already, and the time of the next reminder for each one 
		/// (or null if dismissed). Items are expired from this list 
		/// </summary>
		private Dictionary<string, UpcomingMeeting> upcoming = new Dictionary<string, UpcomingMeeting>();

		#endregion

		#region constants

		/// <summary>
		/// The period of time this plug-in will sleep between checking if there are any upcoming meetings. 
		/// Don't do it so often that responsiveness is reduced, but do it often enough we have a good chance of 
		/// picking up late new/changed invites
		/// </summary>
		private TimeSpan SleepInterval { get { return new TimeSpan(0, 0, Properties.Settings.Default.searchFrequencySecs); } }

		private TimeSpan DefaultReminderTime { get { return new TimeSpan(0, 0, Properties.Settings.Default.defaultReminderSecs); } }


		/// <summary>
		/// Wait a bit before doing anything, to give outlook a chance to finish starting and improve responsiveness
		/// </summary>
		private const int StartupDelaySecs = 20;

		#endregion


		private void updateUpcomingMeetings()
		{
			DateTime now = DateTime.Now;
			// first update existing items in case there were any changes
			foreach (UpcomingMeeting m in upcoming.Values)
			{
				// if start date was changed in either direction we should recalculate 
				// the reminder - might need a notification sooner,
				// or if the meeting was postponed then later. 
				// always resetting to defaultremindertime is the safest/most conservative 
				// option since it gives maximum opportunity for the user to decide 
				// whether to defer the meeting
				if (m.UpdateStartTime())
				{
					logger.Info("Resetting reminder time for " + m + " due to change in start time");
					m.NextReminderTime = m.StartTime - DefaultReminderTime;
				}
			}

			// add in any new meetings we're not aware of yet that will start or need to be reminded about in the 
			// next sleep interval, ignoring any that have ended already
			// this filter is conservative - must not miss any items, but is ok if it contains some extra ones
			string filter = "[Start] >= '" + (now).ToString("g") + "'"
				+ " AND [Start] <= '" + (now + SleepInterval + DefaultReminderTime).ToString("g") + "'"
				+ " AND [End] >= '" + (now).ToString("g") + "'"
				// not really necessary but
				+ " AND [End] <= '" + (now + new TimeSpan(1, 0, 0, 0)).ToString("g") + "'"
				;
			/*
			filter = "[Start] >= '" + new DateTime(2016, 1, 17).ToString("g") + "'"
							+ " AND [Start] <= '" + new DateTime(2016, 1, 19).ToString("g") + "'"
							+ " AND [End] >= '" + new DateTime(2016, 1, 17).ToString("g") + "'"
							+ " AND [End] <= '" + new DateTime(2016, 1, 19).ToString("g") + "'"
							;
			 * */
			Outlook.Folder calFolder = Application.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderCalendar) as Outlook.Folder;

			Outlook.Items calItems = calFolder.Items;
			// weirdly, sorting is *required* otherwise the start dates for recurring items are all wrong
			calItems.Sort("[Start]", false);
			calItems.IncludeRecurrences = true;
			calItems = calItems.Restrict(filter);
			logger.Debug("Searching calendar using filter: " + filter + " -> got " + calItems.Count + " items");

			foreach (Outlook.AppointmentItem item in calItems)
			{
				if (item.AllDayEvent) continue;
				//logger.Debug("Returned: " + new UpcomingMeeting(item, item.Start - DefaultReminderTime)); // TODO; remove
				// nb: the second check should be unecessary, but prevents bad behaviour if outlook's filtering doesn't work right for some reason
				if (!upcoming.ContainsKey(item.EntryID) && item.End >= now)
					upcoming.Add(item.EntryID, new UpcomingMeeting(item, item.Start - DefaultReminderTime));
			}

			// finally, expire any meetings that have ended (we can't expire items until they definitely 
			// won't show up in the above filter otherwise we might end up re-adding dismissed reminders)
			// probably safe to expire them even if not dismissed, if they've ended
			var expired = new List<string>(upcoming.Values.Where(e => (e.EndTime < now) || e.IsDeleted).Select(e => e.ID));
			foreach (var id in expired)
			{
				logger.Debug("Removing expired item: " + upcoming[id]);
				upcoming[id].Dispose();
				upcoming.Remove(id);
			}
			logger.Debug(upcoming.Count + " upcoming items: " + string.Join("\n   ", upcoming.Values));
		}

		/// <summary>
		/// Called when the timer wakes up or when a reminder form has been closed to work out 
		/// what to do next
		/// </summary>
		private void waitOrRemind()
		{
			try
			{
				DateTime now = DateTime.Now;
				updateUpcomingMeetings();


				UpcomingMeeting next = null;
				if (upcoming.Count > 0)
				{
					next = upcoming.Values.OrderBy(m => m.NextReminderTime).First();
					if (next.IsDismissed) // implies it contains only expired items
						next = null;
				}

				DateTime sleepUntil = now + SleepInterval;

				if (next != null)
				{
					// either we wokeup just in time or maybe we have a backlog of reminder(s) to clear
					// (nb: use 500ms fudge factor to make sure potentially imprecise timers don't hurt us)
					if (next.NextReminderTime <= now + new TimeSpan(0, 0, 0, 0, 500))
					{
						logger.Info("Showing reminder for: " + next);
						playReminderSound();
						ReminderForm form = new ReminderForm(next);
						form.FormClosed += ReminderFormClosedEventHandler;
						form.Show();
						// timers etc are disabled until the current window has been closed - 
						// simpler to manage and avoids getting a million popups if you leave it running while on vacation
						return;
					}

					if (next.NextReminderTime < sleepUntil)
						sleepUntil = next.NextReminderTime;
				}

				myTimer.Interval = Convert.ToInt32((sleepUntil - now).TotalMilliseconds);
				if (myTimer.Interval <= 0)
				{
					// should never happen
					logger.Info("Warning: attempted to set invalid interval of " + myTimer.Interval + "; next=" + next);
					myTimer.Interval = 100;
				}
				nextPlannedWakeup = sleepUntil; // used to measure lateness
				logger.Debug("Setting timer to wait in " + myTimer.Interval + " at " + sleepUntil);
				myTimer.Start();

				if (DateTime.Now - now > new TimeSpan(0, 0, 0, 0, 500))
					logger.Debug("waitOrRemind took a long time: " + (DateTime.Now - now));
			}
			catch (Exception ex)
			{
				logger.Error("Unexpected error in waitOrRemind: ", ex);
				// don't throw out of here as it could result in the dialog being uncloseable
			}
		}

		private void ThisAddIn_Startup(object sender, System.EventArgs e)
		{
			logger.Info("AddIn is starting");

			Globals.BetterRemindersAddIn.Application.OptionsPagesAdd += new Outlook.ApplicationEvents_11_OptionsPagesAddEventHandler(Application_OptionsPagesAdd);
			
			// wait a bit before doing anything, to give outlook a chance to finish starting
			myTimer.Tick += new EventHandler(TimerEventProcessor);
			myTimer.Interval = StartupDelaySecs * 10;
			myTimer.Start();
			nextPlannedWakeup = DateTime.Now + new TimeSpan(0, 0, 0, 0, myTimer.Interval);

			// init sound player if necessary
			if (Properties.Settings.Default.playSoundOnReminder != "" && Properties.Settings.Default.playSoundOnReminder != "(default)")
				try
				{
					soundPlayer = new System.Media.SoundPlayer(Properties.Settings.Default.playSoundOnReminder);
					soundPlayer.Load();
				}
				catch (Exception ex)
				{
					logger.Error("Error loading sound: ", ex);
					soundPlayer = null;
				}

			logger.Debug("End of startup");
		}

        void Application_OptionsPagesAdd(Outlook.PropertyPages Pages)
        {
            Pages.Add(new PreferencesPage(), "BetterReminders");
        }


		private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
		{
			logger.Debug("Shutdown");
			myTimer.Stop();
			if (soundPlayer != null)
				soundPlayer.Dispose();
			logger.Shutdown();
		}

		private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
		{
			myTimer.Stop();
			logger.Debug("Timer triggered at " + DateTime.Now + " (late by " + Math.Round((DateTime.Now - nextPlannedWakeup).TotalSeconds) + "s)");
			waitOrRemind();
		}

		void ReminderFormClosedEventHandler(object sender, FormClosedEventArgs e)
		{
			logger.Debug("Form was closed");
			waitOrRemind();
		}


		private void playReminderSound()
		{
			if (Properties.Settings.Default.playSoundOnReminder == "(default)")
				System.Media.SystemSounds.Asterisk.Play();
			else if (soundPlayer != null)
				soundPlayer.Play();

		}


		#region VSTO generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InternalStartup()
		{
			this.Startup += new System.EventHandler(ThisAddIn_Startup);
			this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
		}

		#endregion
	}
}
