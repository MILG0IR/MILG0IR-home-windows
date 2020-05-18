﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MILG0IR_home_windows_x64.functions;

namespace MILG0IR_home_windows_x64 {
    public partial class MILG0IR_connect : Form {
        readonly Functions MILG0IR = new Functions();
        public Button submitBtn;
        public TextBox uriInput;
        public TextBox apiInput;
        public MILG0IR_connect() { InitializeComponent(); }
        public void MILG0IR_connect_Load(object sender, EventArgs e) {
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
                Text = "Please enter the url and api key for your MILG0IR server."
            };
            uriInput = new TextBox {
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
                Location = new Point(25, (uriInput.Location.Y + uriInput.Height) + 1),
                BackColor = Color.FromArgb(43, 53, 83),
                Height = 2,
                Width = backPanel.Width - 50
            };
            apiInput = new TextBox {
                AutoSize = true,
                Font = new Font("Arial", 20, FontStyle.Regular),
                BackColor = Color.FromArgb(29, 29, 44),
                ForeColor = Color.FromArgb(255, 255, 255),
                BorderStyle = BorderStyle.None,
                Width = backPanel.Width - 50,
                Location = new Point(25, Convert.ToInt32(backPanel.Height * 0.55)),
                Text = "API key",
                Name = "apiInput"
            };
            var underline2 = new Panel() {
                Location = new Point(25, (apiInput.Location.Y + apiInput.Height) + 1),
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
            backPanel.Controls.Add(uriInput);
            backPanel.Controls.Add(underline1);
            backPanel.Controls.Add(apiInput);
            backPanel.Controls.Add(underline2);
            backPanel.Controls.Add(submitBtn);
        }
        private void Connx(object sender, EventArgs e) {
            bool isUri = Uri.IsWellFormedUriString(uriInput.Text, UriKind.RelativeOrAbsolute);
            if(isUri) {
                string uri = uriInput.Text;
                if (!uri.ToLower().StartsWith("http")) uri = "http://" + uri;
                if (!uri.ToLower().EndsWith("/")) uri += "/";
                MessageBox.Show(uri);
                if (apiInput.Text != "" && uriInput.Text != "") {
                    try {
                        var myRequest = new Http(uri + "api/index.php", "POST", "#=get_messages&_=key+&u1=1000&u2=1");
                        MessageBox.Show("NOT NULL\n" + myRequest.GetResponse());
                    } catch (WebException ex) {
                        MessageBox.Show(ex + "");
                    }
                } else {

                }
            }
        }
    }
}