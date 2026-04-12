using BLL_BankManagement;
using Classes;
using Global_Classes;
using System;
using System.Windows.Forms;

namespace BankManagement
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close the application
            Application.Exit();
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {
            // 2. Set the current date on load
            lblDate.Text = "Date: " + DateTime.Now.ToString("d/M/yyyy");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginFailedMessage.Visible = false;

            if (!this.ValidateChildren())
            {
                return;
            }

            clsUser user = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(), clsHashing.ComputeHash(txtPassword.Text.Trim()));

            //check if the user is found or not.
            if (user != null)
            {
                if (chkRememberMe.Checked)
                {
                    //store username and password
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    //store empty username and password
                    clsGlobal.RememberUsernameAndPassword("", "");
                }

                if (!user.IsActive)
                {
                    txtUserName.Focus();
                    lblErrorMessage.Text = "You are not active anymore!";
                    LoginFailedMessage.Visible = true;
                    return;
                }

                clsGlobal.CurrentUser = user;
                frmDashboard frm = new frmDashboard(this);

                this.Hide();
                frm.ShowDialog();
            }
            else
            {
                txtUserName.Focus();
                lblErrorMessage.Text = "Invalid username or password!";
                LoginFailedMessage.Visible = true;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredential(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;
        }

        private void txtUserName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "UserName can not be empty.");
            }
            else
            {
                errorProvider1.SetError(txtUserName, string.Empty);
            }
        }

        private void txtPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Password can not be empty.");
            }
            else
            {
                errorProvider1.SetError(txtUserName, string.Empty);
            }
        }
    }
}