using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

// Copyright (c) 2016 Ben Spiller. 


namespace BetterReminders
{

	/// <summary>
	/// Shows a reminder window for a SINGLE outlook meeting. 
	/// User can dismiss or specify a future time to wakeup and get another reminder, 
	/// and the meeting's NextReminderTime will be set accordingly.
	/// Form is disposed automatically on close. 
	/// 
	/// Callers should typically call Show() and then listen for the close notification
	/// </summary>
	public partial class ReminderForm : Form
	{
		private static Logger logger = Logger.GetLogger();
		private UpcomingMeeting meeting;
		private readonly string joinUrl;

		public ReminderForm(UpcomingMeeting meeting)
		{
			this.meeting = meeting;
			this.joinUrl = meeting.GetMeetingUrl();
			InitializeComponent();

			// populate the combo with a hardcoded list of items whose primary purpose is to illustrate the 
			// syntax of all possible values
			timeCombo.Items.Add(new SnoozeTime(20, false));
			timeCombo.Items.Add(new SnoozeTime(-20, false));
			timeCombo.Items.Add(new SnoozeTime(30, true));
			timeCombo.Items.Add(new SnoozeTime(60, true));

			// set labels for this meeting
			nameLabel.Text = meeting.Subject;
			extraInfoLabel.Text = "";
			if (meeting.Location.Length > 0)
				extraInfoLabel.Text = "Location: " + meeting.Location + "; ";
			extraInfoLabel.Text += "Duration " + meeting.OutlookItem.Duration + " mins";

			if (meeting.getOrganizer() != null)
				extraInfoLabel.Text += "; organizer: " + meeting.getOrganizer();
			IEnumerable<string> attendees = meeting.GetAttendees();
			if (attendees.Count() == 1)
				extraInfoLabel.Text += "; with: " + string.Join(", ", attendees);
			else
				extraInfoLabel.Text += "; " + attendees.Count() + " attendees";
			extraInfoLabel.Text += "";

			toolTip.SetToolTip(nameLabel, nameLabel.Text); // in case it's too long
			toolTip.SetToolTip(extraInfoLabel, meeting.Body);

			joinButton.Visible = joinUrl.Length > 0;

			// start time timer
			timer.Enabled = true;
			updateStartTime();

			// reminder times list. stored in MRU order but sorted in time order
			timeList.Items.Clear();

			var list = SnoozeTime.ParseList(Properties.Settings.Default.mruSnoozeTimes);
			if (list.Count == 0)
			{
				list.Add(new SnoozeTime(-30, false));
				list.Add(new SnoozeTime(30, true));
				list.Add(new SnoozeTime(60, true));
				list.Add(new SnoozeTime(60 * 5, true));
				Properties.Settings.Default.mruSnoozeTimes = SnoozeTime.ListToString(list);
				Properties.Settings.Default.Save();
			}
			list.Sort();
			list.ForEach(st => timeList.Items.Add(st));

			// by default, dismiss when dialog is closed. unless snooze is selected
			meeting.Dismiss();

			reactivateTime = DateTime.Now + reactivateTimeSpan;


		}

		private void ReminderForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			timer.Enabled = false;
		}

		private void updateStartTime()
		{
			TimeSpan span = (meeting.StartTime-DateTime.Now);
			string spanstr;
			if (span.TotalSeconds >= 0)
				spanstr = "in ";
 			else
				spanstr = "overdue by ";
			if (startTimeLabel.Font.Bold != (span.TotalSeconds < 0))
				startTimeLabel.Font = new Font(startTimeLabel.Font, (span.TotalSeconds < 0) ? FontStyle.Bold : FontStyle.Regular);
			//assume no days
			if (span.Hours > 0)
				spanstr += span.ToString("h\\:mm\\:ss");
			else
				spanstr += span.ToString("m\\:ss");
			startTimeLabel.Text = "Starting at " + String.Format("{0:t}", meeting.StartTime) + ", "+spanstr;
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			updateStartTime();
			// keep this dialog around for a bit so people can apologise if they've forgotten a meeting,
			// but 2 hours after the meeting ends give up
			if (DateTime.Now > meeting.EndTime + new TimeSpan(2, 0, 0))
				Close();
			else if (DateTime.Now > reactivateTime)
			{
				// periodically reactivate and restore in case it was minimized or lost focus somehow
				reactivateTime = DateTime.Now + reactivateTimeSpan;
				if (WindowState != FormWindowState.Normal)
					logger.Debug("Restoring and activating reminder window after timeout");
				WindowState = FormWindowState.Normal;
				Activate();
				//SetForegroundWindow(this.Handle);

			}
		}

		private void openButton_Click(object sender, EventArgs e)
		{
			if (!meeting.IsDeleted)
				meeting.OutlookItem.Display();

			if (!ModifierKeys.HasFlag(Keys.Control))
				Close();
		}

		private void joinButton_Click(object sender, EventArgs e)
		{
			// launching a join URL should open it in default web browser
			ProcessStartInfo sInfo = new ProcessStartInfo(joinUrl);
			Process.Start(sInfo);

			if (!ModifierKeys.HasFlag(Keys.Control))
				Close();
		}

		private void snooze(string snoozeTime, bool record=true)
		{
			SnoozeTime st;
			DateTime wakeup;
			try
			{
				st = SnoozeTime.Parse(snoozeTime);
				wakeup = st.GetNextReminderTime(meeting.StartTime);
				if (wakeup < DateTime.Now)
					throw new Exception("Reminder time is in the past");
			} catch (Exception e)
			{
				MessageBox.Show(this, "Cannot follow snooze instruction '" + snoozeTime + "': " + e.Message, "Invalid Snooze", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			logger.Info("Snooze time "+st+" -> reminder time is: "+wakeup);


			// record the snoozeTime MRU
			var list = SnoozeTime.ParseList(Properties.Settings.Default.mruSnoozeTimes);
			list.Remove(st);
			list.Insert(0, st);
			while (list.Count > 4) list.RemoveAt(list.Count - 1);
			Properties.Settings.Default.mruSnoozeTimes = SnoozeTime.ListToString(list);
			Properties.Settings.Default.Save();

			meeting.NextReminderTime = wakeup;
			Close();
		}

		private void timeList_SelectedIndexChanged(object sender, EventArgs e)
		{
			timeCombo.Text = timeList.SelectedItem.ToString();
		}

		private void timeList_DoubleClick(object sender, EventArgs e)
		{
			snooze(timeList.SelectedItem.ToString());
		}

		private DateTime reactivateTime = DateTime.MaxValue;
		// long enough to finish what you're doing, but not to make you too late for the meeting
		// could make this configurable
		private readonly TimeSpan reactivateTimeSpan = new TimeSpan(0, 0, 45);
		private void ReminderForm_Resize(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Minimized)
			{
				reactivateTime = DateTime.Now + reactivateTimeSpan;
			}
		}

		private void timeCombo_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				snooze(timeCombo.Text);
		}
		private void dismissButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
