using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagement.Accounts
{
    public partial class frmAccountInfo : Form
    {
        private int _AccountID;
        public frmAccountInfo(int accountID)
        {
            InitializeComponent();
            _AccountID = accountID;
        }

        private void frmAccountInfo_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAccountInfo_Load(object sender, EventArgs e)
        {
            ctrlAccountInfo1.LoadAccountInformation(_AccountID);
        }
    }
}
