using BankManagement.Accounts;
using BankManagement.ManageAccounts_Transactions.Accounts;
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

namespace BankManagement.ManageAccounts_Transactions
{
    public partial class frmManageAccounts_Transactions : Form
    {
        private static DataTable _dtAllAccounts = clsAccount.GetAllAccounts();

        public frmManageAccounts_Transactions()
        {
            InitializeComponent();
        }

        private void _LoadAccountsData()
        {
            _dtAllAccounts = clsAccount.GetAllAccounts();

            dgvAccounts.DataSource = _dtAllAccounts;
            lblNumberOfRecords.Text = dgvAccounts.Rows.Count.ToString();
            cmbFilterBy.SelectedIndex = 0;
        }

        private void frmManageAccounts_Transactions_Load(object sender, EventArgs e)
        {
            _LoadAccountsData();

            dgvAccounts.Columns[0].HeaderText = "Account ID";
            dgvAccounts.Columns[0].Width = 130;

            dgvAccounts.Columns[1].HeaderText = "Client ID";
            dgvAccounts.Columns[1].Width = 130;

            dgvAccounts.Columns[2].HeaderText = "Account Number";
            dgvAccounts.Columns[2].Width = 200;

            dgvAccounts.Columns[3].HeaderText = "Client Name";
            dgvAccounts.Columns[3].Width = 270;

            dgvAccounts.Columns[4].HeaderText = "Balance";
            dgvAccounts.Columns[4].Width = 150;

            dgvAccounts.Columns[5].HeaderText = "Is Active";
            dgvAccounts.Columns[5].Width = 130;

            dgvAccounts.Columns[6].HeaderText = "Created By";
            dgvAccounts.Columns[6].Width = 130;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            // Map the ComboBox selection to the actual DataTable column names
            switch (cmbFilterBy.Text)
            {
                case "Account ID":
                    FilterColumn = "AccountID";
                    break;

                case "Client ID":
                    FilterColumn = "ClientID";
                    break;

                case "Account Number":
                    FilterColumn = "AccountNumber";
                    break;

                case "Client Name":
                    FilterColumn = "ClientName";
                    break;

                case "Created By":
                    FilterColumn = "UserName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            // Reset the filters in case nothing is selected or the filter text is empty
            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllAccounts.DefaultView.RowFilter = "";
                lblNumberOfRecords.Text = dgvAccounts.Rows.Count.ToString();
                return;
            }

            // Apply the filter based on the data type of the selected column
            if (FilterColumn == "AccountID" || FilterColumn == "ClientID")
            {
                // 1. Deal with numbers
                // Note: It's good practice to ensure the user actually typed a number to prevent crashes.
                if (int.TryParse(txtFilter.Text.Trim(), out int value))
                {
                    _dtAllAccounts.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, value);
                }
            }
            else
            {
                // 2. Deal with strings (Account Number, Client Name, Created By)
                _dtAllAccounts.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilter.Text.Trim());
            }

            lblNumberOfRecords.Text = _dtAllAccounts.DefaultView.Count.ToString();
        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset the filter value textbox and visibility based on the selected filter
            _dtAllAccounts.DefaultView.RowFilter = "";
            lblNumberOfRecords.Text = dgvAccounts.Rows.Count.ToString();

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

            if (string.IsNullOrEmpty(selectedFilter) || selectedFilter == "None")
                return;

            char keyChar = e.KeyChar;

            // Allow control characters like Backspace
            if (char.IsControl(keyChar))
                return;

            switch (selectedFilter)
            {
                case "Account ID":
                case "Client ID":
                    // Allow only digits
                    if (!char.IsDigit(keyChar))
                    {
                        e.Handled = true;
                    }
                    break;

                case "Client Name":
                    // Allow letters and spaces (Fixed to allow first and last name!)
                    if (!char.IsLetter(keyChar) && !char.IsWhiteSpace(keyChar))
                    {
                        e.Handled = true;
                    }
                    break;

                case "Account Number":
                    // Allow letters, digits, and dashes (e.g., "ACC-1001" or "123456789")
                    if (!char.IsLetterOrDigit(keyChar) && keyChar != '-')
                    {
                        e.Handled = true;
                    }
                    break;

                case "Created By":
                    // Usernames usually contain letters, digits, and maybe underscores or dots
                    if (!char.IsLetterOrDigit(keyChar) && keyChar != '_' && keyChar != '.')
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }

