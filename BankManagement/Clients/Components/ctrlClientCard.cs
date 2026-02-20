using BLL_BankManagement;
using System.Windows.Forms;

namespace BankManagement.Clients.Components
{
    public partial class ctrlClientCard : UserControl
    {
        private clsClient _Client;
        private int _ClientID = -1;

        public int ClientID
        {
            get { return _ClientID; }
        }

        public clsClient SelectedClientInfo
        {
            get { return _Client; }
        }

        public ctrlClientCard()
        {
            InitializeComponent();
        }

        private void LoadDefaultInfo()
        {
            lblClientID.Text = "[?????]";
            lblDateJoined.Text = "[??-??-??]";
            lblIsActive.Text = "[?????]";
        }

        // to call it form outside when needed
        public void ResetClientInfo()
        {
            LoadDefaultInfo();
        }

        public void LoadClientInformation(int ClientID)
        {
            _Client = clsClient.Find(ClientID);

            LoadDefaultInfo();
            if (_Client == null)
            {
                MessageBox.Show($"Client with ID={ClientID} was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCard1.ResetPersonInfo();
                return;
            }

            ctrlPersonCard1.LoadPersonInformation(_Client.PersonID);
            _LoadClientInfo();
        }

        public void LoadClientInformation(string NationalNo)
        {
            _Client = clsClient.Find(NationalNo);

            LoadDefaultInfo();
            if (_Client == null)
            {
                MessageBox.Show($"Client with NationalNo={NationalNo} was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCard1.ResetPersonInfo();
                return;
            }
            
            ctrlPersonCard1.LoadPersonInformation(_Client.PersonID);
            _LoadClientInfo();
        }

        private void _LoadClientInfo()
        {
            _ClientID = _Client.ClientID;
            lblClientID.Text = ClientID.ToString();
            lblDateJoined.Text = _Client.DateJoined.ToString("dd/MM/yyyy");
            lblIsActive.Text = _Client.IsActive ? "Active" : "Inactive";
        }

    }
}