namespace Harvest.Bridge.UI.UserControls
{
    partial class CtrlStepFlow
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
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.cmbResultAction = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSourceDB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblControlTitle = new System.Windows.Forms.Label();
            this.txtSQL = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbExpression = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(14, 28);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkEnabled.TabIndex = 52;
            this.chkEnabled.Text = "Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // cmbResultAction
            // 
            this.cmbResultAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbResultAction.FormattingEnabled = true;
            this.cmbResultAction.Items.AddRange(new object[] {
            "Stop Project, Moves To Next Project",
            "Stop Solution, Stop processing"});
            this.cmbResultAction.Location = new System.Drawing.Point(282, 234);
            this.cmbResultAction.Name = "cmbResultAction";
            this.cmbResultAction.Size = new System.Drawing.Size(184, 21);
            this.cmbResultAction.TabIndex = 49;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(245, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "then:";
            // 
            // cmbSourceDB
            // 
            this.cmbSourceDB.FormattingEnabled = true;
            this.cmbSourceDB.Items.AddRange(new object[] {
            "SourceDB",
            "StagingDB",
            "TargetDB"});
            this.cmbSourceDB.Location = new System.Drawing.Point(83, 49);
            this.cmbSourceDB.Name = "cmbSourceDB";
            this.cmbSourceDB.Size = new System.Drawing.Size(300, 21);
            this.cmbSourceDB.TabIndex = 47;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Source DB:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblControlTitle
            // 
            this.lblControlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblControlTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblControlTitle.Location = new System.Drawing.Point(5, 4);
            this.lblControlTitle.Name = "lblControlTitle";
            this.lblControlTitle.Size = new System.Drawing.Size(546, 17);
            this.lblControlTitle.TabIndex = 45;
            this.lblControlTitle.Text = "Step Flow";
            this.lblControlTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtSQL
            // 
            this.txtSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSQL.Location = new System.Drawing.Point(9, 76);
            this.txtSQL.Multiline = true;
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSQL.Size = new System.Drawing.Size(536, 152);
            this.txtSQL.TabIndex = 44;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(9, 303);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtDescription.Size = new System.Drawing.Size(536, 78);
            this.txtDescription.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 287);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Description";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(128, 26);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(255, 20);
            this.txtName.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 53;
            this.label5.Text = "If result is ";
            // 
            // cmbExpression
            // 
            this.cmbExpression.FormattingEnabled = true;
            this.cmbExpression.Items.AddRange(new object[] {
            "Record Count = 0",
            "Execute Scalar = 0",
            "Record Count > 0",
            "Execute Scalar > 0"});
            this.cmbExpression.Location = new System.Drawing.Point(78, 234);
            this.cmbExpression.Name = "cmbExpression";
            this.cmbExpression.Size = new System.Drawing.Size(161, 21);
            this.cmbExpression.TabIndex = 54;
            // 
            // CtrlStepFlow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbExpression);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.cmbResultAction);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbSourceDB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblControlTitle);
            this.Controls.Add(this.txtSQL);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Name = "CtrlStepFlow";
            this.Size = new System.Drawing.Size(556, 391);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.ComboBox cmbResultAction;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSourceDB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblControlTitle;
        private System.Windows.Forms.TextBox txtSQL;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbExpression;
    }
}
