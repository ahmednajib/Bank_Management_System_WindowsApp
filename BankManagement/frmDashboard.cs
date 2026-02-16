using BankManagement.Clients.ManageClients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankManagement
{
    public partial class frmDashboard : Form
    {
        private Form activeForm = null;

        private void OpenChildForm(Form childForm)
        {
            // 1. Clear existing form just like before
            if (pnlMainContainer.Controls.Count > 0)
                pnlMainContainer.Controls[0].Dispose();

            // 2. Setup basic properties
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;

            // --- THE FIX STARTS HERE ---

            // TURN OFF Docking. It conflicts with Guna2BorderlessForm.
            childForm.Dock = DockStyle.None;

            // Use Anchoring instead. This tells the form: 
            // "If the container gets bigger, stretch with it in all directions."
            childForm.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Manually set the size to fit exactly inside the container's padded area.
            // DisplayRectangle gets the area *inside* the padding you set earlier.
            childForm.Bounds = pnlMainContainer.DisplayRectangle;

            // --- THE FIX ENDS HERE ---

            // 4. Add to container and show
            pnlMainContainer.Controls.Add(childForm);
            pnlMainContainer.Tag = childForm;
            childForm.Show();
        }

        public frmDashboard()
        {
            InitializeComponent();
        }

        private void btnManageClients_Click(object sender, EventArgs e)
        {
            // Pass the form you created for the Client List
            OpenChildForm(new frmManageClients());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            this.Close();
            frm.ShowDialog();

        }
    }
}
