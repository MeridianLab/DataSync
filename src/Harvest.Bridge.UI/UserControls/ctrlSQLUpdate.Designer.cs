namespace Harvest.Bridge.UI.UserControls
{
    partial class ctrlSQLUpdate
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
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSQL = new System.Windows.Forms.TextBox();
            this.lblControlTitle = new System.Windows.Forms.Label();
            this.cmbTargetDB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSourceDB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(1, 333);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtDescription.Size = new System.Drawing.Size(703, 78);
            this.txtDescription.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 317);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Description";
            // 
            // txtSQL
            // 
            this.txtSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSQL.Location = new System.Drawing.Point(1, 68);
            this.txtSQL.Multiline = true;
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSQL.Size = new System.Drawing.Size(703, 246);
            this.txtSQL.TabIndex = 15;
            // 
            // lblControlTitle
            // 
            this.lblControlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblControlTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblControlTitle.Location = new System.Drawing.Point(3, 1);
            this.lblControlTitle.Name = "lblControlTitle";
            this.lblControlTitle.Size = new System.Drawing.Size(704, 19);
            this.lblControlTitle.TabIndex = 16;
            this.lblControlTitle.Text = "SQL Update";
            this.lblControlTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbTargetDB
            // 
            this.cmbTargetDB.FormattingEnabled = true;
            this.cmbTargetDB.Items.AddRange(new object[] {
            "SourceDB",
            "StagingDB",
            "TargetDB"});
            this.cmbTargetDB.Location = new System.Drawing.Point(383, 46);
            this.cmbTargetDB.Name = "cmbTargetDB";
            this.cmbTargetDB.Size = new System.Drawing.Size(239, 21);
            this.cmbTargetDB.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Target DB:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbSourceDB
            // 
            this.cmbSourceDB.FormattingEnabled = true;
            this.cmbSourceDB.Items.AddRange(new object[] {
            "SourceDB",
            "StagingDB",
            "TargetDB"});
            this.cmbSourceDB.Location = new System.Drawing.Point(72, 46);
            this.cmbSourceDB.Name = "cmbSourceDB";
            this.cmbSourceDB.Size = new System.Drawing.Size(239, 21);
            this.cmbSourceDB.TabIndex = 35;
            this.cmbSourceDB.DropDown += new System.EventHandler(this.cmbSourceDB_DropDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Source DB:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(121, 23);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(501, 20);
            this.txtName.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(77, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Name:";
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(6, 25);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkEnabled.TabIndex = 38;
            this.chkEnabled.Text = "Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // ctrlSQLUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.cmbTargetDB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSourceDB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblControlTitle);
            this.Controls.Add(this.txtSQL);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label2);
            this.Name = "ctrlSQLUpdate";
            this.Size = new System.Drawing.Size(707, 411);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSQL;
        private System.Windows.Forms.Label lblControlTitle;
        private System.Windows.Forms.ComboBox cmbTargetDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSourceDB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkEnabled;
    }
}
