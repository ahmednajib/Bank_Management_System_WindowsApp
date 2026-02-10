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
            // 1. Close any existing open form to free memory
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm.Dispose();
            }

            activeForm = childForm;

            // 2. Configure the Form to act as a Control
            childForm.TopLevel = false;          // Critical for hosting inside another form
            childForm.FormBorderStyle = FormBorderStyle.None; // Remove window borders
            childForm.Dock = DockStyle.Fill;     // Fill the remaining space

            // 3. Add to the main panel
            pnlMainContainer.Controls.Add(childForm);
            pnlMainContainer.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }

        // Example usage for your 'Show Client List' button
        private void btnShowClientList_Click(object sender, EventArgs e)
        {
            // Pass the form you created for the Client List
            //OpenChildForm(new frmClientList());
        }
        public frmDashboard()
        {
            InitializeComponent();
        }
    }
}
