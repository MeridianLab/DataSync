namespace Harvest.Bridge.UI.UserControls
{
    partial class ctrlSolutionEditor
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSequenceDown = new System.Windows.Forms.Button();
            this.btnAddProject = new System.Windows.Forms.Button();
            this.txtNewProjectName = new System.Windows.Forms.TextBox();
            this.lstProjectSequence = new System.Windows.Forms.ListBox();
            this.btnApplySequenceUpdate = new System.Windows.Forms.Button();
            this.btnSequenceUp = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkPreDeleteData = new System.Windows.Forms.CheckBox();
            this.chkDefaultActionUpdateAll = new System.Windows.Forms.CheckBox();
            this.cmbTimeFrameType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTimeFrame = new System.Windows.Forms.TextBox();
            this.lblCurrentStep = new System.Windows.Forms.Label();
            this.lblCrntProject = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnSaveConfigChanges = new System.Windows.Forms.Button();
            this.dtgConfiguration = new System.Windows.Forms.DataGridView();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createDateDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.configurationModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnHistoryRefresh = new System.Windows.Forms.Button();
            this.dtgHistoryImportCounts = new System.Windows.Forms.DataGridView();
            this.dtgHistorySteps = new System.Windows.Forms.DataGridView();
            this.ProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stepNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.historyStepBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dtgHistory = new System.Windows.Forms.DataGridView();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startDateTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endDateTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.historyModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnSaveSQLTokenRepl = new System.Windows.Forms.Button();
            this.dtgSQLTokenReplacement = new System.Windows.Forms.DataGridView();
            this.oldValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sQLTokenReplacementBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tmrHistoryGrid = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgConfiguration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.configurationModelBindingSource)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistoryImportCounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistorySteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.historyStepBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.historyModelBindingSource)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSQLTokenReplacement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sQLTokenReplacementBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1334, 817);
            this.tabControl1.TabIndex = 37;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSequenceDown);
            this.tabPage1.Controls.Add(this.btnAddProject);
            this.tabPage1.Controls.Add(this.txtNewProjectName);
            this.tabPage1.Controls.Add(this.lstProjectSequence);
            this.tabPage1.Controls.Add(this.btnApplySequenceUpdate);
            this.tabPage1.Controls.Add(this.btnSequenceUp);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage1.Size = new System.Drawing.Size(1326, 779);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Project Sequence";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSequenceDown
            // 
            this.btnSequenceDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSequenceDown.Location = new System.Drawing.Point(196, 663);
            this.btnSequenceDown.Margin = new System.Windows.Forms.Padding(6);
            this.btnSequenceDown.Name = "btnSequenceDown";
            this.btnSequenceDown.Size = new System.Drawing.Size(150, 44);
            this.btnSequenceDown.TabIndex = 39;
            this.btnSequenceDown.Text = "Move Down";
            this.btnSequenceDown.UseVisualStyleBackColor = true;
            this.btnSequenceDown.Click += new System.EventHandler(this.btnSequenceDown_Click);
            // 
            // btnAddProject
            // 
            this.btnAddProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddProject.Enabled = false;
            this.btnAddProject.Location = new System.Drawing.Point(702, 92);
            this.btnAddProject.Margin = new System.Windows.Forms.Padding(6);
            this.btnAddProject.Name = "btnAddProject";
            this.btnAddProject.Size = new System.Drawing.Size(236, 44);
            this.btnAddProject.TabIndex = 2;
            this.btnAddProject.Text = "Add New Project";
            this.btnAddProject.UseVisualStyleBackColor = true;
            this.btnAddProject.Click += new System.EventHandler(this.btnAddProject_Click);
            // 
            // txtNewProjectName
            // 
            this.txtNewProjectName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewProjectName.Location = new System.Drawing.Point(702, 42);
            this.txtNewProjectName.Margin = new System.Windows.Forms.Padding(6);
            this.txtNewProjectName.Name = "txtNewProjectName";
            this.txtNewProjectName.Size = new System.Drawing.Size(324, 31);
            this.txtNewProjectName.TabIndex = 1;
            this.txtNewProjectName.TextChanged += new System.EventHandler(this.txtNewProjectName_TextChanged);
            // 
            // lstProjectSequence
            // 
            this.lstProjectSequence.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstProjectSequence.FormattingEnabled = true;
            this.lstProjectSequence.ItemHeight = 25;
            this.lstProjectSequence.Location = new System.Drawing.Point(6, 6);
            this.lstProjectSequence.Margin = new System.Windows.Forms.Padding(6);
            this.lstProjectSequence.Name = "lstProjectSequence";
            this.lstProjectSequence.Size = new System.Drawing.Size(680, 629);
            this.lstProjectSequence.TabIndex = 3;
            // 
            // btnApplySequenceUpdate
            // 
            this.btnApplySequenceUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApplySequenceUpdate.Enabled = false;
            this.btnApplySequenceUpdate.Location = new System.Drawing.Point(460, 663);
            this.btnApplySequenceUpdate.Margin = new System.Windows.Forms.Padding(6);
            this.btnApplySequenceUpdate.Name = "btnApplySequenceUpdate";
            this.btnApplySequenceUpdate.Size = new System.Drawing.Size(206, 44);
            this.btnApplySequenceUpdate.TabIndex = 37;
            this.btnApplySequenceUpdate.Text = "Apply Updates";
            this.btnApplySequenceUpdate.UseVisualStyleBackColor = true;
            this.btnApplySequenceUpdate.Click += new System.EventHandler(this.btnApplySequenceUpdate_Click);
            // 
            // btnSequenceUp
            // 
            this.btnSequenceUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSequenceUp.Location = new System.Drawing.Point(34, 663);
            this.btnSequenceUp.Margin = new System.Windows.Forms.Padding(6);
            this.btnSequenceUp.Name = "btnSequenceUp";
            this.btnSequenceUp.Size = new System.Drawing.Size(150, 44);
            this.btnSequenceUp.TabIndex = 38;
            this.btnSequenceUp.Text = "Move Up";
            this.btnSequenceUp.UseVisualStyleBackColor = true;
            this.btnSequenceUp.Click += new System.EventHandler(this.btnSequenceUp_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkPreDeleteData);
            this.tabPage2.Controls.Add(this.chkDefaultActionUpdateAll);
            this.tabPage2.Controls.Add(this.cmbTimeFrameType);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txtTimeFrame);
            this.tabPage2.Controls.Add(this.lblCurrentStep);
            this.tabPage2.Controls.Add(this.lblCrntProject);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.btnRun);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage2.Size = new System.Drawing.Size(1326, 779);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Test Run Solution";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkPreDeleteData
            // 
            this.chkPreDeleteData.AutoSize = true;
            this.chkPreDeleteData.Location = new System.Drawing.Point(200, 130);
            this.chkPreDeleteData.Name = "chkPreDeleteData";
            this.chkPreDeleteData.Size = new System.Drawing.Size(486, 29);
            this.chkPreDeleteData.TabIndex = 46;
            this.chkPreDeleteData.Text = "Delete Records For Timeframe Before Test Run";
            this.chkPreDeleteData.UseVisualStyleBackColor = true;
            // 
            // chkDefaultActionUpdateAll
            // 
            this.chkDefaultActionUpdateAll.AutoSize = true;
            this.chkDefaultActionUpdateAll.Location = new System.Drawing.Point(200, 95);
            this.chkDefaultActionUpdateAll.Name = "chkDefaultActionUpdateAll";
            this.chkDefaultActionUpdateAll.Size = new System.Drawing.Size(326, 29);
            this.chkDefaultActionUpdateAll.TabIndex = 45;
            this.chkDefaultActionUpdateAll.Text = "Default Action Update Records";
            this.chkDefaultActionUpdateAll.UseVisualStyleBackColor = true;
            // 
            // cmbTimeFrameType
            // 
            this.cmbTimeFrameType.AutoCompleteCustomSource.AddRange(new string[] {
            "Default",
            "Days",
            "Hours",
            "Minutes"});
            this.cmbTimeFrameType.FormattingEnabled = true;
            this.cmbTimeFrameType.Items.AddRange(new object[] {
            "Default",
            "Minutes",
            "Days",
            "Date"});
            this.cmbTimeFrameType.Location = new System.Drawing.Point(200, 46);
            this.cmbTimeFrameType.Margin = new System.Windows.Forms.Padding(6);
            this.cmbTimeFrameType.Name = "cmbTimeFrameType";
            this.cmbTimeFrameType.Size = new System.Drawing.Size(238, 33);
            this.cmbTimeFrameType.TabIndex = 44;
            this.cmbTimeFrameType.Text = "Minutes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 54);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 25);
            this.label4.TabIndex = 43;
            this.label4.Text = "Time Frame";
            // 
            // txtTimeFrame
            // 
            this.txtTimeFrame.Location = new System.Drawing.Point(456, 46);
            this.txtTimeFrame.Margin = new System.Windows.Forms.Padding(6);
            this.txtTimeFrame.Name = "txtTimeFrame";
            this.txtTimeFrame.Size = new System.Drawing.Size(196, 31);
            this.txtTimeFrame.TabIndex = 42;
            this.txtTimeFrame.Text = "30";
            // 
            // lblCurrentStep
            // 
            this.lblCurrentStep.AutoSize = true;
            this.lblCurrentStep.Location = new System.Drawing.Point(317, 243);
            this.lblCurrentStep.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCurrentStep.Name = "lblCurrentStep";
            this.lblCurrentStep.Size = new System.Drawing.Size(0, 25);
            this.lblCurrentStep.TabIndex = 4;
            // 
            // lblCrntProject
            // 
            this.lblCrntProject.AutoSize = true;
            this.lblCrntProject.Location = new System.Drawing.Point(317, 197);
            this.lblCrntProject.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCrntProject.Name = "lblCrntProject";
            this.lblCrntProject.Size = new System.Drawing.Size(0, 25);
            this.lblCrntProject.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(167, 243);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Current Step:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(149, 197);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Current Project:";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(668, 40);
            this.btnRun.Margin = new System.Windows.Forms.Padding(6);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(150, 44);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Test Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnSaveConfigChanges);
            this.tabPage3.Controls.Add(this.dtgConfiguration);
            this.tabPage3.Location = new System.Drawing.Point(4, 34);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage3.Size = new System.Drawing.Size(1326, 779);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Configuration";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnSaveConfigChanges
            // 
            this.btnSaveConfigChanges.Location = new System.Drawing.Point(6, 6);
            this.btnSaveConfigChanges.Margin = new System.Windows.Forms.Padding(6);
            this.btnSaveConfigChanges.Name = "btnSaveConfigChanges";
            this.btnSaveConfigChanges.Size = new System.Drawing.Size(73, 33);
            this.btnSaveConfigChanges.TabIndex = 1;
            this.btnSaveConfigChanges.Text = "Save";
            this.btnSaveConfigChanges.UseVisualStyleBackColor = true;
            this.btnSaveConfigChanges.Click += new System.EventHandler(this.btnSaveConfigChanges_Click);
            // 
            // dtgConfiguration
            // 
            this.dtgConfiguration.AutoGenerateColumns = false;
            this.dtgConfiguration.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgConfiguration.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Name,
            this.valueDataGridViewTextBoxColumn,
            this.createDateDataGridViewTextBoxColumn1,
            this.ModifiedDate});
            this.dtgConfiguration.DataSource = this.configurationModelBindingSource;
            this.dtgConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgConfiguration.Location = new System.Drawing.Point(6, 6);
            this.dtgConfiguration.Margin = new System.Windows.Forms.Padding(6);
            this.dtgConfiguration.Name = "dtgConfiguration";
            this.dtgConfiguration.Size = new System.Drawing.Size(1314, 767);
            this.dtgConfiguration.TabIndex = 0;
            // 
            // Name
            // 
            this.Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Name.DataPropertyName = "Name";
            this.Name.HeaderText = "-    Name";
            this.Name.Name = "Name";
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            // 
            // createDateDataGridViewTextBoxColumn1
            // 
            this.createDateDataGridViewTextBoxColumn1.DataPropertyName = "CreateDate";
            this.createDateDataGridViewTextBoxColumn1.HeaderText = "CreateDate";
            this.createDateDataGridViewTextBoxColumn1.Name = "createDateDataGridViewTextBoxColumn1";
            this.createDateDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // ModifiedDate
            // 
            this.ModifiedDate.DataPropertyName = "ModifiedDate";
            this.ModifiedDate.HeaderText = "ModifiedDate";
            this.ModifiedDate.Name = "ModifiedDate";
            // 
            // configurationModelBindingSource
            // 
            this.configurationModelBindingSource.DataSource = typeof(Harvest.Bridge.Common.Models.ConfigurationModel);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnHistoryRefresh);
            this.tabPage4.Controls.Add(this.dtgHistoryImportCounts);
            this.tabPage4.Controls.Add(this.dtgHistorySteps);
            this.tabPage4.Controls.Add(this.dtgHistory);
            this.tabPage4.Location = new System.Drawing.Point(4, 34);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1326, 779);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Run History";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnHistoryRefresh
            // 
            this.btnHistoryRefresh.Location = new System.Drawing.Point(6, 6);
            this.btnHistoryRefresh.Margin = new System.Windows.Forms.Padding(6);
            this.btnHistoryRefresh.Name = "btnHistoryRefresh";
            this.btnHistoryRefresh.Size = new System.Drawing.Size(106, 44);
            this.btnHistoryRefresh.TabIndex = 3;
            this.btnHistoryRefresh.Text = "Refresh";
            this.btnHistoryRefresh.UseVisualStyleBackColor = true;
            this.btnHistoryRefresh.Click += new System.EventHandler(this.btnHistoryRefresh_Click);
            // 
            // dtgHistoryImportCounts
            // 
            this.dtgHistoryImportCounts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgHistoryImportCounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgHistoryImportCounts.Location = new System.Drawing.Point(6, 592);
            this.dtgHistoryImportCounts.Margin = new System.Windows.Forms.Padding(6);
            this.dtgHistoryImportCounts.Name = "dtgHistoryImportCounts";
            this.dtgHistoryImportCounts.Size = new System.Drawing.Size(1306, 183);
            this.dtgHistoryImportCounts.TabIndex = 2;
            // 
            // dtgHistorySteps
            // 
            this.dtgHistorySteps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgHistorySteps.AutoGenerateColumns = false;
            this.dtgHistorySteps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgHistorySteps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProjectName,
            this.stepNameDataGridViewTextBoxColumn,
            this.LogLevel,
            this.messageDataGridViewTextBoxColumn,
            this.createDateDataGridViewTextBoxColumn});
            this.dtgHistorySteps.DataSource = this.historyStepBindingSource;
            this.dtgHistorySteps.Location = new System.Drawing.Point(6, 221);
            this.dtgHistorySteps.Margin = new System.Windows.Forms.Padding(6);
            this.dtgHistorySteps.Name = "dtgHistorySteps";
            this.dtgHistorySteps.Size = new System.Drawing.Size(1306, 360);
            this.dtgHistorySteps.TabIndex = 1;
            // 
            // ProjectName
            // 
            this.ProjectName.DataPropertyName = "ProjectName";
            this.ProjectName.HeaderText = "ProjectName";
            this.ProjectName.Name = "ProjectName";
            // 
            // stepNameDataGridViewTextBoxColumn
            // 
            this.stepNameDataGridViewTextBoxColumn.DataPropertyName = "StepName";
            this.stepNameDataGridViewTextBoxColumn.HeaderText = "StepName";
            this.stepNameDataGridViewTextBoxColumn.Name = "stepNameDataGridViewTextBoxColumn";
            // 
            // LogLevel
            // 
            this.LogLevel.DataPropertyName = "LogLevel";
            this.LogLevel.HeaderText = "LogLevel";
            this.LogLevel.Name = "LogLevel";
            // 
            // messageDataGridViewTextBoxColumn
            // 
            this.messageDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.messageDataGridViewTextBoxColumn.DataPropertyName = "Message";
            this.messageDataGridViewTextBoxColumn.HeaderText = "Message";
            this.messageDataGridViewTextBoxColumn.Name = "messageDataGridViewTextBoxColumn";
            // 
            // createDateDataGridViewTextBoxColumn
            // 
            this.createDateDataGridViewTextBoxColumn.DataPropertyName = "CreateDate";
            this.createDateDataGridViewTextBoxColumn.HeaderText = "CreateDate";
            this.createDateDataGridViewTextBoxColumn.Name = "createDateDataGridViewTextBoxColumn";
            // 
            // historyStepBindingSource
            // 
            this.historyStepBindingSource.DataSource = typeof(Harvest.Bridge.Common.Models.History.HistoryStepModel);
            // 
            // dtgHistory
            // 
            this.dtgHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgHistory.AutoGenerateColumns = false;
            this.dtgHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.descriptionDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.startDateTimeDataGridViewTextBoxColumn,
            this.endDateTimeDataGridViewTextBoxColumn});
            this.dtgHistory.DataSource = this.historyModelBindingSource;
            this.dtgHistory.Location = new System.Drawing.Point(6, 6);
            this.dtgHistory.Margin = new System.Windows.Forms.Padding(6);
            this.dtgHistory.Name = "dtgHistory";
            this.dtgHistory.Size = new System.Drawing.Size(1306, 204);
            this.dtgHistory.TabIndex = 0;
            this.dtgHistory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgHistory_CellContentClick);
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "   Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            // 
            // startDateTimeDataGridViewTextBoxColumn
            // 
            this.startDateTimeDataGridViewTextBoxColumn.DataPropertyName = "StartDateTime";
            this.startDateTimeDataGridViewTextBoxColumn.HeaderText = "StartDateTime";
            this.startDateTimeDataGridViewTextBoxColumn.Name = "startDateTimeDataGridViewTextBoxColumn";
            // 
            // endDateTimeDataGridViewTextBoxColumn
            // 
            this.endDateTimeDataGridViewTextBoxColumn.DataPropertyName = "EndDateTime";
            this.endDateTimeDataGridViewTextBoxColumn.HeaderText = "EndDateTime";
            this.endDateTimeDataGridViewTextBoxColumn.Name = "endDateTimeDataGridViewTextBoxColumn";
            // 
            // historyModelBindingSource
            // 
            this.historyModelBindingSource.DataSource = typeof(Harvest.Bridge.Common.Models.History.HistoryModel);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.btnSaveSQLTokenRepl);
            this.tabPage5.Controls.Add(this.dtgSQLTokenReplacement);
            this.tabPage5.Location = new System.Drawing.Point(4, 34);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1326, 779);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "SQL Token Replacement";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btnSaveSQLTokenRepl
            // 
            this.btnSaveSQLTokenRepl.Location = new System.Drawing.Point(6, 642);
            this.btnSaveSQLTokenRepl.Name = "btnSaveSQLTokenRepl";
            this.btnSaveSQLTokenRepl.Size = new System.Drawing.Size(118, 54);
            this.btnSaveSQLTokenRepl.TabIndex = 1;
            this.btnSaveSQLTokenRepl.Text = "Save";
            this.btnSaveSQLTokenRepl.UseVisualStyleBackColor = true;
            this.btnSaveSQLTokenRepl.Click += new System.EventHandler(this.btnSaveSQLTokenRepl_Click);
            // 
            // dtgSQLTokenReplacement
            // 
            this.dtgSQLTokenReplacement.AutoGenerateColumns = false;
            this.dtgSQLTokenReplacement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgSQLTokenReplacement.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.oldValueDataGridViewTextBoxColumn,
            this.newValueDataGridViewTextBoxColumn});
            this.dtgSQLTokenReplacement.DataSource = this.sQLTokenReplacementBindingSource;
            this.dtgSQLTokenReplacement.Location = new System.Drawing.Point(6, 3);
            this.dtgSQLTokenReplacement.Name = "dtgSQLTokenReplacement";
            this.dtgSQLTokenReplacement.RowTemplate.Height = 33;
            this.dtgSQLTokenReplacement.Size = new System.Drawing.Size(1314, 633);
            this.dtgSQLTokenReplacement.TabIndex = 0;
            // 
            // oldValueDataGridViewTextBoxColumn
            // 
            this.oldValueDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.oldValueDataGridViewTextBoxColumn.DataPropertyName = "OldValue";
            this.oldValueDataGridViewTextBoxColumn.HeaderText = "Old Value";
            this.oldValueDataGridViewTextBoxColumn.Name = "oldValueDataGridViewTextBoxColumn";
            // 
            // newValueDataGridViewTextBoxColumn
            // 
            this.newValueDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.newValueDataGridViewTextBoxColumn.DataPropertyName = "NewValue";
            this.newValueDataGridViewTextBoxColumn.HeaderText = "New Value";
            this.newValueDataGridViewTextBoxColumn.Name = "newValueDataGridViewTextBoxColumn";
            // 
            // sQLTokenReplacementBindingSource
            // 
            this.sQLTokenReplacementBindingSource.DataSource = typeof(Harvest.Bridge.Common.Models.SQLTokenReplacementModel);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(766, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "New Project";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // tmrHistoryGrid
            // 
            this.tmrHistoryGrid.Interval = 5000;
            this.tmrHistoryGrid.Tick += new System.EventHandler(this.tmrHistoryGrid_Tick);
            // 
            // ctrlSolutionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Size = new System.Drawing.Size(1334, 817);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgConfiguration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.configurationModelBindingSource)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistoryImportCounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistorySteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.historyStepBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.historyModelBindingSource)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgSQLTokenReplacement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sQLTokenReplacementBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnSequenceDown;
        private System.Windows.Forms.Button btnAddProject;
        private System.Windows.Forms.TextBox txtNewProjectName;
        private System.Windows.Forms.ListBox lstProjectSequence;
        private System.Windows.Forms.Button btnApplySequenceUpdate;
        private System.Windows.Forms.Button btnSequenceUp;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblCurrentStep;
        private System.Windows.Forms.Label lblCrntProject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dtgConfiguration;
        private System.Windows.Forms.Button btnSaveConfigChanges;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dtgHistoryImportCounts;
        private System.Windows.Forms.DataGridView dtgHistorySteps;
        private System.Windows.Forms.DataGridView dtgHistory;
        private System.Windows.Forms.BindingSource historyModelBindingSource;
        private System.Windows.Forms.BindingSource historyStepBindingSource;
        private System.Windows.Forms.Button btnHistoryRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startDateTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endDateTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.Timer tmrHistoryGrid;
        private System.Windows.Forms.ComboBox cmbTimeFrameType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTimeFrame;
        private System.Windows.Forms.BindingSource configurationModelBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn stepNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createDateDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedDate;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnSaveSQLTokenRepl;
        private System.Windows.Forms.DataGridView dtgSQLTokenReplacement;
        private System.Windows.Forms.DataGridViewTextBoxColumn oldValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn newValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource sQLTokenReplacementBindingSource;
        private System.Windows.Forms.CheckBox chkDefaultActionUpdateAll;
        private System.Windows.Forms.CheckBox chkPreDeleteData;
    }
}
