namespace Harvest.Bridge.UI.UserControls
{
    partial class ctrlVariable
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdValueType = new System.Windows.Forms.ComboBox();
            this.txtValueStr = new System.Windows.Forms.TextBox();
            this.txtInputSQL = new System.Windows.Forms.TextBox();
            this.lblControlTitle = new System.Windows.Forms.Label();
            this.cmbValueType = new System.Windows.Forms.ComboBox();
            this.lblValueType = new System.Windows.Forms.Label();
            this.cmbSourceDB = new System.Windows.Forms.ComboBox();
            this.lblSourceDB = new System.Windows.Forms.Label();
            this.btnValidate = new System.Windows.Forms.Button();
            this.lblTestResultValue = new System.Windows.Forms.Label();
            this.txtTestResultValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(84, 25);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(403, 20);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 250);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(0, 266);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtDescription.Size = new System.Drawing.Size(558, 78);
            this.txtDescription.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Source Type:";
            // 
            // cmdValueType
            // 
            this.cmdValueType.FormattingEnabled = true;
            this.cmdValueType.Items.AddRange(new object[] {
            "GenerateId",
            "Value",
            "SQL"});
            this.cmdValueType.Location = new System.Drawing.Point(84, 49);
            this.cmdValueType.Name = "cmdValueType";
            this.cmdValueType.Size = new System.Drawing.Size(164, 21);
            this.cmdValueType.TabIndex = 5;
            this.cmdValueType.SelectedIndexChanged += new System.EventHandler(this.cmdValueType_SelectedIndexChanged);
            // 
            // txtValueStr
            // 
            this.txtValueStr.Location = new System.Drawing.Point(84, 101);
            this.txtValueStr.Name = "txtValueStr";
            this.txtValueStr.Size = new System.Drawing.Size(164, 20);
            this.txtValueStr.TabIndex = 7;
            this.txtValueStr.Visible = false;
            // 
            // txtInputSQL
            // 
            this.txtInputSQL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInputSQL.Location = new System.Drawing.Point(84, 101);
            this.txtInputSQL.Multiline = true;
            this.txtInputSQL.Name = "txtInputSQL";
            this.txtInputSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInputSQL.Size = new System.Drawing.Size(474, 65);
            this.txtInputSQL.TabIndex = 8;
            this.txtInputSQL.Visible = false;
            // 
            // lblControlTitle
            // 
            this.lblControlTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblControlTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblControlTitle.Location = new System.Drawing.Point(4, 2);
            this.lblControlTitle.Name = "lblControlTitle";
            this.lblControlTitle.Size = new System.Drawing.Size(554, 17);
            this.lblControlTitle.TabIndex = 10;
            this.lblControlTitle.Text = "Variable";
            this.lblControlTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbValueType
            // 
            this.cmbValueType.FormattingEnabled = true;
            this.cmbValueType.Items.AddRange(new object[] {
            "String",
            "Numeric",
            "Guid",
            "CommaDelimitedString"});
            this.cmbValueType.Location = new System.Drawing.Point(84, 74);
            this.cmbValueType.Name = "cmbValueType";
            this.cmbValueType.Size = new System.Drawing.Size(164, 21);
            this.cmbValueType.TabIndex = 12;
            this.cmbValueType.SelectedIndexChanged += new System.EventHandler(this.cmbValueType_SelectedIndexChanged);
            // 
            // lblValueType
            // 
            this.lblValueType.AutoSize = true;
            this.lblValueType.Location = new System.Drawing.Point(3, 77);
            this.lblValueType.Name = "lblValueType";
            this.lblValueType.Size = new System.Drawing.Size(64, 13);
            this.lblValueType.TabIndex = 11;
            this.lblValueType.Text = "Value Type:";
            // 
            // cmbSourceDB
            // 
            this.cmbSourceDB.FormattingEnabled = true;
            this.cmbSourceDB.Items.AddRange(new object[] {
            "SourceDB",
            "StagingDB",
            "TargetDB"});
            this.cmbSourceDB.Location = new System.Drawing.Point(323, 49);
            this.cmbSourceDB.Name = "cmbSourceDB";
            this.cmbSourceDB.Size = new System.Drawing.Size(164, 21);
            this.cmbSourceDB.TabIndex = 27;
            this.cmbSourceDB.Visible = false;
            // 
            // lblSourceDB
            // 
            this.lblSourceDB.AutoSize = true;
            this.lblSourceDB.Location = new System.Drawing.Point(254, 52);
            this.lblSourceDB.Name = "lblSourceDB";
            this.lblSourceDB.Size = new System.Drawing.Size(62, 13);
            this.lblSourceDB.TabIndex = 26;
            this.lblSourceDB.Text = "Source DB:";
            this.lblSourceDB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSourceDB.Visible = false;
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(4, 143);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(75, 23);
            this.btnValidate.TabIndex = 28;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // lblTestResultValue
            // 
            this.lblTestResultValue.AutoSize = true;
            this.lblTestResultValue.Location = new System.Drawing.Point(12, 191);
            this.lblTestResultValue.Name = "lblTestResultValue";
            this.lblTestResultValue.Size = new System.Drawing.Size(91, 13);
            this.lblTestResultValue.TabIndex = 29;
            this.lblTestResultValue.Text = "Test Result Value";
            this.lblTestResultValue.Visible = false;
            // 
            // txtTestResultValue
            // 
            this.txtTestResultValue.Location = new System.Drawing.Point(109, 191);
            this.txtTestResultValue.Multiline = true;
            this.txtTestResultValue.Name = "txtTestResultValue";
            this.txtTestResultValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTestResultValue.Size = new System.Drawing.Size(449, 58);
            this.txtTestResultValue.TabIndex = 30;
            this.txtTestResultValue.Visible = false;
            // 
            // ctrlVariable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtTestResultValue);
            this.Controls.Add(this.lblTestResultValue);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.cmbSourceDB);
            this.Controls.Add(this.lblSourceDB);
            this.Controls.Add(this.cmbValueType);
            this.Controls.Add(this.lblValueType);
            this.Controls.Add(this.lblControlTitle);
            this.Controls.Add(this.txtInputSQL);
            this.Controls.Add(this.txtValueStr);
            this.Controls.Add(this.cmdValueType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Name = "ctrlVariable";
            this.Size = new System.Drawing.Size(564, 349);
            this.Load += new System.EventHandler(this.ctrlVariable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmdValueType;
        private System.Windows.Forms.TextBox txtValueStr;
        private System.Windows.Forms.TextBox txtInputSQL;
        private System.Windows.Forms.Label lblControlTitle;
        private System.Windows.Forms.ComboBox cmbValueType;
        private System.Windows.Forms.Label lblValueType;
        private System.Windows.Forms.ComboBox cmbSourceDB;
        private System.Windows.Forms.Label lblSourceDB;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.Label lblTestResultValue;
        private System.Windows.Forms.TextBox txtTestResultValue;
    }
}
