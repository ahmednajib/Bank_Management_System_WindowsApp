using BLL_BankManagement;
using Classes;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BankManagement.Transactions
{
    public partial class frmDepositWithdrawal : Form
    {
        public enum TransactionType { Deposit, Withdrawal }
        private TransactionType _transactionType;

        private int _accountID;
        private clsAccount _Account;

        private clsTransaction _clsTransaction;

        public frmDepositWithdrawal(int AccountID, TransactionType transactionType)
        {
            InitializeComponent();
            _accountID = AccountID;
            _transactionType = transactionType;
        }

        private void frmDepositWithdrawal_Load(object sender, EventArgs e)
        {
            ctrlAccountCard1.LoadAccountInfo(_accountID);

            if (ctrlAccountCard1.SelectedAccountInfo == null)
            {
                MessageBox.Show("Account information could not be loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;
            }

            _Account = ctrlAccountCard1.SelectedAccountInfo;

            if (_transactionType == TransactionType.Deposit)
            {
                this.Text = "Deposit";
                lblAmount.Text = "Deposit Amount:";
                lblTitle.Text = "Deposit Money";
            }
            else
            {
                this.Text = "Withdrawal";
                lblAmount.Text = "Withdrawal Amount:";
                lblTitle.Text = "Withdraw Money";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAmount_Validating(object sender, CancelEventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0 || txtAmount.Text == null)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAmount, "Please enter a valid positive amount.");
            }
            else if (_transactionType == TransactionType.Withdrawal && amount > _Account.Balance)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAmount, "Withdrawal amount exceeds current balance.");
            }
            else
            {
                errorProvider1.SetError(txtAmount, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
                return;

            if (MessageBox.Show("Are you sure you want to perform this operation", "confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                // Create a new transaction object and populate it with the necessary details
                _clsTransaction = new clsTransaction();

                _clsTransaction.Amount = amount;
                _clsTransaction.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (_transactionType == TransactionType.Deposit)
                {
                    string defaultDescription = $"Money was deposited in {DateTime.Now.ToShortDateString()}";
                    _clsTransaction.Description = txtDescription.Text.Trim() != string.Empty ? txtDescription.Text.Trim() : defaultDescription;
                    _clsTransaction.SenderAccountID = 0; // For deposits, sender is external
                    _clsTransaction.ReceiverAccountID = _Account.AccountID;

                    if (_clsTransaction.PerformNewTransaction(clsTransaction.enTransactionType.Deposit))
                    {
                        ctrlAccountCard1.LoadAccountInfo(_accountID); // Refresh account info to show updated balance
                        _Account = ctrlAccountCard1.SelectedAccountInfo; // Update local account info with refreshed data

                        MessageBox.Show($"Successfully deposited {amount:C2}. New balance: {_Account.Balance:C2}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while processing the deposit. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    string defaultDescription = $"Money was Wethdrew in {DateTime.Now.ToShortDateString()}";
                    _clsTransaction.Description = txtDescription.Text.Trim() != string.Empty ? txtDescription.Text.Trim() : defaultDescription;

                    _clsTransaction.SenderAccountID = _Account.AccountID;
                    _clsTransaction.ReceiverAccountID = 0; // For withdrawals, receiver is external

                    if (amount > _Account.Balance)
                    {
                        MessageBox.Show("Insufficient funds for this withdrawal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (_clsTransaction.PerformNewTransaction(clsTransaction.enTransactionType.Withdraw))
                    {
                        ctrlAccountCard1.LoadAccountInfo(_accountID); // Refresh account info to show updated balance
                        _Account = ctrlAccountCard1.SelectedAccountInfo; // Update local account info with refreshed data

                        MessageBox.Show($"Successfully withdrew {amount:C2}. New balance: {_Account.Balance:C2}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while processing the withdrawal. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                txtAmount.Clear();
                txtAmount.Focus();
            }
        }
    }
}