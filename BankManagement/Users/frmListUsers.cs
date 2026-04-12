using BLL_BankManagement;
using System;
using System.Data;
using System.Windows.Forms;

namespace BankManagement.Users
{
    public partial class frmListUsers : Form
    {
        private static DataTable _dtAllUsers;

        public frmListUsers()
        {
            InitializeComponent();
        }

        private void _LoadUserInfo()
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            cbFilterBy.SelectedIndex = 0;
            lblNumberOfRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void frmListUsers_Load(object sender, System.EventArgs e)
        {
            _LoadUserInfo();

            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 100;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 100;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 350;

                dgvUsers.Columns[3].HeaderText = "User Name";
                dgvUsers.Columns[3].Width = 150;

                dgvUsers.Columns[4].HeaderText = "Email";
                dgvUsers.Columns[4].Width = 160;

                dgvUsers.Columns[5].HeaderText = "Phone";
                dgvUsers.Columns[5].Width = 160;

                dgvUsers.Columns[6].HeaderText = "Is Active";
                dgvUsers.Columns[6].Width = 120;
            }
        }

        private void txtFilterValue_TextChanged(object sender, System.EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "User Name":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblNumberOfRecords.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            if (FilterColumn != "FullName" && FilterColumn != "UserName")
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblNumberOfRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Reset the filter value textbox and visibility based on the selected filter
            _dtAllUsers.DefaultView.RowFilter = "";
            lblNumberOfRecords.Text = dgvUsers.Rows.Count.ToString();

            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cbIsActive.Text == "Yes")
                _dtAllUsers.DefaultView.RowFilter = "[IsActive] = true"; // Default to showing active users
            else if (cbIsActive.Text == "No")
                _dtAllUsers.DefaultView.RowFilter = "[IsActive] = false"; // Default to showing inactive users
            else
                _dtAllUsers.DefaultView.RowFilter = ""; // Show all records if "All" is selected
            lblNumberOfRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id or user id is selected.
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAddNewUser_Click(object sender, System.EventArgs e)
        {
            frmAddUpdateUser frmAddUpdateUser = new frmAddUpdateUser();
            frmAddUpdateUser.ShowDialog();
            _LoadUserInfo();
        }

        private void showDetailsToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _LoadUserInfo();
        }

        private void editUserToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            frmAddUpdateUser frmAddUpdateUser = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frmAddUpdateUser.ShowDialog();
            _LoadUserInfo();
        }

        private void changePasswordToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmChangePassword frmChangePassword = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frmChangePassword.ShowDialog();

            _LoadUserInfo();
        }

        private void DeactivateUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedIndexID = Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value);

            clsUser User = clsUser.Find(SelectedIndexID);

            if (User == null)
            {
                MessageBox.Show("No User was found with ID = " + SelectedIndexID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to deactivate User '" + User.PersonInfo.FullName + "'?", "Confirm Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                User.IsActive = false;

                if (User.Save())
                {
                    MessageBox.Show("User '" + User.PersonInfo.FullName + "' has been deactivated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _LoadUserInfo(); // Refresh the data after diactivation
                }
                else
                {
                    MessageBox.Show("Failed to deactivate User '" + User.PersonInfo.FullName + "'. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void activateUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedIndexID = Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value);

            clsUser User = clsUser.Find(SelectedIndexID);

            if (User == null)
            {
                MessageBox.Show("No User was found with ID = " + SelectedIndexID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to activate User '" + User.PersonInfo.FullName + "'?", "Confirm activation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                User.IsActive = true;

                if (User.Save())
                {
                    MessageBox.Show("User '" + User.PersonInfo.FullName + "' has been activated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _LoadUserInfo(); // Refresh the data after diactivation
                }
                else
                {
                    MessageBox.Show("Failed to activate User '" + User.PersonInfo.FullName + "'. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmsUsers_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int SelectedUserID = Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value);

            clsUser User = clsUser.Find(SelectedUserID);

            if (User == null)
            {
                MessageBox.Show("No User was found with ID = " + SelectedUserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            DeactivateUserToolStripMenuItem.Enabled = User.IsActive;
            activateUserToolStripMenuItem.Enabled = !User.IsActive;
        }
    }
}