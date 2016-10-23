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

// Copyright (c) 2016 Ben Spiller. 


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
			if (reminderSoundPath.Text != "" && reminderSoundPath.Text != "(default)" && 
				!System.IO.File.Exists(reminderSoundPath.Text))
			{
				MessageBox.Show("Reminder .wav path does not exist. Provide a valid .wav path, empty string or (default).", "Invalid BetterReminders settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
				throw new Exception("BetterReminders got invalid input"); // stops isDirty being changed
			}

			Properties.Settings.Default.defaultReminderSecs = Decimal.ToInt32(defaultReminderTimeSecs.Value);
			Properties.Settings.Default.searchFrequencySecs = Decimal.ToInt32(searchFrequencyMins.Value)*60;
			Properties.Settings.Default.playSoundOnReminder = reminderSoundPath.Text;
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

				propertyPageSite = GetPropertyPageSite();
				logger.Info("Successfully loaded preferences page");
			}
			catch (Exception ex)
			{
				logger.Error("Error loading preferences page: ", ex);
				throw; 
			}
		}

	}

}
