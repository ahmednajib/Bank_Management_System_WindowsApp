using System;
using System.Windows.Forms;

namespace BankManagement.Clients
{
    public partial class frmClientInfo : Form
    {
        private int _ClientID;
        public frmClientInfo(int clientID)
        {
            InitializeComponent();
            _ClientID = clientID;
        }

        private void frmClientInfo_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmClientInfo_Load(object sender, EventArgs e)
        {
            ctrlClientCard1.LoadClientInformation(_ClientID);
        }
    }
}
