using BankManagement.Properties;
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
using TheArtOfDevHtmlRenderer.Adapters;

namespace BankManagement.Clients.Add_Update_Client
{
    public partial class frmAddUpdateClient : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int ClientID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        int _ClientID;
        clsClient _Client = new clsClient();

        public frmAddUpdateClient(int ClientID)
        {
            InitializeComponent();

            _ClientID = ClientID;
            _Mode = enMode.Update;
            lblTitle.Text = "Update Client";
        }

        public frmAddUpdateClient()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        private void _LoadData()
        {
            _Client = clsClient.Find(_ClientID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;

            //check if the Client was found or not, if not found we close the form and show a message.
            if (_Client == null)
            {
                MessageBox.Show("This form will be closed because No Contact with ID = " + _Client);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_Client.PersonID);

            //the following code will be executed if the Client was found
            if (_Client.IsActive == true)
                rbActive.Checked = true;
            else
                rbInActive.Checked = true;
        }

        private void frmAddUpdateClient_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ctrlPersonCardWithFilter1.PersonID == -1)
            {
                MessageBox.Show("Please select a person for this client", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(_Mode == enMode.AddNew) 
            { 
                if(clsClient.Find(ctrlPersonCardWithFilter1.SelectedPersonInfo.NationalNo) != null)
                {
                    MessageBox.Show("This person is already a client, please select another person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            _Client.PersonID = ctrlPersonCardWithFilter1.PersonID;

            if (rbActive.Checked == true)
                _Client.IsActive = true;
            else
                _Client.IsActive = false;

            if (_Client.Save())
            {
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Client";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Client.PersonID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}