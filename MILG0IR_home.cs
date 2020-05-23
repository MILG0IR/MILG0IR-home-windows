using System;
using System.CodeDom;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MILG0IR_home_windows;
using MILG0IR_home_windows.functions;
using MILG0IR_home_windows.Properties;

namespace MILG0IR_home_windows_x64 {
    public partial class MILG0IR_home : Form {
        #region Windows effects for borderless window
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private bool m_aeroEnabled;
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        public struct MARGINS {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams {
            get {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW; return cp;
            }
        }
        private bool CheckAeroEnabled() {
            if (Environment.OSVersion.Version.Major >= 6) {
                int enabled = 0; DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m) {
            const int RESIZE_HANDLE_SIZE = 10;

            switch (m.Msg) {
                case 0x0084:
                    base.WndProc(ref m);
                    if ((int)m.Result == HTCLIENT && WindowState != FormWindowState.Maximized) {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = PointToClient(screenPoint);
                        if (clientPoint.Y <= RESIZE_HANDLE_SIZE) {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)12/*HTTOP*/ ;
                            else
                                m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                        } else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE)) {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)10/*HTLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)2/*HTCAPTION*/ ;
                            else
                                m.Result = (IntPtr)11/*HTRIGHT*/ ;
                        } else {
                            if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                            else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                            else
                                m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                        }
                    }
                    return;
                case WM_NCPAINT:
                    base.WndProc(ref m);
                    if (m_aeroEnabled) {
                        var v = 2;
                        DwmSetWindowAttribute(Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS() {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        }; DwmExtendFrameIntoClientArea(Handle, ref margins);
                    }
                    break;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
        }
        #endregion
        readonly Initialize MILG0IR_init = new Initialize();
        readonly Functions MILG0IR = new Functions();
        public Boolean MenuStatus = false;
        public Boolean MenuStatus_hover = false;

        public MILG0IR_home() { InitializeComponent(); }
        private void MILG0IR_home_Load(object sender, EventArgs e) {
            PrivateFontCollection pfc = new PrivateFontCollection();
            int fontLength = Resources.segoe_mdl2_assets.Length;
            byte[] fontdata = Resources.segoe_mdl2_assets;
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);

            FormBorderStyle = FormBorderStyle.None;
            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            if (Settings.Default.WindowSize == new Size(0, 0)) {
                var len = Screen.PrimaryScreen.WorkingArea.Width / 2;
                var wid = Screen.PrimaryScreen.WorkingArea.Height / 2;
                Size = new Size(len, wid);
            } else {
                Size = Settings.Default.WindowSize;
            }
            if (Settings.Default.WindowLocation == new Point(0, 0)) {
                var x = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (Width / 2);
                var y = (Screen.PrimaryScreen.WorkingArea.Height / 2) - (Height / 2);
                Location = new Point(x, y);
            } else {
                Location = Settings.Default.WindowLocation;
            }
            if (Settings.Default.WindowState == FormWindowState.Maximized) {
                Padding = new Padding(0, 0, 0, 0);
            } else {
                Padding = new Padding(2, 0, 2, 2);
            }
            WindowState = Settings.Default.WindowState;
            MILG0IR_init.Create_title_bar("MG_time", true, true, true);
        }
        private void MILG0IR_home_FormClosed(object sender, FormClosedEventArgs e) {
            MILG0IR.Close();
        }
        private void MILG0IR_home_ResizeEnd(object sender, EventArgs e) {
            Settings.Default.WindowSize = Size;
        }
        private void MILG0IR_home_Resize(object sender, EventArgs e) {
            if (WindowState != FormWindowState.Maximized) {
                Padding = new Padding(2, 0, 2, 2);
            } else {
                Padding = new Padding(0, 0, 0, 0);
            }
        }
        private void MILG0IR_home_Move(object sender, EventArgs e) {
            Settings.Default.WindowLocation = Location;
        }
        private void Toggle_Menu(object sender, EventArgs e) {
            if (MenuStatus) {
                MenuStatus = false;
                MG_Menu.Width = 50;
            } else {
                MenuStatus = true;
                MG_Menu.Width = 250;
            }
        }
        private void Hover_Menu(object sender, EventArgs e) {
            if(!MenuStatus) {
                if (MenuStatus_hover) {
                    MenuStatus_hover = false;
                    MG_Menu.Width = 50;
                } else {
                    MenuStatus_hover = true;
                    MG_Menu.Width = 250;
                }
            }
        }
        private void MG_Menu_Paint(object sender, PaintEventArgs e) {
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(MG_Menu.ClientRectangle, Color.FromArgb(225, 78, 202), Color.FromArgb(186, 84, 245), 90);

            ColorBlend cblend = new ColorBlend(2);
            cblend.Colors = new Color[2] { Color.FromArgb(225, 78, 202), Color.FromArgb(186, 84, 245) };
            cblend.Positions = new float[2] { 0f, 1f };

            linearGradientBrush.InterpolationColors = cblend;

            e.Graphics.FillRectangle(linearGradientBrush, MG_Menu.ClientRectangle);
        }
        private void Button1_Click(object sender, EventArgs e) {
            new MILG0IR_initialize().Show();
        }










    }
}
