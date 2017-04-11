using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Text.RegularExpressions;

// Copyright (c) 2016-2017 Ben Spiller. 

namespace BetterReminders
{
	/// <summary>
	/// Represents an UpcomingMeeting, containing NextReminderTime and a reference to the underlying outlook item. 
	/// 
	/// Provides some accessors to simplify getting data from it and a local cache of some values to avoid having 
	/// to call methods on the outlook object (which could theoretically involve crossing COM boundary, marshalling, 
	/// thread serialization etc) any more frequently than necessary, and because such methods fail if the item is 
	/// deleted and there are some fields it would be tiresome to check individually before each calll. 
	/// </summary>
	public class UpcomingMeeting : IDisposable
	{
		private static Logger logger = Logger.GetLogger();

		/// <summary>
		/// The next time we need to show a reminder for this item. 
		/// If dismissed, holds DateTime.MaxValue (gives simpler semantics than using a nullable)
		/// </summary>
		public DateTime NextReminderTime = DateTime.MaxValue;

		public bool IsDismissed { get { return NextReminderTime == DateTime.MaxValue; } }

		public void Dismiss() { NextReminderTime = DateTime.MaxValue; }

		public DateTime StartTime;
		public DateTime EndTime;
		public readonly Outlook.AppointmentItem OutlookItem;
		private DateTime lastModificationTime;

		private string id;
		public string ID { get { return id; } }

		private string subject;
		public string Subject { get { return subject ?? "<no subject>"; } }

		public string Location { get { return OutlookItem.Location ?? ""; } }
		public string Body { get { return OutlookItem.Body ?? ""; } }

		// works for both lync and webex invites
		public const string DefaultMeetingUrlRegex = "HYPERLINK \"(?<url>[^\"]+)\" *Join .*[Mm]eeting";

		public string GetMeetingUrl()
		{
			Regex re;
			string regexSetting = Properties.Settings.Default.meetingUrlRegex;
			try
			{
				if (String.IsNullOrWhiteSpace(regexSetting))
					re = new Regex(DefaultMeetingUrlRegex);
				else
					re = new Regex(regexSetting);
				Match m = re.Match(Body);
				if (m.Success)
					return m.Groups["url"].Value;
				return "";
			}
			catch (Exception e)
			{
				logger.Info("Error creating or matching regex '" + regexSetting + "' for "+this+": " + e);
				return "";
			}
		}
		public IEnumerable<string> GetAttendees()
		{
			string currentUser = OutlookItem.Application.Session.CurrentUser.Name;
			string all = OutlookItem.RequiredAttendees+";"+OutlookItem.OptionalAttendees;
			return all.Split(';').Select(s => s.Trim()).Where(s => ((s??"") != "") && s != currentUser);
		}
		public string getOrganizer()
		{
			if (OutlookItem.Organizer == null) return null;
			if (OutlookItem.Organizer == OutlookItem.Application.Session.CurrentUser.Name) return "<me>";
			return OutlookItem.Organizer;
		}


		public UpcomingMeeting(Outlook.AppointmentItem item, DateTime reminderTime)
		{
			if (item.AllDayEvent) throw new ArgumentException("Cannot handle all day events");
			deleted = false;
			item.BeforeDelete += item_BeforeDelete;
			OutlookItem = item;

			id = item.EntryID;
			subject = item.Subject;
			NextReminderTime = reminderTime;
			StartTime = item.Start;
			EndTime = item.End;
			lastModificationTime = item.LastModificationTime;
		}

		public bool IsDeleted { get { return deleted; } }
		private bool deleted;
		private void item_BeforeDelete(object Item, ref bool Cancel)
		{
			logger.Info("Item was deleted: "+Subject);
			// to prevent reminders being displayed for this once it's deleted, 
			Dispose();
		}

		public override string ToString()
		{
			if (deleted)
				return "DeletedMeeting<subject=" + Subject + ">";
			return "Meeting<start="+StartTime+", end="+EndTime+", reminder="+(IsDismissed ? "<dismissed>" : NextReminderTime.ToString())+", subject="+Subject+">";
		}

		/// Checks if this item has been modified, and updates all relevant fields if it has. 
		/// Returns true if the start time has changed
		public bool UpdateStartTime()
		{
			// this may be premature optimization, but try to okeep calls into the outlook item to a minimum 
			// since there's some COM marshalling and thread serialization that has to happen behind the scenes
			if (!deleted && OutlookItem.LastModificationTime != lastModificationTime)
			{
				lastModificationTime = OutlookItem.LastModificationTime;
				bool result = OutlookItem.Start != StartTime;
				StartTime = OutlookItem.Start;
				EndTime = OutlookItem.End;
				subject = OutlookItem.Subject;
				logger.Info("Item was changed by Outlook at " + lastModificationTime + ": " + this);
				return result;
			}
			else
				return false;
		}


		#region IDisposable Members

		public void Dispose()
		{
			// this method exists to help deal with deleted items
			if (deleted) return;
			// dismissing disables most of the problematic code paths
			Dismiss(); 
			deleted = true;

			OutlookItem.BeforeDelete -= item_BeforeDelete;
		}

		#endregion
	}
}
