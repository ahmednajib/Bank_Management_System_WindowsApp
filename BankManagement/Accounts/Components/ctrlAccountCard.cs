using BLL_BankManagement;
using System.Drawing;
using System.Windows.Forms;

namespace BankManagement.Accounts.Components
{
    public partial class ctrlAccountCard : UserControl
    {
        public ctrlAccountCard()
        {
            InitializeComponent();
        }

        private clsAccount _Account;

        public int AccountID => (_Account != null) ? _Account.AccountID : -1;

        public decimal Update_Balance 
        {
            set 
            { 
                    if (_Account != null)
                    {
                        lblBalance.Text = value.ToString("C2");
                    }
            }
        }

        public string ChangeTitile
        {
            set
            {
                lblTitle.Text = value.ToString();
            }
        }

        public clsAccount SelectedAccountInfo => _Account;

        public void LoadAccountInfo(int AccountID)
        {
            _Account = clsAccount.Find(AccountID);

            if (_Account == null)
            {
                _ResetDefaultValues();
                MessageBox.Show("Account not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillCardData();
        }

        private void _ResetDefaultValues()
        {
            lblClientName.Text = "[?????]";
            lblAccountNumber.Text = "[?????]";
            lblBalance.Text = "[?????]";
            lblNationality.Text = "[?????]";
            lblIsActive.Text = "[?????]";
            lblIsActive.ForeColor = Color.Black;

            pbClientImage.Image = Properties.Resources.Male_512;
        }

        private void _FillCardData()
        {
            // Accessing data through composition chain
            lblAccountNumber.Text = _Account.AccountNumber;
            lblBalance.Text = _Account.Balance.ToString("C2");

            // Drilling down to PersonInfo
            lblClientName.Text = _Account.ClientInfo.PersonInfo.FullName;
            lblNationality.Text = _Account.ClientInfo.PersonInfo.CountryInfo.CountryName;

            // Handling the image
            if (_Account.ClientInfo.PersonInfo.ImagePath != "")
                pbClientImage.ImageLocation = _Account.ClientInfo.PersonInfo.ImagePath;
            else
            {
                if (_Account.ClientInfo.PersonInfo.Gender == 0)
                    pbClientImage.Image = Properties.Resources.Male_512;
                else
                    pbClientImage.Image = Properties.Resources.Female_512;
            }

            // Visual status
            lblIsActive.Text = (_Account.IsActive) ? "Active" : "Inactive";
            lblIsActive.ForeColor = (_Account.IsActive) ? Color.Green : Color.Red;
        }
    }
}