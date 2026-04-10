using BankManagement.PersonalInformation.Components;
using BLL_BankManagement;
using Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagement.ManageAccounts_Transactions.Accounts
{
    public partial class frmAddUpdateAccount : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        int _AccountID;
        clsAccount _Account = new clsAccount();

        public frmAddUpdateAccount()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;

            gbAccountInformation.Enabled = false;
        }

        public frmAddUpdateAccount(int AccountID)
        {
            InitializeComponent();
            _AccountID = AccountID;
            
            _Mode = enMode.Update;
            lblTitle.Text = "Update Account";
            txtAccountNumber.Enabled = false;
        }

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Account";
                this.Text = "Add New Account";
                _Account = new clsAccount();

                ctrlClientCardWithFilter1.FilterFocus();
            }
            else
            {
                lblTitle.Text = "Update Account";
                this.Text = "Update Account";

                btnSave.Enabled = true;
            }

            txtAccountNumber.Text = "";
            txtPinCode.Text = "";
            txtConfirmPinCode.Text = "";
        }

        private void _LoadData()
        {
            _Account = clsAccount.Find(_AccountID);
            ctrlClientCardWithFilter1.FilterEnabled = false;

            //check if the Account was found or not, if not found we close the form and show a message.
            if (_Account == null)
            {
                MessageBox.Show("This form will be closed because No Account with ID = " + _Account);
                this.Close();
                return;
            }

            ctrlClientCardWithFilter1.LoadClientInfo(_Account.ClientID);

            //the following code will not be executed if the Client was not found
            lblAccountID.Text = _Account.AccountID.ToString();
            txtAccountNumber.Text = _Account.AccountNumber.ToString();

            //the following code will be executed if the Account was found
            if (_Account.IsActive == true)
                rbActive.Checked = true;
            else
                rbInActive.Checked = true;
        }

        private void frmAddUpdateAccount_Load(object sender, EventArgs e)
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

            if (ctrlClientCardWithFilter1.ClientID == -1)
            {
                MessageBox.Show("Please select or register a new Client for this Account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_Mode == enMode.AddNew)
            {
                if (!(clsClient.Find(ctrlClientCardWithFilter1.SelectedClientInfo.ClientID) != null))
                {
                    MessageBox.Show("This Client is not exist, please select another Client", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            _Account.ClientID = ctrlClientCardWithFilter1.ClientID;

            if (rbActive.Checked == true)
                _Account.IsActive = true;
            else
                _Account.IsActive = false;

            _Account.AccountNumber = txtAccountNumber.Text.Trim();

            if (!string.IsNullOrEmpty(txtPinCode.Text.Trim()))
                _Account.PinCode = clsHashing.ComputeHash(txtPinCode.Text.Trim());

            if (_Account.Save())
            {
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Account";
                txtAccountNumber.Enabled = false;

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblAccountID.Text = _Account.AccountID.ToString();
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtAccountNumber_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccountNumber.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAccountNumber, "AccountNumber cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtAccountNumber, null);
            }

            if (_Mode == enMode.Update && txtAccountNumber.Text.Trim() == _Account.AccountNumber)
                return;

            if (clsAccount.IsAccountExist(txtAccountNumber.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAccountNumber, "this AccountNumber already exist use another one");
            }
            else
            {
                errorProvider1.SetError(txtAccountNumber, null);
            }
        }

        private void txtPinCode_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPinCode.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPinCode, "PinCode cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtPinCode, null);
            }
        }

        private void txtConfirmPinCode_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPinCode.Text != txtPinCode.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPinCode, "Confirm PinCode does not match PinCode");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPinCode, null);
            }
        }

        private void ctrlClientCardWithFilter1_OnClientSelected(int obj)
        {
            if (obj != -1)
                gbAccountInformation.Enabled = true;
            else
                gbAccountInformation.Enabled = false;
        }
    }
}