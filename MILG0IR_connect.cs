using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using MILG0IR_home_windows_x64.functions;
using MILG0IR_home_windows_x64.Properties;

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
            MILG0IR.create_title_bar("MILG0IR home", true, false, true);
            init();
        }
        private void init() {
            var backPanel = new Panel();
                backPanel.Size = new Size(Width, Height - 25);
                backPanel.Dock = DockStyle.Bottom;
            var welcome = new System.Windows.Forms.Label();
                welcome.AutoSize = true;
                welcome.MaximumSize = new Size(Width - 50, 0);
                welcome.Text = "Hello.\nWelcome home.";
                welcome.ForeColor = Color.FromArgb(255, 255, 255);
                welcome.Font = new Font("ubuntu", 30, FontStyle.Regular);
                welcome.Location = new Point(25, Convert.ToInt32(backPanel.Height * 0.2));
            var info = new System.Windows.Forms.Label();
                info.AutoSize = true;
                info.MaximumSize = new Size(Width - 50, 0);
                info.Font = new Font("ubuntu", 10, FontStyle.Regular);
                info.ForeColor = Color.FromArgb(255, 255, 255);
                info.Location = new Point(25, Convert.ToInt32(backPanel.Height * 0.35));
                info.Text = "Please enter the url and api key for your MILG0IR server.";
            uriInput = new TextBox();
                uriInput.AutoSize = true;
                uriInput.Font = new Font("Arial", 20, FontStyle.Regular);
                uriInput.BackColor = Color.FromArgb(29, 29, 44);
                uriInput.ForeColor = Color.FromArgb(255, 255, 255);
                uriInput.BorderStyle = BorderStyle.None;
                uriInput.Width = backPanel.Width - 50;
                uriInput.Location = new Point(25, Convert.ToInt32(backPanel.Height * 0.45));
                uriInput.Text = "Server URL";
            var underline1 = new Panel();
                underline1.Location = new Point(25, (uriInput.Location.Y + uriInput.Height) + 1);
                underline1.BackColor = Color.FromArgb(43, 53, 83);
                underline1.Height = 2;
                underline1.Width = backPanel.Width - 50;
            apiInput = new TextBox();
                apiInput.AutoSize = true;
                apiInput.Font = new Font("Arial", 20, FontStyle.Regular);
                apiInput.BackColor = Color.FromArgb(29, 29, 44);
                apiInput.ForeColor = Color.FromArgb(255, 255, 255);
                apiInput.BorderStyle = BorderStyle.None;
                apiInput.Width = backPanel.Width - 50;
                apiInput.Location = new Point(25, Convert.ToInt32(backPanel.Height * 0.55));
                apiInput.Text = "API key";
            var underline2 = new Panel();
                underline2.Location = new Point(25, (apiInput.Location.Y + apiInput.Height) + 1);
                underline2.BackColor = Color.FromArgb(43, 53, 83);
                underline2.Height = 2;
                underline2.Width = backPanel.Width - 50;
            submitBtn = new Button();
                submitBtn.AutoSize = true;
                submitBtn.Width = backPanel.Width - (Width / 2);
                submitBtn.Font = new Font("Arial", 20, FontStyle.Regular);
                submitBtn.Location = new Point((Width / 4), Convert.ToInt32(backPanel.Height * 0.65));
                submitBtn.ForeColor = Color.FromArgb(255, 255, 255);
                submitBtn.FlatStyle = FlatStyle.Flat;
                submitBtn.Text = "Connect";
                submitBtn.TabStop = false;
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
                string api = apiInput.Text;
                string uri = uriInput.Text;
                if (!uri.ToLower().StartsWith("http")) uri = "http://" + uri;
                if (!uri.ToLower().EndsWith("/")) uri += "/";
                if (api != "" && uri != "") {
                    try {
                        var myRequest = new Http(uri + "api/index.php", "POST", "$=confirm_api_key&_=key");
                        if (myRequest.GetResponse() == "success") { 
                            MessageBox.Show("Your details have been accepted.");
                            Settings.Default.URI = uri;
                            Settings.Default.API_KEY = api;
                            Close();
                            new MILG0IR_login().Show();
                        }
                    } catch (WebException ex) {
                        MessageBox.Show(ex.Message+"", ex.Status+"", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                } else {

                }
            }
        }
    }
}