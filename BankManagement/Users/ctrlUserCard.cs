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

namespace BankManagement.Users
{
    public partial class ctrlUserCard : UserControl
    {
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        private int _UserID = -1;
        private clsUser _User;

        private void _LoadDefaultInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();
            lblUserID.Text = "[?????]";
            lblUserName.Text = "[?????]";
            lblIsActive.Text = "[?????]";
        }

        private void _LoadUserInfo()
        {
            _UserID = _User.UserID;
            lblUserID.Text = _UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();

            if (_User.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";

            ctrlPersonCard1.LoadPersonInformation(_User.PersonID);
        }

        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.Find(UserID);

            if (_User == null)
            {
                _LoadDefaultInfo();
                MessageBox.Show($"User with ID={UserID} was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadUserInfo();
        }
    }
}
