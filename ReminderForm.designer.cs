namespace BetterReminders
{
	partial class ReminderForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReminderForm));
			this.dismissButton = new System.Windows.Forms.Button();
			this.nameLabel = new System.Windows.Forms.Label();
			this.startTimeLabel = new System.Windows.Forms.Label();
			this.openButton = new System.Windows.Forms.Button();
			this.joinButton = new System.Windows.Forms.Button();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.timeList = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.timeCombo = new System.Windows.Forms.ComboBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.extraInfoLabel = new System.Windows.Forms.Label();
			this.dummyButtonToTakeFocusOnStartup = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// dismissButton
			// 
			this.dismissButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.dismissButton.Location = new System.Drawing.Point(15, 105);
			this.dismissButton.Name = "dismissButton";
			this.dismissButton.Size = new System.Drawing.Size(108, 23);
			this.dismissButton.TabIndex = 100;
			this.dismissButton.Text = "Dismiss";
			this.dismissButton.UseVisualStyleBackColor = true;
			this.dismissButton.Click += new System.EventHandler(this.dismissButton_Click);
			// 
			// nameLabel
			// 
			this.nameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nameLabel.AutoEllipsis = true;
			this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.nameLabel.Location = new System.Drawing.Point(12, 9);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(390, 39);
			this.nameLabel.TabIndex = 1;
			this.nameLabel.Text = "Title of appointment here";
			// 
			// startTimeLabel
			// 
			this.startTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.startTimeLabel.Location = new System.Drawing.Point(12, 85);
			this.startTimeLabel.Name = "startTimeLabel";
			this.startTimeLabel.Size = new System.Drawing.Size(390, 17);
			this.startTimeLabel.TabIndex = 2;
			this.startTimeLabel.Text = "Starting in: 0:01";
			this.startTimeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// openButton
			// 
			this.openButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.openButton.Location = new System.Drawing.Point(294, 105);
			this.openButton.Name = "openButton";
			this.openButton.Size = new System.Drawing.Size(108, 23);
			this.openButton.TabIndex = 2;
			this.openButton.Text = "Open Item";
			this.toolTip.SetToolTip(this.openButton, "Open the item and dismiss. Hold [Ctrl] to leave this window open.");
			this.openButton.UseVisualStyleBackColor = true;
			this.openButton.Click += new System.EventHandler(this.openButton_Click);
			// 
			// joinButton
			// 
			this.joinButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.joinButton.Location = new System.Drawing.Point(180, 105);
			this.joinButton.Name = "joinButton";
			this.joinButton.Size = new System.Drawing.Size(108, 23);
			this.joinButton.TabIndex = 1;
			this.joinButton.Text = "Join";
			this.toolTip.SetToolTip(this.joinButton, "Join the online meeting and dismiss. Hold [Ctrl] to leave this window open.");
			this.joinButton.UseVisualStyleBackColor = true;
			this.joinButton.Click += new System.EventHandler(this.joinButton_Click);
			// 
			// timer
			// 
			this.timer.Interval = 1000;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// timeList
			// 
			this.timeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.timeList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timeList.FormattingEnabled = true;
			this.timeList.ItemHeight = 16;
			this.timeList.Items.AddRange(new object[] {
            "Remind 30 seconds before start",
            "Remind in 30 seconds",
            "Remind in 1 minute",
            "Remind in 5 minutes"});
			this.timeList.Location = new System.Drawing.Point(15, 165);
			this.timeList.Name = "timeList";
			this.timeList.Size = new System.Drawing.Size(387, 68);
			this.timeList.TabIndex = 3;
			this.toolTip.SetToolTip(this.timeList, "This list contains your recently used snooze times");
			this.timeList.SelectedIndexChanged += new System.EventHandler(this.timeList_SelectedIndexChanged);
			this.timeList.DoubleClick += new System.EventHandler(this.timeList_DoubleClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 139);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(221, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "... or double-click/enter a time to remind after:";
			// 
			// timeCombo
			// 
			this.timeCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.timeCombo.FormattingEnabled = true;
			this.timeCombo.Location = new System.Drawing.Point(15, 249);
			this.timeCombo.Name = "timeCombo";
			this.timeCombo.Size = new System.Drawing.Size(387, 21);
			this.timeCombo.TabIndex = 4;
			this.timeCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.timeCombo_KeyUp);
			// 
			// extraInfoLabel
			// 
			this.extraInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.extraInfoLabel.Location = new System.Drawing.Point(12, 54);
			this.extraInfoLabel.Name = "extraInfoLabel";
			this.extraInfoLabel.Size = new System.Drawing.Size(390, 48);
			this.extraInfoLabel.TabIndex = 101;
			this.extraInfoLabel.Text = "Duration and location info";
			// 
			// dummyButtonToTakeFocusOnStartup
			// 
			this.dummyButtonToTakeFocusOnStartup.Location = new System.Drawing.Point(-30, -30);
			this.dummyButtonToTakeFocusOnStartup.Name = "dummyButtonToTakeFocusOnStartup";
			this.dummyButtonToTakeFocusOnStartup.Size = new System.Drawing.Size(0, 0);
			this.dummyButtonToTakeFocusOnStartup.TabIndex = 0;
			this.dummyButtonToTakeFocusOnStartup.Text = "exists to prevent user accidentally clicking a button if reminder window pops up " +
    "while they\'re typing ";
			this.dummyButtonToTakeFocusOnStartup.UseVisualStyleBackColor = true;
			// 
			// ReminderForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.dismissButton;
			this.ClientSize = new System.Drawing.Size(414, 282);
			this.Controls.Add(this.dummyButtonToTakeFocusOnStartup);
			this.Controls.Add(this.timeCombo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.timeList);
			this.Controls.Add(this.joinButton);
			this.Controls.Add(this.openButton);
			this.Controls.Add(this.startTimeLabel);
			this.Controls.Add(this.nameLabel);
			this.Controls.Add(this.dismissButton);
			this.Controls.Add(this.extraInfoLabel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ReminderForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "BetterReminders for Outlook - Reminder";
			this.TopMost = true;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReminderForm_FormClosed);
			this.Resize += new System.EventHandler(this.ReminderForm_Resize);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button dismissButton;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label startTimeLabel;
		private System.Windows.Forms.Button openButton;
		private System.Windows.Forms.Button joinButton;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.ListBox timeList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox timeCombo;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Label extraInfoLabel;
		private System.Windows.Forms.Button dummyButtonToTakeFocusOnStartup;
	}
}

