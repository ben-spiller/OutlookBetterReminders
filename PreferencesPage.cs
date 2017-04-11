using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Text.RegularExpressions;

// Copyright (c) 2016-2017 Ben Spiller. 


namespace BetterReminders
{
	[ComVisible(true)]
	public partial class PreferencesPage : UserControl, Outlook.PropertyPage
	{
		private Logger logger = Logger.GetLogger();
		private Outlook.PropertyPageSite propertyPageSite;

		public PreferencesPage()
		{
			InitializeComponent();
		}

		bool isDirty = false;
		void Outlook.PropertyPage.Apply()
		{
			string meetingregex = meetingUrlRegex.Text;
			// normalize use of default regex to improve upgradeability
			if (meetingregex == UpcomingMeeting.DefaultMeetingUrlRegex)
				meetingregex = "";
			if (meetingregex != "")
				try
				{
					Regex re = new Regex(meetingregex);
					if (!re.GetGroupNames().Contains("url"))
						throw new Exception("The meeting regex must include a regex group named 'url' e.g. '"+UpcomingMeeting.DefaultMeetingUrlRegex+"'");
				} catch (Exception e)
				{
					string msg = "Invalid meeting URL regex: "+e.Message;
					MessageBox.Show(msg, "Invalid Meeting URL regex", MessageBoxButtons.OK, MessageBoxIcon.Error);
					throw new Exception(msg, e); // stops isDirty being changed
				}

			// first, validation
			string reminderSound = (reminderSoundPath.Text == "(none)") ? "" : reminderSoundPath.Text;
			if (reminderSound != "" && reminderSound != "(default)" &&
				!System.IO.File.Exists(reminderSound))
			{
				MessageBox.Show("Reminder .wav path does not exist. Provide a valid .wav path, empty string or (default).", "Invalid BetterReminders settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
				throw new Exception("BetterReminders got invalid input"); // stops isDirty being changed
			}

			Properties.Settings.Default.defaultReminderSecs = Decimal.ToInt32(defaultReminderTimeSecs.Value);
			Properties.Settings.Default.searchFrequencySecs = Decimal.ToInt32(searchFrequencyMins.Value)*60;
			Properties.Settings.Default.playSoundOnReminder = reminderSound;
			Properties.Settings.Default.meetingUrlRegex = meetingregex;
			Properties.Settings.Default.Save();
			isDirty = false;
		}

		bool Outlook.PropertyPage.Dirty
		{
			get
			{
				return isDirty;
			}
		}
		void Outlook.PropertyPage.GetPageInfo(ref string helpFile, ref int helpContext)
		{
			// nothing to do here
		}

		private void valueChanged(object sender, EventArgs e)
		{
			if (propertyPageSite == null) return; // still loading

			if (isDirty) return; //already called

			isDirty = true;
			propertyPageSite.OnStatusChange();
		}

		Outlook.PropertyPageSite GetPropertyPageSite()
		{
			// this is what MS's documentation recommends, but doesn't seem to work as Parent is null
			if (Parent is Outlook.PropertyPageSite) return (Outlook.PropertyPageSite)Parent;

			// nb: I can't believe this hack is really required, but since Parent=null 
			// I can't find any better way to do it

			Type type = typeof(System.Object);
			string assembly = type.Assembly.CodeBase.Replace("mscorlib.dll", "System.Windows.Forms.dll");
			assembly = assembly.Replace("file:///", "");

			string assemblyName = System.Reflection.AssemblyName.GetAssemblyName(assembly).FullName;
			Type unsafeNativeMethods = Type.GetType(System.Reflection.Assembly.CreateQualifiedName(assemblyName, "System.Windows.Forms.UnsafeNativeMethods"));

			Type oleObj = unsafeNativeMethods.GetNestedType("IOleObject");
			System.Reflection.MethodInfo methodInfo = oleObj.GetMethod("GetClientSite");
			object propertyPageSite = methodInfo.Invoke(this, null);

			return (Outlook.PropertyPageSite)propertyPageSite;
		}


		private void PreferencesPage_Load(object sender, EventArgs e)
		{
			try
			{
				defaultReminderTimeSecs.Value = Properties.Settings.Default.defaultReminderSecs;
				searchFrequencyMins.Value = Math.Max(1, Math.Min(Properties.Settings.Default.searchFrequencySecs/60, searchFrequencyMins.Maximum));
				reminderSoundPath.Text = Properties.Settings.Default.playSoundOnReminder;

				// provide default in case user forgets
				meetingUrlRegex.Items.Add(UpcomingMeeting.DefaultMeetingUrlRegex);
				
				meetingUrlRegex.Text = Properties.Settings.Default.meetingUrlRegex;
				if (string.IsNullOrWhiteSpace(meetingUrlRegex.Text))
					meetingUrlRegex.Text = UpcomingMeeting.DefaultMeetingUrlRegex;

				propertyPageSite = GetPropertyPageSite();
				logger.Info("Successfully loaded preferences page");
			}
			catch (Exception ex)
			{
				logger.Error("Error loading preferences page: ", ex);
				throw; 
			}
		}

		private void reminderSoundBrowse_Click(object sender, EventArgs e)
		{
			if (reminderSoundPath.Text.StartsWith("("))
				reminderSoundBrowseDialog.FileName = "";
			else
				reminderSoundBrowseDialog.FileName = reminderSoundPath.Text;
			if (reminderSoundBrowseDialog.ShowDialog(ParentForm) == DialogResult.OK)
				reminderSoundPath.Text = reminderSoundBrowseDialog.FileName;
		}

	}

}
