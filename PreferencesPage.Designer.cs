namespace BetterReminders
{
	partial class PreferencesPage
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.defaultReminderTimeSecs = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.searchFrequencyMins = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.reminderSoundPath = new System.Windows.Forms.ComboBox();
			this.reminderSoundBrowse = new System.Windows.Forms.Button();
			this.reminderSoundBrowseDialog = new System.Windows.Forms.OpenFileDialog();
			this.label5 = new System.Windows.Forms.Label();
			this.meetingUrlRegex = new System.Windows.Forms.ComboBox();
			this.subjectExcludeRegex = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.defaultReminderTimeSecs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.searchFrequencyMins)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(15, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(293, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Default reminder time (secs):";
			// 
			// defaultReminderTimeSecs
			// 
			this.defaultReminderTimeSecs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.defaultReminderTimeSecs.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.defaultReminderTimeSecs.Location = new System.Drawing.Point(326, 30);
			this.defaultReminderTimeSecs.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.defaultReminderTimeSecs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.defaultReminderTimeSecs.Name = "defaultReminderTimeSecs";
			this.defaultReminderTimeSecs.Size = new System.Drawing.Size(99, 20);
			this.defaultReminderTimeSecs.TabIndex = 2;
			this.defaultReminderTimeSecs.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.defaultReminderTimeSecs.ValueChanged += new System.EventHandler(this.valueChanged);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(15, 70);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(302, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "Appointment search frequency (mins):";
			// 
			// searchFrequencyMins
			// 
			this.searchFrequencyMins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.searchFrequencyMins.Location = new System.Drawing.Point(326, 70);
			this.searchFrequencyMins.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.searchFrequencyMins.Name = "searchFrequencyMins";
			this.searchFrequencyMins.Size = new System.Drawing.Size(99, 20);
			this.searchFrequencyMins.TabIndex = 4;
			this.searchFrequencyMins.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.searchFrequencyMins.ValueChanged += new System.EventHandler(this.valueChanged);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(15, 231);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(305, 23);
			this.label3.TabIndex = 9;
			this.label3.Text = "Reminder sound (.wav)";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Location = new System.Drawing.Point(18, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(407, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = " BetterReminders was created by Ben Spiller";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// reminderSoundPath
			// 
			this.reminderSoundPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.reminderSoundPath.FormattingEnabled = true;
			this.reminderSoundPath.Items.AddRange(new object[] {
            "(none)",
            "(default)"});
			this.reminderSoundPath.Location = new System.Drawing.Point(18, 255);
			this.reminderSoundPath.Name = "reminderSoundPath";
			this.reminderSoundPath.Size = new System.Drawing.Size(323, 21);
			this.reminderSoundPath.TabIndex = 10;
			this.reminderSoundPath.TextChanged += new System.EventHandler(this.valueChanged);
			// 
			// reminderSoundBrowse
			// 
			this.reminderSoundBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.reminderSoundBrowse.Location = new System.Drawing.Point(350, 255);
			this.reminderSoundBrowse.Name = "reminderSoundBrowse";
			this.reminderSoundBrowse.Size = new System.Drawing.Size(75, 23);
			this.reminderSoundBrowse.TabIndex = 11;
			this.reminderSoundBrowse.Text = "Browse";
			this.reminderSoundBrowse.UseVisualStyleBackColor = true;
			this.reminderSoundBrowse.Click += new System.EventHandler(this.reminderSoundBrowse_Click);
			// 
			// reminderSoundBrowseDialog
			// 
			this.reminderSoundBrowseDialog.Filter = "Wav files|*.wav";
			this.reminderSoundBrowseDialog.Title = "Select reminder sound";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(15, 108);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(102, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Meeting URL regex:";
			// 
			// meetingUrlRegex
			// 
			this.meetingUrlRegex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.meetingUrlRegex.FormattingEnabled = true;
			this.meetingUrlRegex.Location = new System.Drawing.Point(18, 135);
			this.meetingUrlRegex.Name = "meetingUrlRegex";
			this.meetingUrlRegex.Size = new System.Drawing.Size(407, 21);
			this.meetingUrlRegex.TabIndex = 6;
			this.meetingUrlRegex.TextChanged += new System.EventHandler(this.valueChanged);
			// 
			// subjectExcludeRegex
			// 
			this.subjectExcludeRegex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.subjectExcludeRegex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.subjectExcludeRegex.FormattingEnabled = true;
			this.subjectExcludeRegex.Location = new System.Drawing.Point(18, 196);
			this.subjectExcludeRegex.Name = "subjectExcludeRegex";
			this.subjectExcludeRegex.Size = new System.Drawing.Size(407, 21);
			this.subjectExcludeRegex.TabIndex = 8;
			this.subjectExcludeRegex.TextChanged += new System.EventHandler(this.valueChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(15, 169);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(122, 13);
			this.label6.TabIndex = 7;
			this.label6.Text = "Subject exclusion regex:";
			// 
			// PreferencesPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.subjectExcludeRegex);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.meetingUrlRegex);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.reminderSoundBrowse);
			this.Controls.Add(this.reminderSoundPath);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.searchFrequencyMins);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.defaultReminderTimeSecs);
			this.Controls.Add(this.label1);
			this.Name = "PreferencesPage";
			this.Size = new System.Drawing.Size(440, 317);
			this.Load += new System.EventHandler(this.PreferencesPage_Load);
			((System.ComponentModel.ISupportInitialize)(this.defaultReminderTimeSecs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.searchFrequencyMins)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown defaultReminderTimeSecs;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown searchFrequencyMins;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox reminderSoundPath;
		private System.Windows.Forms.Button reminderSoundBrowse;
		private System.Windows.Forms.OpenFileDialog reminderSoundBrowseDialog;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox meetingUrlRegex;
		private System.Windows.Forms.ComboBox subjectExcludeRegex;
		private System.Windows.Forms.Label label6;
	}
}
