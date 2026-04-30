using BLL_BankManagement;
using Classes;
using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace BankManagement.Dashboard
{
    public class ctrlDashboardStatistics : UserControl
    {
        private readonly TableLayoutPanel _layout = new TableLayoutPanel();
        private readonly FlowLayoutPanel _cardRow = new FlowLayoutPanel();
        private readonly Guna2Panel _heroPanel = new Guna2Panel();
        private readonly Guna2Panel _overviewPanel = new Guna2Panel();
        private readonly Guna2Panel _healthPanel = new Guna2Panel();
        private readonly Label _lblGreeting = new Label();
        private readonly Label _lblSubtitle = new Label();
        private readonly Label _lblUpdated = new Label();
        private readonly Label _lblActiveClients = new Label();
        private readonly Label _lblActiveAccounts = new Label();
        private readonly Label _lblActiveUsers = new Label();
        private readonly Guna2ProgressBar _pbClients = new Guna2ProgressBar();
        private readonly Guna2ProgressBar _pbAccounts = new Guna2ProgressBar();
        private readonly Guna2ProgressBar _pbUsers = new Guna2ProgressBar();

        public ctrlDashboardStatistics()
        {
            Tag = "SkipModernTheme";
            InitializeDashboard();
            Load += (sender, e) => RefreshStatistics();
        }

        public void RefreshStatistics()
        {
            DataTable clients = clsClient.GetAllClients();
            DataTable accounts = clsAccount.GetAllAccounts();
            DataTable users = clsUser.GetAllUsers();

            int clientsCount = clients.Rows.Count;
            int accountsCount = accounts.Rows.Count;
            int usersCount = users.Rows.Count;
            int activeClients = CountActive(clients, "IsActive");
            int activeAccounts = CountActive(accounts, "IsActive");
            int activeUsers = CountActive(users, "IsActive");
            decimal totalBalance = SumDecimal(accounts, "Balance");

            string userName = clsGlobal.CurrentUser != null ? clsGlobal.CurrentUser.UserName : "User";
            _lblGreeting.Text = "Welcome back, " + userName;
            _lblUpdated.Text = "Updated " + DateTime.Now.ToString("MMM d, yyyy  h:mm tt", CultureInfo.CurrentCulture);

            _cardRow.Controls.Clear();
            _cardRow.Controls.Add(CreateStatCard("Clients", clientsCount.ToString("N0"), activeClients + " active", clsModernTheme.Accent));
            _cardRow.Controls.Add(CreateStatCard("Accounts", accountsCount.ToString("N0"), activeAccounts + " active", clsModernTheme.Success));
            _cardRow.Controls.Add(CreateStatCard("Total Balance", totalBalance.ToString("C0"), "Across all accounts", Color.FromArgb(14, 165, 233)));
            _cardRow.Controls.Add(CreateStatCard("Users", usersCount.ToString("N0"), activeUsers + " active", clsModernTheme.Warning));

            SetProgress(_pbClients, clientsCount, activeClients);
            SetProgress(_pbAccounts, accountsCount, activeAccounts);
            SetProgress(_pbUsers, usersCount, activeUsers);

            _lblActiveClients.Text = ActiveText(activeClients, clientsCount, "clients");
            _lblActiveAccounts.Text = ActiveText(activeAccounts, accountsCount, "accounts");
            _lblActiveUsers.Text = ActiveText(activeUsers, usersCount, "users");
        }

        private void InitializeDashboard()
        {
            BackColor = clsModernTheme.AppBackground;
            Dock = DockStyle.Fill;
            Padding = new Padding(24);

            _layout.Dock = DockStyle.Fill;
            _layout.BackColor = clsModernTheme.AppBackground;
            _layout.ColumnCount = 1;
            _layout.RowCount = 3;
            _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            _layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 190F));
            _layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Controls.Add(_layout);

            BuildHero();
            BuildCards();
            BuildLowerPanels();
        }

        private void BuildHero()
        {
            _heroPanel.BorderRadius = 8;
            _heroPanel.BackColor = clsModernTheme.Sidebar;
            _heroPanel.FillColor = clsModernTheme.Sidebar;
            _heroPanel.BorderColor = clsModernTheme.Sidebar;
            _heroPanel.BorderThickness = 1;
            _heroPanel.Dock = DockStyle.Fill;
            _heroPanel.Margin = new Padding(0, 0, 0, 18);
            _heroPanel.Padding = new Padding(28, 22, 28, 20);

            _lblGreeting.AutoSize = false;
            _lblGreeting.Dock = DockStyle.Top;
            _lblGreeting.Height = 44;
            _lblGreeting.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            _lblGreeting.ForeColor = Color.White;
            _lblGreeting.BackColor = clsModernTheme.Sidebar;

            _lblSubtitle.AutoSize = false;
            _lblSubtitle.Dock = DockStyle.Top;
            _lblSubtitle.Height = 30;
            _lblSubtitle.Font = new Font("Segoe UI", 10.5F);
            _lblSubtitle.ForeColor = Color.FromArgb(203, 213, 225);
            _lblSubtitle.BackColor = clsModernTheme.Sidebar;
            _lblSubtitle.Text = "Live overview of your bank clients, accounts, balances, and users.";

            _lblUpdated.AutoSize = false;
            _lblUpdated.Dock = DockStyle.Bottom;
            _lblUpdated.Height = 24;
            _lblUpdated.Font = new Font("Segoe UI", 9.5F);
            _lblUpdated.ForeColor = Color.FromArgb(148, 163, 184);
            _lblUpdated.BackColor = clsModernTheme.Sidebar;

            _heroPanel.Controls.Add(_lblUpdated);
            _heroPanel.Controls.Add(_lblSubtitle);
            _heroPanel.Controls.Add(_lblGreeting);
            _layout.Controls.Add(_heroPanel, 0, 0);
        }

        private void BuildCards()
        {
            _cardRow.Dock = DockStyle.Fill;
            _cardRow.BackColor = clsModernTheme.AppBackground;
            _cardRow.WrapContents = true;
            _cardRow.Margin = new Padding(0, 0, 0, 12);
            _cardRow.AutoScroll = false;
            _layout.Controls.Add(_cardRow, 0, 1);
        }

        private void BuildLowerPanels()
        {
            TableLayoutPanel lowerLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = clsModernTheme.AppBackground
            };
            lowerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56F));
            lowerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
            _layout.Controls.Add(lowerLayout, 0, 2);

            BuildOverviewPanel();
            BuildHealthPanel();

            lowerLayout.Controls.Add(_overviewPanel, 0, 0);
            lowerLayout.Controls.Add(_healthPanel, 1, 0);
        }

        private void BuildOverviewPanel()
        {
            _overviewPanel.BorderRadius = 8;
            _overviewPanel.FillColor = clsModernTheme.Surface;
            _overviewPanel.BorderColor = clsModernTheme.Border;
            _overviewPanel.BorderThickness = 1;
            _overviewPanel.Dock = DockStyle.Fill;
            _overviewPanel.Margin = new Padding(0, 0, 10, 0);
            _overviewPanel.Padding = new Padding(24);

            Label title = CreatePanelTitle("Operational Health");
            _overviewPanel.Controls.Add(CreateProgressBlock("Active Clients", _lblActiveClients, _pbClients, clsModernTheme.Accent));
            _overviewPanel.Controls.Add(CreateProgressBlock("Active Accounts", _lblActiveAccounts, _pbAccounts, clsModernTheme.Success));
            _overviewPanel.Controls.Add(CreateProgressBlock("Active Users", _lblActiveUsers, _pbUsers, clsModernTheme.Warning));
            _overviewPanel.Controls.Add(title);
        }

        private void BuildHealthPanel()
        {
            _healthPanel.BorderRadius = 8;
            _healthPanel.FillColor = clsModernTheme.Surface;
            _healthPanel.BorderColor = clsModernTheme.Border;
            _healthPanel.BorderThickness = 1;
            _healthPanel.Dock = DockStyle.Fill;
            _healthPanel.Margin = new Padding(10, 0, 0, 0);
            _healthPanel.Padding = new Padding(24);

            Label title = CreatePanelTitle("Quick Actions");
            Label message = new Label
            {
                Dock = DockStyle.Top,
                Height = 96,
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = clsModernTheme.MutedText,
                Text = "Use the sidebar to manage clients, search records, maintain accounts, review users, or update the current user profile.",
                TextAlign = ContentAlignment.TopLeft
            };

            Guna2Panel accentStrip = new Guna2Panel
            {
                Dock = DockStyle.Top,
                Height = 86,
                BorderRadius = 8,
                FillColor = clsModernTheme.AccentSoft,
                Margin = new Padding(0, 12, 0, 0),
                Padding = new Padding(18)
            };

            Label hint = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = clsModernTheme.Accent,
                Text = "Click the bank logo anytime to return here and refresh the statistics.",
                TextAlign = ContentAlignment.MiddleLeft
            };

            accentStrip.Controls.Add(hint);
            _healthPanel.Controls.Add(accentStrip);
            _healthPanel.Controls.Add(message);
            _healthPanel.Controls.Add(title);
        }

        private Guna2Panel CreateStatCard(string title, string value, string subtitle, Color accent)
        {
            Guna2Panel card = new Guna2Panel
            {
                BorderRadius = 8,
                BorderThickness = 1,
                BorderColor = clsModernTheme.Border,
                FillColor = clsModernTheme.Surface,
                Size = new Size(250, 154),
                Margin = new Padding(0, 0, 16, 16),
                Padding = new Padding(18)
            };

            Guna2Panel marker = new Guna2Panel
            {
                BorderRadius = 4,
                FillColor = accent,
                Size = new Size(42, 6),
                Location = new Point(18, 18)
            };

            Label lblTitle = new Label
            {
                AutoSize = false,
                Location = new Point(18, 38),
                Size = new Size(214, 24),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                ForeColor = clsModernTheme.MutedText,
                Text = title
            };

            Label lblValue = new Label
            {
                AutoSize = false,
                Location = new Point(18, 66),
                Size = new Size(214, 42),
                Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold),
                ForeColor = clsModernTheme.Text,
                Text = value
            };

            Label lblSubtitle = new Label
            {
                AutoSize = false,
                Location = new Point(18, 115),
                Size = new Size(214, 24),
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = clsModernTheme.MutedText,
                Text = subtitle
            };

            card.Controls.Add(lblSubtitle);
            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);
            card.Controls.Add(marker);
            return card;
        }

        private Control CreateProgressBlock(string title, Label valueLabel, Guna2ProgressBar progressBar, Color color)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 76,
                BackColor = clsModernTheme.Surface,
                Padding = new Padding(0, 10, 0, 0)
            };

            Label titleLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 22,
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                ForeColor = clsModernTheme.Text,
                Text = title
            };

            valueLabel.Dock = DockStyle.Top;
            valueLabel.Height = 20;
            valueLabel.Font = new Font("Segoe UI", 9F);
            valueLabel.ForeColor = clsModernTheme.MutedText;

            progressBar.Dock = DockStyle.Bottom;
            progressBar.Height = 9;
            progressBar.BorderRadius = 4;
            progressBar.FillColor = Color.FromArgb(226, 232, 240);
            progressBar.ProgressColor = color;
            progressBar.ProgressColor2 = color;

            panel.Controls.Add(progressBar);
            panel.Controls.Add(valueLabel);
            panel.Controls.Add(titleLabel);
            return panel;
        }

        private Label CreatePanelTitle(string text)
        {
            return new Label
            {
                Dock = DockStyle.Top,
                Height = 42,
                Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold),
                ForeColor = clsModernTheme.Text,
                Text = text
            };
        }

        private static void SetProgress(Guna2ProgressBar progressBar, int total, int active)
        {
            progressBar.Value = total == 0 ? 0 : Math.Min(100, Math.Max(0, (active * 100) / total));
        }

        private static string ActiveText(int active, int total, string label)
        {
            int percent = total == 0 ? 0 : (active * 100) / total;
            return active + " of " + total + " " + label + " active (" + percent + "%)";
        }

        private static int CountActive(DataTable table, string columnName)
        {
            if (!table.Columns.Contains(columnName))
                return 0;

            return table.AsEnumerable().Count(row => IsActiveValue(row[columnName]));
        }

        private static bool IsActiveValue(object value)
        {
            if (value == null || value == DBNull.Value)
                return false;

            if (value is bool boolValue)
                return boolValue;

            string text = value.ToString().Trim();
            return text.Equals("true", StringComparison.OrdinalIgnoreCase)
                || text.Equals("active", StringComparison.OrdinalIgnoreCase)
                || text.Equals("yes", StringComparison.OrdinalIgnoreCase);
        }

        private static decimal SumDecimal(DataTable table, string columnName)
        {
            if (!table.Columns.Contains(columnName))
                return 0;

            decimal total = 0;
            foreach (DataRow row in table.Rows)
            {
                if (row[columnName] != DBNull.Value)
                    total += Convert.ToDecimal(row[columnName], CultureInfo.InvariantCulture);
            }

            return total;
        }
    }
}
