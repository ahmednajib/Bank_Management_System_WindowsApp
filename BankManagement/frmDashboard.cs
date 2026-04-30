using BankManagement.Clients;
using BankManagement.Clients.Add_Update_Client;
using BankManagement.Clients.ManageClients;
using BankManagement.Dashboard;
using BankManagement.ManageAccounts_Transactions;
using BankManagement.Users;
using Classes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BankManagement
{
    public partial class frmDashboard : Form
    {
        private Form activeForm = null;
        private ctrlDashboardStatistics _dashboardStatistics;

        // Define your colors here so you can change them easily in one place
        private readonly Color SidebarButtonDefaultColor = clsModernTheme.Sidebar;
        private readonly Color SidebarButtonActiveColor = clsModernTheme.Accent;

        private void LayoutSidebarControls()
        {
            int width = pnlSideBar.ClientSize.Width;
            int margin = 12;
            int buttonHeight = 40;
            int buttonWidth = width - (margin * 2);
            int currentY = 116;

            guna2PictureBox1.Size = new Size(82, 82);
            guna2PictureBox1.Location = new Point((width - guna2PictureBox1.Width) / 2, 14);

            guna2Separator1.Location = new Point(margin, 106);
            guna2Separator1.Size = new Size(buttonWidth, 10);

            PlaceSidebarButton(btnManageClients, margin, ref currentY, buttonWidth, buttonHeight);
            PlaceSidebarButton(btnFindClient, margin, ref currentY, buttonWidth, buttonHeight);
            PlaceSidebarButton(guna2Button3, margin, ref currentY, buttonWidth, buttonHeight);
            PlaceSidebarButton(btnManageUsers, margin, ref currentY, buttonWidth, buttonHeight);

            int bottomY = pnlSideBar.ClientSize.Height - 58;
            btnLogout.Location = new Point(margin, Math.Max(currentY + 18, bottomY));
            btnLogout.Size = new Size(buttonWidth, buttonHeight);

            btnCurrencyExchange.Location = new Point(margin, btnLogout.Top - 48);
            btnCurrencyExchange.Size = new Size(buttonWidth, buttonHeight);

            guna2Button2.Location = new Point(margin, btnCurrencyExchange.Top - 48);
            guna2Button2.Size = new Size(buttonWidth, buttonHeight);

            guna2HtmlLabel2.Location = new Point(margin, guna2Button2.Top - 34);

            guna2Separator2.Location = new Point(margin, guna2HtmlLabel2.Top - 18);
            guna2Separator2.Size = new Size(buttonWidth, 10);
        }

        private void PlaceSidebarButton(Guna.UI2.WinForms.Guna2Button button, int margin, ref int y, int width, int height)
        {
            button.Location = new Point(margin, y);
            button.Size = new Size(width, height);
            y += height + 10;
        }

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
            childForm.Dock = DockStyle.Fill;

            // 6. Add and show
            pnlMainContainer.Controls.Add(childForm);
            pnlMainContainer.Tag = childForm;
            clsModernTheme.Apply(childForm);
            childForm.Show();
        }

        private void ShowDashboardStatistics()
        {
            ResetButtonColors();
            CloseFloatingForms();

            if (activeForm != null)
            {
                activeForm.Close();
                activeForm = null;
            }

            pnlMainContainer.Controls.Clear();

            _dashboardStatistics = new ctrlDashboardStatistics();
            _dashboardStatistics.RefreshStatistics();
            pnlMainContainer.Controls.Add(_dashboardStatistics);
        }

        private void CloseFloatingForms()
        {
            Form[] openForms = new Form[Application.OpenForms.Count];

            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                openForms[i] = Application.OpenForms[i];
            }

            foreach (Form form in openForms)
            {
                if (form == this || form == _LoginForm)
                    continue;

                form.Close();
            }
        }


        frmLogin _LoginForm;
        private bool isLoggingOut = false;

        public frmDashboard(frmLogin LoginForm)
        {
            InitializeComponent();
            _LoginForm = LoginForm;
            WindowState = FormWindowState.Maximized;
            clsModernTheme.Apply(this);
            LayoutSidebarControls();
            ShowDashboardStatistics();
        }

        private void btnManageClients_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button clickedButton = (Guna.UI2.WinForms.Guna2Button)sender;
            // Pass the form you created for the Client List
            OpenChildForm(new frmManageClients(), clickedButton);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            isLoggingOut = true;
            clsGlobal.CurrentUser = null;
            _LoginForm.Show();
            this.Close();
        }

        private void btnFindClient_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button clickedButton = (Guna.UI2.WinForms.Guna2Button)sender;

            // Pass the form you created for the Client List
            OpenChildForm(new frmFindClient(), clickedButton);
        }

        private void frmDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isLoggingOut)
            {
                // If the form is closed without logging out, we exit the application.
                // This is to ensure that the application does not remain running in the background.
                Application.Exit();
            }
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button clickedButton = (Guna.UI2.WinForms.Guna2Button)sender;
            
            // Pass the form you created for the Client List
            OpenChildForm(new frmListUsers(), clickedButton);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button clickedButton = (Guna.UI2.WinForms.Guna2Button)sender;

            // Pass the form you created for the Client List
            OpenChildForm(new frmUserInfo(clsGlobal.CurrentUser.UserID), clickedButton);
        }

        private void btnCurrencyExchange_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button clickedButton = (Guna.UI2.WinForms.Guna2Button)sender;

            // Pass the form you created for the Client List
            OpenChildForm(new frmChangePassword(clsGlobal.CurrentUser.UserID), clickedButton);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button clickedButton = (Guna.UI2.WinForms.Guna2Button)sender;

            // Pass the form you created for the Client List
            OpenChildForm(new frmManageAccounts_Transactions(), clickedButton);
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            ShowDashboardStatistics();
        }

        private void frmDashboard_Resize(object sender, EventArgs e)
        {
            LayoutSidebarControls();
        }
    }
}
