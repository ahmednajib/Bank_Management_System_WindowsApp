using BankManagement.Clients.Add_Update_Client;
using BLL_BankManagement;
using System;
using System.Windows.Forms;

namespace BankManagement.Clients.Components
{
    public partial class ClientCardWithFilter : UserControl
    {
        // Define a custom event handler delegate with parameters
        public event Action<int> OnClientSelected;

        // Create a protected method to raise the event with a parameter
        protected virtual void ClientSelected(int ClientID)
        {
            Action<int> handler = OnClientSelected;
            if (handler != null)
            {
                handler(ClientID); // Raise the event with the parameter
            }
        }

        private bool _ShowAddClient = true;

        public bool ShowAddClient
        {
            get
            {
                return _ShowAddClient;
            }
            set
            {
                _ShowAddClient = value;
                btnAddNewClient.Visible = _ShowAddClient;
            }
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        public ClientCardWithFilter()
        {
            InitializeComponent();
        }

        public int ClientID
        {
            get { return ctrlClientCard1.ClientID; }
        }

        public clsClient SelectedClientInfo
        {
            get { return ctrlClientCard1.SelectedClientInfo; }
        }

        public void LoadClientInfo(int ClientID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = ClientID.ToString();
            FindNow();
        }

        private void DataBackEvent(object sender, int ClientID)
        {
            // Handle the data received
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = ClientID.ToString();
            ctrlClientCard1.LoadClientInformation(ClientID);
        }

        private void FindNow()
        {
            switch (cbFilterBy.Text)
            {
                case "Client ID":
                    ctrlClientCard1.LoadClientInformation(int.Parse(txtFilterValue.Text));

                    break;

                case "National No":
                    ctrlClientCard1.LoadClientInformation(txtFilterValue.Text);
                    break;

                default:
                    break;
            }

            // Check if the Client was found
            if (OnClientSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnClientSelected(ctrlClientCard1.ClientID);
        }

        private void ClientCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0; // Default selection for filter
            txtFilterValue.Focus(); // Set focus to the filter text box
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilterBy.Text == "Client ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = string.Empty; // Clear the filter value when filter type changes
            txtFilterValue.Focus(); // Set focus to the filter text box
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtFilterValue.Text == "")
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Please fill in the field", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FindNow();
        }

        private void btnAddNewClient_Click(object sender, EventArgs e)
        {
            frmAddUpdateClient frm1 = new frmAddUpdateClient();
            frm1.DataBack += DataBackEvent; // Subscribe to the event
            frm1.ShowDialog();
        }
    }
}