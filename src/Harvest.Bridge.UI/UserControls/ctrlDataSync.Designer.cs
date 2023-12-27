namespace Harvest.Bridge.UI.UserControls
{
    partial class ctrlDataSync
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
            this.lblControlTitle = new System.Windows.Forms.Label();
            this.cmbSourceDB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTargetDB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtFK4Clmn = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFK3Clmn = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtFK2Clmn = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFK1Clmn = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEpochTimeClmn = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPrimaryKey = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nudBatchSize = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTablename = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dtgMapColumns = new System.Windows.Forms.DataGridView();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnDeleteRow = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtFK1ClmnTarget = new System.Windows.Forms.TextBox();
            this.txtFK2ClmnTarget = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtFK3ClmnTarget = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtFK4ClmnTarget = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBatchSize)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMapColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // lblControlTitle
            // 
            this.lblControlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblControlTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblControlTitle.Location = new System.Drawing.Point(0, 2);
            this.lblControlTitle.Name = "lblControlTitle";
            this.lblControlTitle.Size = new System.Drawing.Size(763, 19);
            this.lblControlTitle.TabIndex = 25;
            this.lblControlTitle.Text = "Data Sync";
            this.lblControlTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbSourceDB
            // 
            this.cmbSourceDB.FormattingEnabled = true;
            this.cmbSourceDB.Items.AddRange(new object[] {
            "SourceDB",
            "StagingDB",
            "TargetDB"});
            this.cmbSourceDB.Location = new System.Drawing.Point(72, 47);
            this.cmbSourceDB.Name = "cmbSourceDB";
            this.cmbSourceDB.Size = new System.Drawing.Size(239, 21);
            this.cmbSourceDB.TabIndex = 29;
            this.cmbSourceDB.DropDown += new System.EventHandler(this.cmbSourceDB_DropDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Source DB:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(72, 24);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(550, 20);
            this.txtName.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Name:";
            // 
            // cmbTargetDB
            // 
            this.cmbTargetDB.FormattingEnabled = true;
            this.cmbTargetDB.Items.AddRange(new object[] {
            "SourceDB",
            "StagingDB",
            "TargetDB"});
            this.cmbTargetDB.Location = new System.Drawing.Point(383, 47);
            this.cmbTargetDB.Name = "cmbTargetDB";
            this.cmbTargetDB.Size = new System.Drawing.Size(239, 21);
            this.cmbTargetDB.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(314, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Target DB:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(0, 430);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtDescription.Size = new System.Drawing.Size(760, 78);
            this.txtDescription.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 414);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Description";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(5, 73);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(755, 338);
            this.tabControl1.TabIndex = 34;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtFK4ClmnTarget);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.txtFK3ClmnTarget);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.txtFK2ClmnTarget);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.txtFK1ClmnTarget);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.txtFK4Clmn);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.txtFK3Clmn);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.txtFK2Clmn);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.txtFK1Clmn);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.txtEpochTimeClmn);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.txtPrimaryKey);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.nudBatchSize);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.txtTablename);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(747, 312);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtFK4Clmn
            // 
            this.txtFK4Clmn.Location = new System.Drawing.Point(160, 228);
            this.txtFK4Clmn.Name = "txtFK4Clmn";
            this.txtFK4Clmn.Size = new System.Drawing.Size(163, 20);
            this.txtFK4Clmn.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 231);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(147, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Foreign Key 4 Source Column";
            // 
            // txtFK3Clmn
            // 
            this.txtFK3Clmn.Location = new System.Drawing.Point(160, 202);
            this.txtFK3Clmn.Name = "txtFK3Clmn";
            this.txtFK3Clmn.Size = new System.Drawing.Size(163, 20);
            this.txtFK3Clmn.TabIndex = 13;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 205);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(147, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Foreign Key 3 Source Column";
            // 
            // txtFK2Clmn
            // 
            this.txtFK2Clmn.Location = new System.Drawing.Point(162, 174);
            this.txtFK2Clmn.Name = "txtFK2Clmn";
            this.txtFK2Clmn.Size = new System.Drawing.Size(163, 20);
            this.txtFK2Clmn.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 177);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(147, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Foreign Key 2 Source Column";
            // 
            // txtFK1Clmn
            // 
            this.txtFK1Clmn.Location = new System.Drawing.Point(162, 148);
            this.txtFK1Clmn.Name = "txtFK1Clmn";
            this.txtFK1Clmn.Size = new System.Drawing.Size(163, 20);
            this.txtFK1Clmn.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 151);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(147, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Foreign Key 1 Source Column";
            // 
            // txtEpochTimeClmn
            // 
            this.txtEpochTimeClmn.Location = new System.Drawing.Point(111, 102);
            this.txtEpochTimeClmn.Name = "txtEpochTimeClmn";
            this.txtEpochTimeClmn.Size = new System.Drawing.Size(163, 20);
            this.txtEpochTimeClmn.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Epoch Time Column";
            // 
            // txtPrimaryKey
            // 
            this.txtPrimaryKey.Location = new System.Drawing.Point(111, 76);
            this.txtPrimaryKey.Name = "txtPrimaryKey";
            this.txtPrimaryKey.Size = new System.Drawing.Size(163, 20);
            this.txtPrimaryKey.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Primary Key Column";
            // 
            // nudBatchSize
            // 
            this.nudBatchSize.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudBatchSize.Location = new System.Drawing.Point(80, 33);
            this.nudBatchSize.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudBatchSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBatchSize.Name = "nudBatchSize";
            this.nudBatchSize.Size = new System.Drawing.Size(194, 20);
            this.nudBatchSize.TabIndex = 3;
            this.nudBatchSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Batch Size:";
            // 
            // txtTablename
            // 
            this.txtTablename.Location = new System.Drawing.Point(80, 7);
            this.txtTablename.Name = "txtTablename";
            this.txtTablename.Size = new System.Drawing.Size(194, 20);
            this.txtTablename.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "TableName:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDeleteRow);
            this.tabPage2.Controls.Add(this.btnAddRow);
            this.tabPage2.Controls.Add(this.dtgMapColumns);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(747, 312);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Column Mapping";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dtgMapColumns
            // 
            this.dtgMapColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgMapColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgMapColumns.Location = new System.Drawing.Point(3, 24);
            this.dtgMapColumns.MultiSelect = false;
            this.dtgMapColumns.Name = "dtgMapColumns";
            this.dtgMapColumns.Size = new System.Drawing.Size(741, 282);
            this.dtgMapColumns.TabIndex = 1;
            this.dtgMapColumns.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgMapColumns_RowEnter);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(6, 0);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(75, 23);
            this.btnAddRow.TabIndex = 2;
            this.btnAddRow.Text = "Add Row";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Location = new System.Drawing.Point(87, 0);
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteRow.TabIndex = 9;
            this.btnDeleteRow.Text = "Delete Row";
            this.btnDeleteRow.UseVisualStyleBackColor = true;
            this.btnDeleteRow.Click += new System.EventHandler(this.btnDeleteRow_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(342, 151);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "Target";
            // 
            // txtFK1ClmnTarget
            // 
            this.txtFK1ClmnTarget.Location = new System.Drawing.Point(386, 148);
            this.txtFK1ClmnTarget.Name = "txtFK1ClmnTarget";
            this.txtFK1ClmnTarget.Size = new System.Drawing.Size(163, 20);
            this.txtFK1ClmnTarget.TabIndex = 17;
            // 
            // txtFK2ClmnTarget
            // 
            this.txtFK2ClmnTarget.Location = new System.Drawing.Point(386, 177);
            this.txtFK2ClmnTarget.Name = "txtFK2ClmnTarget";
            this.txtFK2ClmnTarget.Size = new System.Drawing.Size(163, 20);
            this.txtFK2ClmnTarget.TabIndex = 19;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(342, 180);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "Target";
            // 
            // txtFK3ClmnTarget
            // 
            this.txtFK3ClmnTarget.Location = new System.Drawing.Point(386, 205);
            this.txtFK3ClmnTarget.Name = "txtFK3ClmnTarget";
            this.txtFK3ClmnTarget.Size = new System.Drawing.Size(163, 20);
            this.txtFK3ClmnTarget.TabIndex = 21;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(342, 208);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 13);
            this.label15.TabIndex = 20;
            this.label15.Text = "Target";
            // 
            // txtFK4ClmnTarget
            // 
            this.txtFK4ClmnTarget.Location = new System.Drawing.Point(386, 228);
            this.txtFK4ClmnTarget.Name = "txtFK4ClmnTarget";
            this.txtFK4ClmnTarget.Size = new System.Drawing.Size(163, 20);
            this.txtFK4ClmnTarget.TabIndex = 23;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(342, 231);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(38, 13);
            this.label16.TabIndex = 22;
            this.label16.Text = "Target";
            // 
            // ctrlDataSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbTargetDB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbSourceDB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblControlTitle);
            this.Name = "ctrlDataSync";
            this.Size = new System.Drawing.Size(763, 508);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBatchSize)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgMapColumns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblControlTitle;
        private System.Windows.Forms.ComboBox cmbSourceDB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTargetDB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dtgMapColumns;
        private System.Windows.Forms.NumericUpDown nudBatchSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTablename;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFK4Clmn;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtFK3Clmn;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtFK2Clmn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFK1Clmn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtEpochTimeClmn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPrimaryKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnDeleteRow;
        private System.Windows.Forms.TextBox txtFK4ClmnTarget;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtFK3ClmnTarget;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtFK2ClmnTarget;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtFK1ClmnTarget;
        private System.Windows.Forms.Label label13;
    }
}
