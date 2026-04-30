using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BankManagement
{
    internal static class clsModernTheme
    {
        public static readonly Color AppBackground = Color.FromArgb(246, 248, 252);
        public static readonly Color Surface = Color.White;
        public static readonly Color SurfaceSoft = Color.FromArgb(238, 243, 249);
        public static readonly Color Border = Color.FromArgb(218, 226, 236);
        public static readonly Color Text = Color.FromArgb(26, 32, 44);
        public static readonly Color MutedText = Color.FromArgb(100, 116, 139);
        public static readonly Color Sidebar = Color.FromArgb(15, 23, 42);
        public static readonly Color SidebarSoft = Color.FromArgb(30, 41, 59);
        public static readonly Color Accent = Color.FromArgb(37, 99, 235);
        public static readonly Color AccentSoft = Color.FromArgb(219, 234, 254);
        public static readonly Color Success = Color.FromArgb(16, 185, 129);
        public static readonly Color Warning = Color.FromArgb(245, 158, 11);
        public static readonly Color Danger = Color.FromArgb(239, 68, 68);

        private static readonly HashSet<int> _styledControls = new HashSet<int>();

        public static void Apply(Control root)
        {
            if (root == null)
                return;

            StyleControlTree(root);
        }

        private static void StyleControlTree(Control control)
        {
            if (control.Tag is string tag && tag == "SkipModernTheme")
                return;

            int controlKey = RuntimeHelpers.GetHashCode(control);

            if (!_styledControls.Contains(controlKey))
            {
                _styledControls.Add(controlKey);
                StyleControl(control);
                control.ControlAdded += (sender, e) => StyleControlTree(e.Control);
            }

            foreach (Control child in control.Controls)
            {
                StyleControlTree(child);
            }
        }

        private static void StyleControl(Control control)
        {
            if (control is Form form)
            {
                form.BackColor = AppBackground;
                form.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                return;
            }

            if (control is Guna2DataGridView gunaGrid)
            {
                StyleGrid(gunaGrid);
                return;
            }

            if (control is DataGridView grid)
            {
                StyleGrid(grid);
                return;
            }

            if (control is Guna2Button button)
            {
                StyleButton(button);
                return;
            }

            if (control is Guna2TextBox textBox)
            {
                textBox.BorderRadius = 8;
                textBox.BorderThickness = 1;
                textBox.BorderColor = Border;
                textBox.FillColor = Surface;
                textBox.FocusedState.BorderColor = Accent;
                textBox.HoverState.BorderColor = Accent;
                textBox.ForeColor = Text;
                textBox.PlaceholderForeColor = MutedText;
                textBox.Font = new Font("Segoe UI", 9.25F);
                return;
            }

            if (control is Guna2ComboBox comboBox)
            {
                comboBox.BorderRadius = 8;
                comboBox.BorderThickness = 1;
                comboBox.BorderColor = Border;
                comboBox.FillColor = Surface;
                comboBox.FocusedState.BorderColor = Accent;
                comboBox.ForeColor = Text;
                comboBox.Font = new Font("Segoe UI", 9.25F);
                return;
            }

            if (control is Guna2Panel panel)
            {
                StylePanel(panel);
                return;
            }

            if (control is Guna2ShadowPanel shadowPanel)
            {
                shadowPanel.Radius = 8;
                shadowPanel.FillColor = Surface;
                shadowPanel.ShadowColor = Color.FromArgb(180, 190, 205);
                return;
            }

            if (control is Guna2HtmlLabel htmlLabel)
            {
                htmlLabel.Font = ModernFont(htmlLabel.Font);
                htmlLabel.ForeColor = IsInDarkContainer(htmlLabel) ? Color.White : Text;
                return;
            }

            if (control is Label label)
            {
                label.Font = ModernFont(label.Font);
                label.ForeColor = IsInDarkContainer(label) ? Color.White : Text;
                return;
            }

            if (control is GroupBox groupBox)
            {
                groupBox.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
                groupBox.ForeColor = Text;
                groupBox.BackColor = AppBackground;
                return;
            }
        }

        private static void StyleButton(Guna2Button button)
        {
            bool isNavigationButton = button.Parent != null && button.Parent.Name == "pnlSideBar";
            bool isLogout = button.Name.IndexOf("logout", StringComparison.OrdinalIgnoreCase) >= 0;

            button.BorderRadius = isNavigationButton ? 10 : 8;
            button.BorderThickness = isNavigationButton ? 0 : 1;
            button.Font = new Font("Segoe UI Semibold", button.Font.Size < 10 ? 9.5F : button.Font.Size, FontStyle.Bold);

            if (isLogout)
            {
                button.FillColor = Danger;
                button.BorderColor = Danger;
                button.HoverState.FillColor = Color.FromArgb(220, 38, 38);
                button.ForeColor = Color.White;
                return;
            }

            if (isNavigationButton)
            {
                button.FillColor = Sidebar;
                button.HoverState.FillColor = SidebarSoft;
                button.ForeColor = Color.FromArgb(226, 232, 240);
                return;
            }

            button.FillColor = Accent;
            button.BorderColor = Accent;
            button.HoverState.FillColor = Color.FromArgb(29, 78, 216);
            button.ForeColor = Color.White;
        }

        private static void StylePanel(Guna2Panel panel)
        {
            if (panel.Name == "pnlSideBar")
            {
                panel.FillColor = Sidebar;
                panel.BackColor = Sidebar;
                return;
            }

            if (panel.Name == "pnlMainContainer")
            {
                panel.FillColor = AppBackground;
                panel.BackColor = AppBackground;
                return;
            }

            if (panel.Dock == DockStyle.Top || panel.Dock == DockStyle.Bottom)
            {
                panel.FillColor = Surface;
                panel.BackColor = Surface;
                panel.BorderColor = Border;
                panel.BorderThickness = 1;
                return;
            }

            if (panel.FillColor == Color.Empty || panel.FillColor == Color.Transparent || panel.FillColor == Color.Silver || panel.FillColor == Color.LightGray)
            {
                panel.FillColor = Surface;
            }

            if (panel.BackColor == Color.Transparent || panel.BackColor == Color.Silver || panel.BackColor == Color.LightGray)
            {
                panel.BackColor = AppBackground;
            }
        }

        private static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Surface;
            grid.BorderStyle = BorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = Color.FromArgb(226, 232, 240);
            grid.RowTemplate.Height = Math.Max(grid.RowTemplate.Height, 32);
            grid.ColumnHeadersHeight = Math.Max(grid.ColumnHeadersHeight, 36);
            grid.ColumnHeadersDefaultCellStyle.BackColor = Sidebar;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            grid.DefaultCellStyle.BackColor = Surface;
            grid.DefaultCellStyle.ForeColor = Text;
            grid.DefaultCellStyle.SelectionBackColor = AccentSoft;
            grid.DefaultCellStyle.SelectionForeColor = Text;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        }

        private static void StyleGrid(Guna2DataGridView grid)
        {
            StyleGrid((DataGridView)grid);
            grid.ThemeStyle.BackColor = Surface;
            grid.ThemeStyle.GridColor = Color.FromArgb(226, 232, 240);
            grid.ThemeStyle.HeaderStyle.BackColor = Sidebar;
            grid.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            grid.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            grid.ThemeStyle.RowsStyle.BackColor = Surface;
            grid.ThemeStyle.RowsStyle.ForeColor = Text;
            grid.ThemeStyle.RowsStyle.SelectionBackColor = AccentSoft;
            grid.ThemeStyle.RowsStyle.SelectionForeColor = Text;
            grid.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(248, 250, 252);
        }

        private static Font ModernFont(Font font)
        {
            FontStyle style = font.Style;
            float size = font.Size;

            if (font.Name.Equals("Tahoma", StringComparison.OrdinalIgnoreCase))
                return new Font("Segoe UI", size, style);

            return font;
        }

        private static bool IsInDarkContainer(Control control)
        {
            Control parent = control.Parent;
            while (parent != null)
            {
                Color color = parent.BackColor;

                if (parent is Guna2Panel panel && panel.FillColor != Color.Empty && panel.FillColor != Color.Transparent)
                    color = panel.FillColor;

                if (parent is Guna2ShadowPanel shadowPanel && shadowPanel.FillColor != Color.Empty && shadowPanel.FillColor != Color.Transparent)
                    color = shadowPanel.FillColor;

                if (color != Color.Empty && color != Color.Transparent && GetBrightness(color) < 95)
                    return true;

                parent = parent.Parent;
            }

            return false;
        }

        private static double GetBrightness(Color color)
        {
            return (0.299 * color.R) + (0.587 * color.G) + (0.114 * color.B);
        }
    }
}
