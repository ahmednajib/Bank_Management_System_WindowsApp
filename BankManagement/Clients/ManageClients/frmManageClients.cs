using BankManagement.Clients.Add_Update_Client;
using BankManagement.ManageAccounts_Transactions.Accounts;
using BLL_BankManagement;
using System;
using System.Data;
using System.Windows.Forms;

namespace BankManagement.Clients.ManageClients
{
    public partial class frmManageClients : Form
    {
        private static DataTable _dtAllClients = clsClient.GetAllClients();
        
        private void _LoadClientsData()
        {
            _dtAllClients = clsClient.GetAllClients();
            
            dgvClients.DataSource = _dtAllClients;
            lblNumberOfRecords.Text = dgvClients.Rows.Count.ToString();
            cmbFilterBy.SelectedIndex = 0;
        }

        public frmManageClients()
        {
            InitializeComponent();
        }

        private void frmManageClients_Load(object sender, EventArgs e)
        {
            _LoadClientsData();

            dgvClients.Columns[0].HeaderText = "Client ID";
            dgvClients.Columns[0].Width = 100;

            dgvClients.Columns[1].HeaderText = "National No";
            dgvClients.Columns[1].Width = 100;

            dgvClients.Columns[2].HeaderText = "Full Name";
            dgvClients.Columns[2].Width = 150;

            dgvClients.Columns[3].HeaderText = "Birth Date";
            dgvClients.Columns[3].Width = 150;

            dgvClients.Columns[4].HeaderText = "Gender";
            dgvClients.Columns[4].Width = 110;

            dgvClients.Columns[5].HeaderText = "Phone";
            dgvClients.Columns[5].Width = 140;

            dgvClients.Columns[6].HeaderText = "Email";
            dgvClients.Columns[6].Width = 160;

            dgvClients.Columns[7].HeaderText = "Nationality";
            dgvClients.Columns[7].Width = 120;

            dgvClients.Columns[8].HeaderText = "Active status";
            dgvClients.Columns[8].Width = 110;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cmbFilterBy.Text)
            {
                case "Client ID":
                    FilterColumn = "ClientID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Nationality":
                    FilterColumn = "Nationality";
                    break;

                case "Gender":
                    FilterColumn = "Gender";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }
            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllClients.DefaultView.RowFilter = "";
                lblNumberOfRecords.Text = dgvClients.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "ClientID")
                //in this case we deal with numbers not string.
                _dtAllClients.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            else
                _dtAllClients.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilter.Text.Trim());

            lblNumberOfRecords.Text = _dtAllClients.DefaultView.Count.ToString();
        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset the filter value textbox and visibility based on the selected filter
            _dtAllClients.DefaultView.RowFilter = "";
            lblNumberOfRecords.Text = dgvClients.Rows.Count.ToString();

            if (cmbFilterBy.Text == "Is Active")
            {
                txtFilter.Visible = false;
                cmbIsActive.Visible = true;
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtFilter.Visible = (cmbFilterBy.Text != "None");
                cmbIsActive.Visible = false;
                txtFilter.Text = "";
                txtFilter.Focus();
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            string selectedFilter = cmbFilterBy.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedFilter))
                return;

            char keyChar = e.KeyChar;

            // Allow control characters like Backspace
            if (char.IsControl(keyChar))
                return;

            switch (selectedFilter)
            {
                case "Client ID":
                    // Allow only digits
                    if (!char.IsDigit(keyChar))
                    {
                        e.Handled = true;
                    }
                    break;

                case "Full Name":
                case "Nationality":
                case "Gender":
                    // Allow only letters
                    if (!char.IsLetter(keyChar))
                    {
                        e.Handled = true;
                    }
                    break;

                case "National No":
                    // Allow letters, digits, and common email characters
                    if (!char.IsLetterOrDigit(keyChar) && keyChar != '@' && keyChar != '.' && keyChar != '-' && keyChar != '_')
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }

        private void cmbActiveOrNot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIsActive.Text == "Yes")
                // Use single quotes because the View returns a string "Active"
                _dtAllClients.DefaultView.RowFilter = "IsActive = 'Active'";
            else if (cmbIsActive.Text == "No")
                // Use single quotes because the View returns a string "Not Active"
                _dtAllClients.DefaultView.RowFilter = "IsActive = 'Not Active'";
            else
                // Clear the filter
                _dtAllClients.DefaultView.RowFilter = "";

            // Best practice: Count from the View, not the Grid rows, for accuracy
            lblNumberOfRecords.Text = _dtAllClients.DefaultView.Count.ToString();
        }

        private void addNewClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateClient addClientForm = new frmAddUpdateClient();
            addClientForm.ShowDialog();
            _LoadClientsData(); // Refresh the data after adding a new client
        }

        private void btnAddNewClient_Click(object sender, EventArgs e)
        {
            frmAddUpdateClient addClientForm = new frmAddUpdateClient();
            addClientForm.ShowDialog();
            _LoadClientsData(); // Refresh the data after adding a new client
        }

        private void updateClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedClientID = Convert.ToInt32(dgvClients.CurrentRow.Cells[0].Value);
            frmAddUpdateClient updateClientForm = new frmAddUpdateClient(SelectedClientID);
            updateClientForm.ShowDialog();
            _LoadClientsData(); // Refresh the data after updating the client
        }

        private void deactivateClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedClientID = Convert.ToInt32(dgvClients.CurrentRow.Cells[0].Value);
            clsClient client = clsClient.Find(SelectedClientID);
            
            if (client == null)
            {
                MessageBox.Show("No Client was found with ID = " + SelectedClientID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            
            DialogResult result = MessageBox.Show("Are you sure you want to diactivate client '" + client.PersonInfo.FullName + "'?", "Confirm Diactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                client.IsActive = false;

                if (client.Save())
                {
                    MessageBox.Show("Client '" + client.PersonInfo.FullName + "' has been diactivated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _LoadClientsData(); // Refresh the data after diactivation
                }
                else
                {
                    MessageBox.Show("Failed to diactivate client '" + client.PersonInfo.FullName + "'. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void activateClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedClientID = Convert.ToInt32(dgvClients.CurrentRow.Cells[0].Value);
            clsClient client = clsClient.Find(SelectedClientID);
            
            if (client == null)
            {
                MessageBox.Show("No Client was found with ID = " + SelectedClientID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            
            DialogResult result = MessageBox.Show("Are you sure you want to activate client '" + client.PersonInfo.FullName + "'?", "Confirm Activation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                client.IsActive = true;

                if (client.Save())
                {
                    MessageBox.Show("Client '" + client.PersonInfo.FullName + "' has been activated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _LoadClientsData(); // Refresh the data after activation
                }
                else
                {
                    MessageBox.Show("Failed to activate client '" + client.PersonInfo.FullName + "'. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int SelectedClientID = Convert.ToInt32(dgvClients.CurrentRow.Cells[0].Value);
            clsClient client = clsClient.Find(SelectedClientID);

            if (client == null)
            {
                MessageBox.Show("No Client was found with ID = " + SelectedClientID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            diactivateClientToolStripMenuItem.Enabled = client.IsActive;
            activateClientToolStripMenuItem.Enabled = !client.IsActive;
        }

        private void clientInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientInfo clientInfoForm = new frmClientInfo(Convert.ToInt32(dgvClients.CurrentRow.Cells[0].Value));
            clientInfoForm.ShowDialog();
            _LoadClientsData();
        }

        private void addAnAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateAccount addAccountForm = new frmAddUpdateAccount();
            addAccountForm.ShowDialog();

            _LoadClientsData();
        }

        private void allAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This functionality is not implemented yet.
        }
    }
}