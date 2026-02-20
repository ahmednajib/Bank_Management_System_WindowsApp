using BankManagement.Clients;
using BankManagement.Clients.Add_Update_Client;
using BankManagement.Clients.ManageClients;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BankManagement
{
    public partial class frmDashboard : Form
    {
        private Form activeForm = null;

        // Define your colors here so you can change them easily in one place
        private readonly Color SidebarButtonDefaultColor = Color.FromArgb(45, 45, 45); // Your normal sidebar color
        private readonly Color SidebarButtonActiveColor = Color.DimGray; // Your highlight color

        private void ResetButtonColors()
        {
            // This assumes all your navigation buttons are inside a panel (e.g., pnlSidebar)
            // It loops through them and sets them back to the default color
            foreach (Control ctrl in pnlSideBar.Controls)
            {
                if (ctrl is Guna.UI2.WinForms.Guna2Button btn)
                {
                    if (!(btn.Name == "btnLogout"))
                        btn.FillColor = SidebarButtonDefaultColor;
                }
            }
        }

        private void OpenChildForm(Form childForm, Guna.UI2.WinForms.Guna2Button btn)
        {
            // 1. Reset all buttons first
            ResetButtonColors();

            // 2. Highlight the one that was just clicked
            btn.FillColor = SidebarButtonActiveColor;

            // 3. Close the previous form to save memory
            if (activeForm != null)
                activeForm.Close();

            this.activeForm = childForm;

            // 4. Clear the container panel
            if (pnlMainContainer.Controls.Count > 0)
                pnlMainContainer.Controls[0].Dispose();

            // 5. Setup basic properties for the child "Card" form
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.None;
            childForm.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // This ensures the form fits perfectly inside the padding you set
            childForm.Bounds = pnlMainContainer.DisplayRectangle;

            // 6. Add and show
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
            Guna.UI2.WinForms.Guna2Button clickedButton = (Guna.UI2.WinForms.Guna2Button)sender;
            // Pass the form you created for the Client List
            OpenChildForm(new frmManageClients(), clickedButton);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            this.Close();
        }

        private void btnFindClient_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button clickedButton = (Guna.UI2.WinForms.Guna2Button)sender;

            // Pass the form you created for the Client List
            OpenChildForm(new frmFindClient(), clickedButton);
        }
    }
}