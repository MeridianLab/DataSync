namespace Harvest.Bridge.UI.UserControls
{
    partial class ctrlProjectEditor
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
            this.components = new System.ComponentModel.Container();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddVariable = new System.Windows.Forms.Button();
            this.btnAddStep = new System.Windows.Forms.Button();
            this.lstProjectSteps = new System.Windows.Forms.ListBox();
            this.btnSequenceUp = new System.Windows.Forms.Button();
            this.btnSequenceDown = new System.Windows.Forms.Button();
            this.btnSaveReload = new System.Windows.Forms.Button();
            this.btnTestProject = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tmrProgress = new System.Windows.Forms.Timer(this.components);
            this.rtfStepLog = new System.Windows.Forms.RichTextBox();
            this.chkAllToProcessParallel = new System.Windows.Forms.CheckBox();
            this.chkParallelForceSync = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmbNewStepType = new System.Windows.Forms.ComboBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkForceInserts = new System.Windows.Forms.CheckBox();
            this.cmbTimeFrameType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTimeFrame = new System.Windows.Forms.TextBox();
            this.logDetailBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logDetailBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(118, 12);
            this.txtName.Margin = new System.Windows.Forms.Padding(6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(476, 31);
            this.txtName.TabIndex = 25;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 25);
            this.label1.TabIndex = 24;
            this.label1.Text = "Name:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(6, 665);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(6);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtDescription.Size = new System.Drawing.Size(1500, 146);
            this.txtDescription.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 635);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 25);
            this.label2.TabIndex = 27;
            this.label2.Text = "Description";
            // 
            // btnAddVariable
            // 
            this.btnAddVariable.Location = new System.Drawing.Point(24, 62);
            this.btnAddVariable.Margin = new System.Windows.Forms.Padding(6);
            this.btnAddVariable.Name = "btnAddVariable";
            this.btnAddVariable.Size = new System.Drawing.Size(150, 44);
            this.btnAddVariable.TabIndex = 29;
            this.btnAddVariable.Text = "Add Variable";
            this.btnAddVariable.UseVisualStyleBackColor = true;
            this.btnAddVariable.Click += new System.EventHandler(this.btnAddVariable_Click);
            // 
            // btnAddStep
            // 
            this.btnAddStep.Location = new System.Drawing.Point(510, 62);
            this.btnAddStep.Margin = new System.Windows.Forms.Padding(6);
            this.btnAddStep.Name = "btnAddStep";
            this.btnAddStep.Size = new System.Drawing.Size(150, 44);
            this.btnAddStep.TabIndex = 30;
            this.btnAddStep.Text = "Add Step";
            this.btnAddStep.UseVisualStyleBackColor = true;
            this.btnAddStep.Click += new System.EventHandler(this.btnAddStep_Click);
            // 
            // lstProjectSteps
            // 
            this.lstProjectSteps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstProjectSteps.FormattingEnabled = true;
            this.lstProjectSteps.ItemHeight = 25;
            this.lstProjectSteps.Location = new System.Drawing.Point(16, 117);
            this.lstProjectSteps.Margin = new System.Windows.Forms.Padding(6);
            this.lstProjectSteps.Name = "lstProjectSteps";
            this.lstProjectSteps.Size = new System.Drawing.Size(640, 454);
            this.lstProjectSteps.TabIndex = 32;
            // 
            // btnSequenceUp
            // 
            this.btnSequenceUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSequenceUp.Location = new System.Drawing.Point(152, 592);
            this.btnSequenceUp.Margin = new System.Windows.Forms.Padding(6);
            this.btnSequenceUp.Name = "btnSequenceUp";
            this.btnSequenceUp.Size = new System.Drawing.Size(150, 44);
            this.btnSequenceUp.TabIndex = 33;
            this.btnSequenceUp.Text = "Move Up";
            this.btnSequenceUp.UseVisualStyleBackColor = true;
            this.btnSequenceUp.Click += new System.EventHandler(this.btnSequenceUp_Click);
            // 
            // btnSequenceDown
            // 
            this.btnSequenceDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSequenceDown.Location = new System.Drawing.Point(314, 592);
            this.btnSequenceDown.Margin = new System.Windows.Forms.Padding(6);
            this.btnSequenceDown.Name = "btnSequenceDown";
            this.btnSequenceDown.Size = new System.Drawing.Size(150, 44);
            this.btnSequenceDown.TabIndex = 34;
            this.btnSequenceDown.Text = "Move Down";
            this.btnSequenceDown.UseVisualStyleBackColor = true;
            this.btnSequenceDown.Click += new System.EventHandler(this.btnSequenceDown_Click);
            // 
            // btnSaveReload
            // 
            this.btnSaveReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveReload.Location = new System.Drawing.Point(556, 592);
            this.btnSaveReload.Margin = new System.Windows.Forms.Padding(6);
            this.btnSaveReload.Name = "btnSaveReload";
            this.btnSaveReload.Size = new System.Drawing.Size(232, 44);
            this.btnSaveReload.TabIndex = 35;
            this.btnSaveReload.Text = "Save And Reload";
            this.btnSaveReload.UseVisualStyleBackColor = true;
            this.btnSaveReload.Click += new System.EventHandler(this.btnSaveReload_Click);
            // 
            // btnTestProject
            // 
            this.btnTestProject.Location = new System.Drawing.Point(700, 12);
            this.btnTestProject.Margin = new System.Windows.Forms.Padding(6);
            this.btnTestProject.Name = "btnTestProject";
            this.btnTestProject.Size = new System.Drawing.Size(150, 44);
            this.btnTestProject.TabIndex = 36;
            this.btnTestProject.Text = "Test Run";
            this.btnTestProject.UseVisualStyleBackColor = true;
            this.btnTestProject.Click += new System.EventHandler(this.btnTestProject_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // tmrProgress
            // 
            this.tmrProgress.Interval = 200;
            this.tmrProgress.Tick += new System.EventHandler(this.tmrProgress_Tick);
            // 
            // rtfStepLog
            // 
            this.rtfStepLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfStepLog.Location = new System.Drawing.Point(6, 65);
            this.rtfStepLog.Margin = new System.Windows.Forms.Padding(6);
            this.rtfStepLog.Name = "rtfStepLog";
            this.rtfStepLog.Size = new System.Drawing.Size(1500, 741);
            this.rtfStepLog.TabIndex = 37;
            this.rtfStepLog.Text = "";
            // 
            // chkAllToProcessParallel
            // 
            this.chkAllToProcessParallel.AutoSize = true;
            this.chkAllToProcessParallel.Location = new System.Drawing.Point(774, 27);
            this.chkAllToProcessParallel.Margin = new System.Windows.Forms.Padding(6);
            this.chkAllToProcessParallel.Name = "chkAllToProcessParallel";
            this.chkAllToProcessParallel.Size = new System.Drawing.Size(273, 29);
            this.chkAllToProcessParallel.TabIndex = 40;
            this.chkAllToProcessParallel.Text = "Allow Parallel Processing";
            this.chkAllToProcessParallel.UseVisualStyleBackColor = true;
            // 
            // chkParallelForceSync
            // 
            this.chkParallelForceSync.AutoSize = true;
            this.chkParallelForceSync.Location = new System.Drawing.Point(774, 62);
            this.chkParallelForceSync.Margin = new System.Windows.Forms.Padding(6);
            this.chkParallelForceSync.Name = "chkParallelForceSync";
            this.chkParallelForceSync.Size = new System.Drawing.Size(266, 29);
            this.chkParallelForceSync.TabIndex = 41;
            this.chkParallelForceSync.Text = "Parallel Force Sync First";
            this.chkParallelForceSync.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1538, 871);
            this.tabControl1.TabIndex = 42;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmbNewStepType);
            this.tabPage1.Controls.Add(this.chkEnabled);
            this.tabPage1.Controls.Add(this.txtName);
            this.tabPage1.Controls.Add(this.chkParallelForceSync);
            this.tabPage1.Controls.Add(this.chkAllToProcessParallel);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnSaveReload);
            this.tabPage1.Controls.Add(this.txtDescription);
            this.tabPage1.Controls.Add(this.btnSequenceDown);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnSequenceUp);
            this.tabPage1.Controls.Add(this.btnAddVariable);
            this.tabPage1.Controls.Add(this.btnAddStep);
            this.tabPage1.Controls.Add(this.lstProjectSteps);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage1.Size = new System.Drawing.Size(1530, 833);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Project";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmbNewStepType
            // 
            this.cmbNewStepType.FormattingEnabled = true;
            this.cmbNewStepType.Items.AddRange(new object[] {
            "SQLUpdate",
            "SQLRead",
            "StepFlow"});
            this.cmbNewStepType.Location = new System.Drawing.Point(256, 65);
            this.cmbNewStepType.Margin = new System.Windows.Forms.Padding(6);
            this.cmbNewStepType.Name = "cmbNewStepType";
            this.cmbNewStepType.Size = new System.Drawing.Size(238, 33);
            this.cmbNewStepType.TabIndex = 43;
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(610, 17);
            this.chkEnabled.Margin = new System.Windows.Forms.Padding(6);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(110, 29);
            this.chkEnabled.TabIndex = 42;
            this.chkEnabled.Text = "Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkForceInserts);
            this.tabPage2.Controls.Add(this.cmbTimeFrameType);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txtTimeFrame);
            this.tabPage2.Controls.Add(this.rtfStepLog);
            this.tabPage2.Controls.Add(this.btnTestProject);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage2.Size = new System.Drawing.Size(1530, 833);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Test";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkForceInserts
            // 
            this.chkForceInserts.AutoSize = true;
            this.chkForceInserts.Location = new System.Drawing.Point(862, 19);
            this.chkForceInserts.Margin = new System.Windows.Forms.Padding(6);
            this.chkForceInserts.Name = "chkForceInserts";
            this.chkForceInserts.Size = new System.Drawing.Size(326, 29);
            this.chkForceInserts.TabIndex = 42;
            this.chkForceInserts.Text = "Default Action Update Records";
            this.chkForceInserts.UseVisualStyleBackColor = true;
            // 
            // cmbTimeFrameType
            // 
            this.cmbTimeFrameType.AutoCompleteCustomSource.AddRange(new string[] {
            "Days",
            "Hours",
            "Minutes"});
            this.cmbTimeFrameType.FormattingEnabled = true;
            this.cmbTimeFrameType.Items.AddRange(new object[] {
            "Default",
            "Minutes",
            "Days",
            "Date"});
            this.cmbTimeFrameType.Location = new System.Drawing.Point(232, 15);
            this.cmbTimeFrameType.Margin = new System.Windows.Forms.Padding(6);
            this.cmbTimeFrameType.Name = "cmbTimeFrameType";
            this.cmbTimeFrameType.Size = new System.Drawing.Size(238, 33);
            this.cmbTimeFrameType.TabIndex = 41;
            this.cmbTimeFrameType.Text = "Minutes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(96, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 25);
            this.label4.TabIndex = 40;
            this.label4.Text = "Time Frame";
            // 
            // txtTimeFrame
            // 
            this.txtTimeFrame.Location = new System.Drawing.Point(488, 15);
            this.txtTimeFrame.Margin = new System.Windows.Forms.Padding(6);
            this.txtTimeFrame.Name = "txtTimeFrame";
            this.txtTimeFrame.Size = new System.Drawing.Size(196, 31);
            this.txtTimeFrame.TabIndex = 38;
            this.txtTimeFrame.Text = "30";
            // 
            // logDetailBindingSource
            // 
            this.logDetailBindingSource.DataSource = typeof(Harvest.Bridge.Common.Models.LogDetail);
            // 
            // ctrlProjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ctrlProjectEditor";
            this.Size = new System.Drawing.Size(1538, 871);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logDetailBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddVariable;
        private System.Windows.Forms.Button btnAddStep;
        private System.Windows.Forms.ListBox lstProjectSteps;
        private System.Windows.Forms.Button btnSequenceUp;
        private System.Windows.Forms.Button btnSequenceDown;
        private System.Windows.Forms.Button btnSaveReload;
        private System.Windows.Forms.Button btnTestProject;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer tmrProgress;
        private System.Windows.Forms.BindingSource logDetailBindingSource;
        private System.Windows.Forms.RichTextBox rtfStepLog;
        private System.Windows.Forms.CheckBox chkAllToProcessParallel;
        private System.Windows.Forms.CheckBox chkParallelForceSync;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox chkForceInserts;
        private System.Windows.Forms.ComboBox cmbTimeFrameType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTimeFrame;
        private System.Windows.Forms.ComboBox cmbNewStepType;
    }
}
