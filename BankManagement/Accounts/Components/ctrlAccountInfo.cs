using BLL_BankManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagement.Accounts.Accounts
{
    public partial class ctrlAccountInfo : UserControl
    {
        private clsAccount _Account;
        private int _AccountID = -1;

        public int AccountID
        {
            get { return _AccountID; }
        }

        public clsAccount SelectedAccountInfo
        {
            get { return _Account; }
        }

        public ctrlAccountInfo()
        {
            InitializeComponent();
        }

        private void LoadDefaultInfo()
        {
            lblAccountID.Text = "[?????]";
            lblAccountNumber.Text = "[?????]";
            lblIsActive.Text = "[?????]";
            lblBalance.Text = "[???]";
        }

        public void LoadAccountInformation(int AccountID)
        {
            _Account = clsAccount.Find(AccountID);
            LoadDefaultInfo();

            if (_Account == null)
            {
                MessageBox.Show($"Account with ID={AccountID} was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ctrlClientCard1.LoadClientInformation(_Account.ClientID);
            _LoadAccountInfo();
        }

        public void LoadAccountInformation(string AccountNumber)
        {
            _Account = clsAccount.Find(AccountNumber);
            LoadDefaultInfo();

            if (_Account == null)
            {
                MessageBox.Show($"Account with Number={AccountNumber} was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ctrlClientCard1.LoadClientInformation(_Account.ClientID);
            _LoadAccountInfo();
        }

        private void _LoadAccountInfo()
        {
            _AccountID = _Account.AccountID;
            lblAccountID.Text = AccountID.ToString();
            lblAccountNumber.Text = _Account.AccountNumber;
            lblBalance.Text = _Account.Balance.ToString("C");
            lblIsActive.Text = _Account.IsActive ? "Active" : "Inactive";
        }
    }
}