        private void cmbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIsActive.Text == "Yes")
                // Use single quotes because the View returns a string "Active"
                _dtAllAccounts.DefaultView.RowFilter = "IsActive = 'True'";
            else if (cmbIsActive.Text == "No")
                // Use single quotes because the View returns a string "Not Active"
                _dtAllAccounts.DefaultView.RowFilter = "IsActive = 'False'";
            else
                // Clear the filter
                _dtAllAccounts.DefaultView.RowFilter = "";

            // Best practice: Count from the View, not the Grid rows, for accuracy
            lblNumberOfRecords.Text = _dtAllAccounts.DefaultView.Count.ToString();
        }

        private void accountInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAccountInfo frm = new frmAccountInfo((int)dgvAccounts.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _LoadAccountsData();
        }

        private void addNewAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateAccount frm = new frmAddUpdateAccount();
            frm.ShowDialog();

            _LoadAccountsData();
        }

        private void updateAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateAccount frm = new frmAddUpdateAccount((int)dgvAccounts.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _LoadAccountsData();
        }

        private void activateAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Get the Account ID from the first column of the selected row
            int SelectedAccountID = (int)dgvAccounts.CurrentRow.Cells[0].Value;

            // 2. Find the account object using the BLL
            clsAccount account = clsAccount.Find(SelectedAccountID);

            if (account == null)
            {
                MessageBox.Show("No Account was found with ID = " + SelectedAccountID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Confirmation message using the Account Number for clarity
            DialogResult result = MessageBox.Show($"Are you sure you want to activate account '{account.AccountNumber}'?",
                "Confirm Activation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // 4. Change the status
                account.IsActive = true;

                // 5. Save using the BLL (which calls your SP_UpdateAccount)
                if (account.Save())
                {
                    MessageBox.Show($"Account '{account.AccountNumber}' has been activated successfully.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _LoadAccountsData(); // Call your refresh method to update the Guna2 grid
                }
                else
                {
                    MessageBox.Show("Failed to activate the account. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void deactivateAccountToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // 1. Get the Account ID from the first column of the selected row
            int SelectedAccountID = (int)dgvAccounts.CurrentRow.Cells[0].Value;

            // 2. Find the account object using the BLL
            clsAccount account = clsAccount.Find(SelectedAccountID);

            if (account == null)
            {
                MessageBox.Show("No Account was found with ID = " + SelectedAccountID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Confirmation message using the Account Number for clarity
            DialogResult result = MessageBox.Show($"Are you sure you want to deactivate account '{account.AccountNumber}'?",
                "Confirm Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // 4. Change the status
                account.IsActive = false;

                // 5. Save using the BLL (which calls your SP_UpdateAccount)
                if (account.Save())
                {
                    MessageBox.Show($"Account '{account.AccountNumber}' has been deactivated successfully.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _LoadAccountsData();
                }
                else
                {
                    MessageBox.Show("Failed to deactivate the account. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void depositToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This event Will be implemented later.
        }

        private void withdrawalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This event Will be implemented later.
        }

        private void transferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This event Will be implemented later.
        }

        private void cmsAccounts_Opening(object sender, CancelEventArgs e)
        {
            if(dgvAccounts.CurrentRow == null)
            {
                cmsAccounts.Enabled = false; // Disable the context menu if no row is selected
            }
            else
            {
                cmsAccounts.Enabled = true; // Enable the context menu if a row is selected
                // Get the IsActive value of the selected account
                
                bool isActive = (bool)dgvAccounts.CurrentRow.Cells[5].Value;
                // Show or hide Activate/Deactivate options based on the account status
                activateAccountToolStripMenuItem.Visible = !isActive; // Show "Activate" if not active
                deactivateAccountToolStripMenuItem1.Visible = isActive; // Show "Deactivate" if active
            }
        }
    }
}