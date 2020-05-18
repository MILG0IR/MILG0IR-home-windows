using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MILG0IR_home_windows_x64.functions;
using MILG0IR_home_windows_x64.Properties;

namespace MILG0IR_home_windows_x64 {
    public partial class MILG0IR_login : Form {
        readonly Functions MILG0IR = new Functions();
        public Button submitBtn;
        public TextBox username;
        public TextBox password;
        public MILG0IR_login() { InitializeComponent(); }
        private void MILG0IR_home_Load(object sender, EventArgs e) {
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.FromArgb(29, 29, 44);
            Size = new Size(450, 750);
            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            var pos = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (Width / 2), (Screen.PrimaryScreen.WorkingArea.Height / 2) - (Height / 2));
            Location = pos;
            MILG0IR.create_title_bar("", true, false, true);
            var backPanel = new Panel {
                Size = new Size(Width, Height - 25),
                Dock = DockStyle.Bottom
            };
            var welcome = new System.Windows.Forms.Label {
                AutoSize = true,
                MaximumSize = new Size(Width - 50, 0),
                Text = "Hello.\nWelcome home.",
                ForeColor = Color.FromArgb(255, 255, 255),
                Font = new Font("ubuntu", 30, FontStyle.Regular),
                Location = new Point(25, Convert.ToInt32(backPanel.Height * 0.2))
            };
            var info = new System.Windows.Forms.Label {
                AutoSize = true,
                MaximumSize = new Size(Width - 50, 0),
                Font = new Font("ubuntu", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(255, 255, 255),
                Location = new Point(25, Convert.ToInt32(backPanel.Height * 0.35)),
                Text = "Log in to accesss your new home."
            };
            username = new TextBox {
                AutoSize = true,
                Font = new Font("Arial", 20, FontStyle.Regular),
                BackColor = Color.FromArgb(29, 29, 44),
                ForeColor = Color.FromArgb(255, 255, 255),
                BorderStyle = BorderStyle.None,
                Width = backPanel.Width - 50,
                Location = new Point(25, Convert.ToInt32(backPanel.Height * 0.45)),
                Text = "Server URL"
            };
            var underline1 = new Panel() {
                Location = new Point(25, (username.Location.Y + username.Height) + 1),
                BackColor = Color.FromArgb(43, 53, 83),
                Height = 2,
                Width = backPanel.Width - 50
            };
            password = new TextBox {
                AutoSize = true,
                Font = new Font("Arial", 20, FontStyle.Regular),
                BackColor = Color.FromArgb(29, 29, 44),
                ForeColor = Color.FromArgb(255, 255, 255),
                BorderStyle = BorderStyle.None,
                Width = backPanel.Width - 50,
                Location = new Point(25, Convert.ToInt32(backPanel.Height * 0.55)),
                Text = "Password"
            };
            var underline2 = new Panel() {
                Location = new Point(25, (password.Location.Y + password.Height) + 1),
                BackColor = Color.FromArgb(43, 53, 83),
                Height = 2,
                Width = backPanel.Width - 50
            };
            submitBtn = new Button {
                AutoSize = true,
                Width = backPanel.Width - (Width / 2),
                Font = new Font("Arial", 20, FontStyle.Regular),
                Location = new Point((Width / 4), Convert.ToInt32(backPanel.Height * 0.65)),
                ForeColor = Color.FromArgb(255, 255, 255),
                FlatStyle = FlatStyle.Flat,
                Text = "Connect",
                TabStop = false,
            };
            submitBtn.Click += new EventHandler(Connx);
            Controls.Add(backPanel);
            backPanel.Controls.Add(welcome);
            backPanel.Controls.Add(info);
            backPanel.Controls.Add(username);
            backPanel.Controls.Add(underline1);
            backPanel.Controls.Add(password);
            backPanel.Controls.Add(underline2);
            backPanel.Controls.Add(submitBtn);
        }
        private void Connx(object sender, EventArgs e) {
            var URI = Settings.Default.URI;
            var API_KEY = Settings.Default.API_KEY;
            if (password.Text != "" && username.Text != "") {
                var myRequest = new Http(URI + "api/index.php", "POST", "#=login&_="+API_KEY+"+&e="+username+"&p="+password);
                MessageBox.Show("NOT NULL\n" + myRequest.GetResponse());
            } else {

            }
        }
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
