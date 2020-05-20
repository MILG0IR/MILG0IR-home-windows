using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using MILG0IR_home_windows_x64.functions;
using MILG0IR_home_windows_x64.Properties;

namespace MILG0IR_home_windows_x64 {
    public partial class MILG0IR_home : Form {
        readonly Functions MILG0IR = new Functions();
        public MILG0IR_home() { InitializeComponent(); }
        private void MILG0IR_home_Load(object sender, EventArgs e) {
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.FromArgb(29, 29, 44);
            var len = Screen.PrimaryScreen.WorkingArea.Width / 2;
            var wid = Screen.PrimaryScreen.WorkingArea.Height / 2;
            Size = new Size(len, wid);
            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            var pos = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (Width / 2), (Screen.PrimaryScreen.WorkingArea.Height / 2) - (Height / 2));
            Location = pos;
            MILG0IR.create_title_bar("MG_time", true, true, true);
            init();
        }
        private void init() { }
        protected override void WndProc(ref Message m) {
            const int RESIZE_HANDLE_SIZE = 10;
            switch (m.Msg) {
                case 0x0084:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x01/*HTCLIENT*/ && WindowState != FormWindowState.Maximized) {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
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
            }
            base.WndProc(ref m);
        }
    }
}
