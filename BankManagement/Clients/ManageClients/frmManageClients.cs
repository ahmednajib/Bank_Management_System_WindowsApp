using BLL_BankManagement;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BankManagement.Clients.ManageClients
{
    public partial class frmManageClients : Form
    {
        private static DataTable _dtAllClients = clsClient.GetAllClients();
        private void _LoadPeopleData()
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

        private void dgvClients_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if the column is the "IsActive" column (adjust index or name as needed)
            // And check if the value is false (inactive)
            if (dgvClients.Columns[e.ColumnIndex].Name == "IsActive")
            {
                // Color the entire row light red to indicate inactive status
                dgvClients.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                dgvClients.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void frmManageClients_Load(object sender, EventArgs e)
        {
            _LoadPeopleData();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";



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

            lblNumberOfRecords.Text = dgvClients.Rows.Count.ToString();
        }
    }
}
