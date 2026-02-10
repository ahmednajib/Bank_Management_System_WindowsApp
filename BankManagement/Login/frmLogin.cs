using System;
using System.Windows.Forms;

namespace BankManagement
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Close the application
            Application.Exit();
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {
            // 2. Set the current date on load
            lblDate.Text = "Date: " + DateTime.Now.ToString("d/M/yyyy");
        }
    }
}