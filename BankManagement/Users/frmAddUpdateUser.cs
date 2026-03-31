using BLL_BankManagement;
using Global_Classes;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BankManagement.Users
{
    public partial class frmAddUpdateUser : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int UserID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        int _UserID;
        clsUser _User = new clsUser();

        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;

            gbUserInformation.Enabled = false;
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
            _Mode = enMode.Update;
            lblTitle.Text = "Update User";
        }

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUser();

                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                btnSave.Enabled = true;
            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
        }

        private void _LoadData()
        {
            _User = clsUser.Find(_UserID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;

            //check if the User was found or not, if not found we close the form and show a message.
            if (_User == null)
            {
                MessageBox.Show("This form will be closed because No Contact with ID = " + _User);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);

            //the following code will not be executed if the person was not found
            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;

            //the following code will be executed if the User was found
            if (_User.IsActive == true)
                rbActive.Checked = true;
            else
                rbInActive.Checked = true;
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                return;
            }

            if (ctrlPersonCardWithFilter1.PersonID == -1)
            {
                MessageBox.Show("Please select or create a person for this User", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_Mode == enMode.AddNew)
            {
                if (clsUser.FindByPersonID(ctrlPersonCardWithFilter1.SelectedPersonInfo.PersonID) != null)
                {
                    MessageBox.Show("This person is already a User, please select another person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;

            if (rbActive.Checked == true)
                _User.IsActive = true;
            else
                _User.IsActive = false;

            _User.UserName = txtUserName.Text.Trim();

            if (!string.IsNullOrEmpty(txtPassword.Text.Trim()))
                _User.Password = clsHashing.ComputeHash(txtPassword.Text.Trim());

            if (_User.Save())
            {
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update User";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblUserID.Text = _User.UserID.ToString();

                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _User.PersonID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (_Mode == enMode.Update)
                return;

            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "UserName cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }

            if (_Mode == enMode.Update && txtUserName.Text.Trim() == _User.UserName)
                return;

            if (clsUser.IsUserExist(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "this UserName already exist use another one");
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (_Mode == enMode.Update && string.IsNullOrEmpty(txtPassword.Text.Trim()))
                return;

            if (txtConfirmPassword.Text != txtPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password does not match Password");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            if (clsUser.FindByPersonID(obj) != null)
            {
                MessageBox.Show("This person is already a User, please select another person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
                return;
            }
            gbUserInformation.Enabled = true;
        }
    }
}