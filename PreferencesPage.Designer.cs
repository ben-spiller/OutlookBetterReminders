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
			this.reminderSoundPath = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
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
			this.label1.Size = new System.Drawing.Size(227, 13);
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
			this.defaultReminderTimeSecs.Location = new System.Drawing.Point(260, 30);
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
			this.label2.Location = new System.Drawing.Point(18, 70);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(236, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "Appointment search frequency (mins):";
			// 
			// searchFrequencyMins
			// 
			this.searchFrequencyMins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.searchFrequencyMins.Location = new System.Drawing.Point(260, 70);
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
			this.label3.Location = new System.Drawing.Point(15, 97);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(239, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "Reminder sound (.wav)";
			// 
			// reminderSoundPath
			// 
			this.reminderSoundPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.reminderSoundPath.Location = new System.Drawing.Point(18, 123);
			this.reminderSoundPath.Name = "reminderSoundPath";
			this.reminderSoundPath.Size = new System.Drawing.Size(341, 20);
			this.reminderSoundPath.TabIndex = 6;
			this.reminderSoundPath.TextChanged += new System.EventHandler(this.valueChanged);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Location = new System.Drawing.Point(18, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(341, 15);
			this.label4.TabIndex = 7;
			this.label4.Text = " BetterReminders was created by Ben Spiller";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// PreferencesPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label4);
			this.Controls.Add(this.reminderSoundPath);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.searchFrequencyMins);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.defaultReminderTimeSecs);
			this.Controls.Add(this.label1);
			this.Name = "PreferencesPage";
			this.Size = new System.Drawing.Size(374, 169);
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
		private System.Windows.Forms.TextBox reminderSoundPath;
		private System.Windows.Forms.Label label4;
	}
}
