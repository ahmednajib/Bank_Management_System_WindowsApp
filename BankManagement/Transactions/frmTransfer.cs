using BLL_BankManagement;
using Classes;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BankManagement.Transactions
{
    public partial class frmTransfer : Form
    {
        private clsAccount _SenderAccount;
        private clsAccount _ReceiverAccount;

        private clsTransaction _Transaction;

        private int _SenderAccountID = -1;
        private int _ReceiverAccountID = -1;

        public frmTransfer(int SenderAccount)
        {
            InitializeComponent();
            _SenderAccountID = SenderAccount;
        }

        private clsAccount FindAccount(int AccountID)
        {
            clsAccount _Account = new clsAccount();
            return clsAccount.Find(AccountID);
        }

        private void frmTransfer_Load(object sender, EventArgs e)
        {
            if ((_SenderAccount = FindAccount(_SenderAccountID)) == null)
            {
                MessageBox.Show("Sender account not found. Please check the account number and try again.", "Account Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtReceiverAccount.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the receiver's account number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtReceiverAccount.Clear();
                txtReceiverAccount.Focus();
                return;
            }

            if ((_ReceiverAccount = FindAccount(int.Parse(txtReceiverAccount.Text.Trim()))) == null)
            {
                MessageBox.Show("Receiver account not found. Please check the account number and try again.", "Account Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtReceiverAccount.Clear();
                txtReceiverAccount.Focus();
                return;
            }
            _ReceiverAccountID = _ReceiverAccount.AccountID;
            ctrlReceiverAccountCard.LoadAccountInfo(_ReceiverAccountID);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                return;
            }

            if (ctrlReceiverAccountCard.SelectedAccountInfo == null)
            {
                MessageBox.Show("Please first find the receiver's account", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_ReceiverAccountID == _SenderAccountID)
            {
                MessageBox.Show("Sender and receiver accounts cannot be the same. Please enter a different receiver account number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((_SenderAccount = FindAccount(_SenderAccountID)) == null)
            {
                MessageBox.Show("Sender account not found. Please check the account number and try again.", "Account Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if(!_SenderAccount.IsActive)
            {
                MessageBox.Show("Sender account is not active. Cannot perform transactions on an inactive account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_ReceiverAccount.IsActive)
            {
                MessageBox.Show("Receiver account is not active. Cannot perform transactions on an inactive account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to perform this transfer?", "Confirm Transfer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            string defaultDescription = $"Transfer from Account {_SenderAccount.AccountNumber} to Account {ctrlReceiverAccountCard.SelectedAccountInfo.AccountNumber}";

            _Transaction = new clsTransaction
            {
                SenderAccountID = _SenderAccount.AccountID,
                ReceiverAccountID = _ReceiverAccount.AccountID,
                Amount = decimal.Parse(txtAmount.Text),
                TransactionType = clsTransaction.enTransactionType.Transfer,
                Description = txtDescription.Text.Trim() == "" ? defaultDescription : txtDescription.Text.Trim(),
                CreatedByUserID = clsGlobal.CurrentUser.UserID
            };

            if (_Transaction.PerformNewTransaction(clsTransaction.enTransactionType.Transfer))
            {
                MessageBox.Show("Transfer successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAmount.Clear();

                ctrlReceiverAccountCard.LoadAccountInfo(_ReceiverAccountID);
                _ReceiverAccount = FindAccount(_ReceiverAccountID);
                _SenderAccount = FindAccount(_SenderAccountID);
            }
            else
            {
                MessageBox.Show("Transfer failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAmount.Clear();
                txtAmount.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtReceiverAccount_Validating(object sender, CancelEventArgs e)
        {
            if (txtReceiverAccount.Text == null)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtReceiverAccount, "Please enter an account number.");
            }
            else
            {
                errorProvider1.SetError(txtReceiverAccount, null);
            }
        }

        private void txtAmount_Validating(object sender, CancelEventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0 || txtAmount.Text == null)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAmount, "Please enter a valid positive amount.");
            }
            else if (amount > _SenderAccount.Balance)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAmount, $"Transfer amount exceeds current balance ({_SenderAccount.Balance})");
            }
            else
            {
                errorProvider1.SetError(txtAmount, null);
            }
        }
    }
}