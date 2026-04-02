namespace BankManagement.ManageAccounts_Transactions.Accounts
{
    partial class frmAddUpdateAccount
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
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.ctrlClientCardWithFilter1 = new BankManagement.Clients.Components.ctrlClientCardWithFilter();
            this.gbAccountInformation = new System.Windows.Forms.GroupBox();
            this.txtAccountNumber = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtConfirmPinCode = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtPinCode = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblAccountID = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2VSeparator1 = new Guna.UI2.WinForms.Guna2VSeparator();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbInActive = new System.Windows.Forms.RadioButton();
            this.rbActive = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.gbAccountInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Navy;
            this.lblTitle.Location = new System.Drawing.Point(222, -4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(311, 44);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "Add New Account";
            // 
            // ctrlClientCardWithFilter1
            // 
            this.ctrlClientCardWithFilter1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ctrlClientCardWithFilter1.FilterEnabled = true;
            this.ctrlClientCardWithFilter1.Location = new System.Drawing.Point(7, 36);
            this.ctrlClientCardWithFilter1.Name = "ctrlClientCardWithFilter1";
            this.ctrlClientCardWithFilter1.ShowAddClient = true;
            this.ctrlClientCardWithFilter1.Size = new System.Drawing.Size(669, 396);
            this.ctrlClientCardWithFilter1.TabIndex = 10;
            // 
            // gbAccountInformation
            // 
            this.gbAccountInformation.Controls.Add(this.txtAccountNumber);
            this.gbAccountInformation.Controls.Add(this.txtConfirmPinCode);
            this.gbAccountInformation.Controls.Add(this.txtPinCode);
            this.gbAccountInformation.Controls.Add(this.lblAccountID);
            this.gbAccountInformation.Controls.Add(this.label4);
            this.gbAccountInformation.Controls.Add(this.label3);
            this.gbAccountInformation.Controls.Add(this.guna2VSeparator1);
            this.gbAccountInformation.Controls.Add(this.label2);
            this.gbAccountInformation.Controls.Add(this.label1);
            this.gbAccountInformation.Controls.Add(this.rbInActive);
            this.gbAccountInformation.Controls.Add(this.rbActive);
            this.gbAccountInformation.Controls.Add(this.label5);
            this.gbAccountInformation.Location = new System.Drawing.Point(19, 431);
            this.gbAccountInformation.Name = "gbAccountInformation";
            this.gbAccountInformation.Size = new System.Drawing.Size(652, 114);
            this.gbAccountInformation.TabIndex = 0;
            this.gbAccountInformation.TabStop = false;
            this.gbAccountInformation.Text = "Account Information";
            // 
            // txtAccountNumber
            // 
            this.txtAccountNumber.BorderRadius = 10;
            this.txtAccountNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAccountNumber.DefaultText = "";
            this.txtAccountNumber.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtAccountNumber.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtAccountNumber.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAccountNumber.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAccountNumber.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAccountNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAccountNumber.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAccountNumber.Location = new System.Drawing.Point(12, 68);
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.PlaceholderText = "Account Number";
            this.txtAccountNumber.SelectedText = "";
            this.txtAccountNumber.Size = new System.Drawing.Size(167, 27);
            this.txtAccountNumber.TabIndex = 163;
            // 
            // txtConfirmPinCode
            // 
            this.txtConfirmPinCode.BorderRadius = 10;
            this.txtConfirmPinCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtConfirmPinCode.DefaultText = "";
            this.txtConfirmPinCode.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtConfirmPinCode.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtConfirmPinCode.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtConfirmPinCode.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtConfirmPinCode.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtConfirmPinCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtConfirmPinCode.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtConfirmPinCode.Location = new System.Drawing.Point(343, 83);
            this.txtConfirmPinCode.Name = "txtConfirmPinCode";
            this.txtConfirmPinCode.PasswordChar = '*';
            this.txtConfirmPinCode.PlaceholderText = "Confirm Pin Code";
            this.txtConfirmPinCode.SelectedText = "";
            this.txtConfirmPinCode.Size = new System.Drawing.Size(167, 27);
            this.txtConfirmPinCode.TabIndex = 162;
            // 
            // txtPinCode
            // 
            this.txtPinCode.BorderRadius = 10;
            this.txtPinCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPinCode.DefaultText = "";
            this.txtPinCode.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPinCode.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPinCode.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPinCode.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPinCode.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPinCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPinCode.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPinCode.Location = new System.Drawing.Point(343, 29);
            this.txtPinCode.Name = "txtPinCode";
            this.txtPinCode.PasswordChar = '*';
            this.txtPinCode.PlaceholderText = "Enter Pin Code";
            this.txtPinCode.SelectedText = "";
            this.txtPinCode.Size = new System.Drawing.Size(167, 27);
            this.txtPinCode.TabIndex = 161;
            // 
            // lblAccountID
            // 
            this.lblAccountID.AutoSize = true;
            this.lblAccountID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountID.Location = new System.Drawing.Point(92, 20);
            this.lblAccountID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAccountID.Name = "lblAccountID";
            this.lblAccountID.Size = new System.Drawing.Size(23, 16);
            this.lblAccountID.TabIndex = 160;
            this.lblAccountID.Text = "??";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 16);
            this.label4.TabIndex = 159;
            this.label4.Text = "AccountID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 158;
            this.label3.Text = "Account Number";
            // 
            // guna2VSeparator1
            // 
            this.guna2VSeparator1.FillThickness = 2;
            this.guna2VSeparator1.Location = new System.Drawing.Point(324, 5);
            this.guna2VSeparator1.Name = "guna2VSeparator1";
            this.guna2VSeparator1.Size = new System.Drawing.Size(10, 109);
            this.guna2VSeparator1.TabIndex = 156;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(343, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 155;
            this.label2.Text = "Confirm Pin Code";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(343, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 154;
            this.label1.Text = "Pin Code";
            // 
            // rbInActive
            // 
            this.rbInActive.AutoSize = true;
            this.rbInActive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbInActive.Location = new System.Drawing.Point(278, 20);
            this.rbInActive.Name = "rbInActive";
            this.rbInActive.Size = new System.Drawing.Size(39, 17);
            this.rbInActive.TabIndex = 100;
            this.rbInActive.Text = "No";
            this.rbInActive.UseVisualStyleBackColor = true;
            // 
            // rbActive
            // 
            this.rbActive.AutoSize = true;
            this.rbActive.Checked = true;
            this.rbActive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbActive.Location = new System.Drawing.Point(213, 20);
            this.rbActive.Name = "rbActive";
            this.rbActive.Size = new System.Drawing.Size(45, 17);
            this.rbActive.TabIndex = 99;
            this.rbActive.TabStop = true;
            this.rbActive.Text = "Yes";
            this.rbActive.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(139, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 97;
            this.label5.Text = "Is Active:";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BorderRadius = 15;
            this.btnClose.BorderThickness = 1;
            this.btnClose.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(20)))), ((int)(((byte)(140)))));
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.SystemColors.ScrollBar;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.btnClose.Location = new System.Drawing.Point(160, 553);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(167, 38);
            this.btnClose.TabIndex = 101;
            this.btnClose.Text = "Close";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BorderRadius = 15;
            this.btnSave.BorderThickness = 1;
            this.btnSave.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(20)))), ((int)(((byte)(140)))));
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.FillColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.btnSave.Location = new System.Drawing.Point(365, 553);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(167, 38);
            this.btnSave.TabIndex = 100;
            this.btnSave.Text = "Save";
            // 
            // frmAddUpdateAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(682, 597);
            this.Controls.Add(this.gbAccountInformation);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.ctrlClientCardWithFilter1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAddUpdateAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Update Account";
            this.gbAccountInformation.ResumeLayout(false);
            this.gbAccountInformation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Clients.Components.ctrlClientCardWithFilter ctrlClientCardWithFilter1;
        private System.Windows.Forms.GroupBox gbAccountInformation;
        private Guna.UI2.WinForms.Guna2TextBox txtAccountNumber;
        private Guna.UI2.WinForms.Guna2TextBox txtConfirmPinCode;
        private Guna.UI2.WinForms.Guna2TextBox txtPinCode;
        private System.Windows.Forms.Label lblAccountID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2VSeparator guna2VSeparator1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbInActive;
        private System.Windows.Forms.RadioButton rbActive;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2Button btnSave;
    }
}