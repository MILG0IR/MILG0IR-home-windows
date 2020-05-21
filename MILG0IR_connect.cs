using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using MILG0IR_home_windows_x64.functions;
using MILG0IR_home_windows_x64.Properties;

namespace MILG0IR_home_windows_x64 {
    public partial class MILG0IR_connect : Form {
        readonly Functions MILG0IR = new Functions();
        readonly Initialize MILG0IR_init = new Initialize();
        public Button SubmitBtn;
        public TextBox UriInput;
        public Panel UriInput_u;
        public TextBox ApiInput;
        public Panel ApiInput_u;
        public Panel Backpanel;
        public Label Title;
        public Label Subtitle;
        public MILG0IR_connect() { InitializeComponent(); }
        public void MILG0IR_connect_Load(object sender, EventArgs e) {
            FormBorderStyle = FormBorderStyle.None;
            Size = new Size(450, 750);
            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            var pos = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (Width / 2), (Screen.PrimaryScreen.WorkingArea.Height / 2) - (Height / 2));
            Location = pos;
            MILG0IR_init.create_title_bar("MILG0IR home", true, false, true);
            InitForm();
            InitGraphics();
        }
        private void InitGraphics() {
        }
        private void InitForm() {
            Backpanel = new Panel();
                Backpanel.Size = new Size(Width, Height - 25);
                Backpanel.BackColor = Color.FromArgb(39, 41, 61);
                Backpanel.Dock = DockStyle.Bottom;
            Title = new Label();
                Title.AutoSize = true;
                Title.MaximumSize = new Size(Width - 50, 0);
                Title.Text = "Hello.\nWelcome home.";
                Title.ForeColor = Color.FromArgb(255, 255, 255);
                Title.Font = new Font("ubuntu", 30, FontStyle.Regular);
                Title.Location = new Point(25, Convert.ToInt32(Backpanel.Height * 0.2));
            Subtitle = new Label();
                Subtitle.AutoSize = true;
                Subtitle.MaximumSize = new Size(Width - 50, 0);
                Subtitle.Font = new Font("ubuntu", 10, FontStyle.Regular);
                Subtitle.ForeColor = Color.FromArgb(255, 255, 255);
                Subtitle.Location = new Point(25, Convert.ToInt32(Backpanel.Height * 0.35));
                Subtitle.Text = "Please enter the url and api key for your MILG0IR server.";
            UriInput = new TextBox();
                UriInput.AutoSize = true;
                UriInput.Font = new Font("Arial", 20, FontStyle.Regular);
                UriInput.BackColor = Color.FromArgb(39, 41, 61);
                UriInput.ForeColor = Color.FromArgb(117, 117, 117);
                UriInput.BorderStyle = BorderStyle.None;
                UriInput.Width = Backpanel.Width - 50;
                UriInput.Location = new Point(25, Convert.ToInt32(Backpanel.Height * 0.45));
                UriInput.Text = "Server URL";
                UriInput.Name = "Server URL";
                UriInput.Enter += new EventHandler(MILG0IR.Input_Focused);
                UriInput.Leave += new EventHandler(MILG0IR.Input_Unfocused);
            UriInput_u = new Panel();
                UriInput_u.Location = new Point(25, (UriInput.Location.Y + UriInput.Height) + 1);
                UriInput_u.BackColor = Color.FromArgb(43, 53, 83);
                UriInput_u.Height = 2;
                UriInput_u.Width = Backpanel.Width - 50;
            ApiInput = new TextBox();
                ApiInput.AutoSize = true;
                ApiInput.Font = new Font("Arial", 20, FontStyle.Regular);
                ApiInput.BackColor = Color.FromArgb(39, 41, 61);
                ApiInput.ForeColor = Color.FromArgb(117, 117, 117);
                ApiInput.BorderStyle = BorderStyle.None;
                ApiInput.Width = Backpanel.Width - 50;
                ApiInput.Location = new Point(25, Convert.ToInt32(Backpanel.Height * 0.55));
                ApiInput.Text = "API key";
                ApiInput.Name = "API key";
                ApiInput.Enter += new EventHandler(MILG0IR.Input_Focused);
                ApiInput.Leave += new EventHandler(MILG0IR.Input_Unfocused);
            ApiInput_u = new Panel();
                ApiInput_u.Location = new Point(25, (ApiInput.Location.Y + ApiInput.Height) + 1);
                ApiInput_u.BackColor = Color.FromArgb(43, 53, 83);
                ApiInput_u.Height = 2;
                ApiInput_u.Width = Backpanel.Width - 50;
            SubmitBtn = new Button();
                SubmitBtn.AutoSize = true;
                SubmitBtn.Width = Backpanel.Width - (Width / 2);
                SubmitBtn.Font = new Font("Arial", 20, FontStyle.Regular);
                SubmitBtn.Location = new Point((Width / 4), Convert.ToInt32(Backpanel.Height * 0.65));
                SubmitBtn.ForeColor = Color.FromArgb(255, 255, 255);
                SubmitBtn.FlatStyle = FlatStyle.Flat;
                SubmitBtn.Text = "Connect";
                SubmitBtn.TabStop = false;
                SubmitBtn.Click += new EventHandler(Connx);
            Controls.Add(Backpanel);
                Backpanel.Controls.Add(Title);
                Backpanel.Controls.Add(Subtitle);
                Backpanel.Controls.Add(UriInput);
                Backpanel.Controls.Add(UriInput_u);
                Backpanel.Controls.Add(ApiInput);
                Backpanel.Controls.Add(ApiInput_u);
                Backpanel.Controls.Add(SubmitBtn);
        }
        private void Connx(object sender, EventArgs e) {
            string api_name = ApiInput.Name;
            string uri_name = UriInput.Name;
            string api = (ApiInput.Text == api_name)?null:ApiInput.Text;
            string uri = (UriInput.Text == uri_name)?null:UriInput.Text;
            
            if (uri == uri_name) { uri = null; }
            bool isUri = Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute);
            if (uri == null && api != null) { MessageBox.Show("Please enter a valid server URI", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Warning); } else
            if (uri != null && api == null) { MessageBox.Show("Please enter a valid server API key", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Warning); } else 
            if (uri == null && api == null) { MessageBox.Show("Please enter a valid server URI and API key", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            if (isUri) {
                if (!uri.ToLower().StartsWith("http")) uri = "http://" + uri;
                if (!uri.ToLower().EndsWith("/")) uri += "/";
                if (api != null && uri != null) {
                    try {
                        var myRequest = new Http(uri + "api/index.php", "POST", "$=confirm_api_key&_="+api);
                        if (myRequest.GetResponse() == "success") { 
                            Settings.Default.URI = uri;
                            Settings.Default.API_KEY = api;
                            Close();
                            new MILG0IR_login().Show();
                        }
                    } catch (WebException ex) {
                        MessageBox.Show(ex.Message+"", ex.Status+"", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void MILG0IR_connect_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}