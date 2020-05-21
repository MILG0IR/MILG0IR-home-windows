using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using MILG0IR_home_windows_x64.functions;
using MILG0IR_home_windows_x64.Properties;

namespace MILG0IR_home_windows_x64 {
    public partial class MILG0IR_login : Form {
        readonly Functions MILG0IR = new Functions();
        readonly Initialize MILG0IR_init = new Initialize();
        public Button SubmitBtn;
        public TextBox Username;
        public Panel Username_u;
        public TextBox Password;
        public Panel Password_u;
        public Panel Backpanel;
        public Label Title;
        public Label Subtitle;
        public MILG0IR_login() { InitializeComponent(); }
        private void MILG0IR_home_Load(object sender, EventArgs e) {
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.FromArgb(39, 41, 61);
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
                Backpanel.Dock = DockStyle.Bottom;
            Title = new System.Windows.Forms.Label();
                Title.AutoSize = true;
                Title.MaximumSize = new Size(Width - 50, 0);
                Title.Text = "Hello.\nWelcome home.";
                Title.ForeColor = Color.FromArgb(255, 255, 255);
                Title.Font = new Font("ubuntu", 30, FontStyle.Regular);
                Title.Location = new Point(25, Convert.ToInt32(Backpanel.Height * 0.2));
            Subtitle= new System.Windows.Forms.Label();
                Subtitle.AutoSize = true;
                Subtitle.MaximumSize = new Size(Width - 50, 0);
                Subtitle.Font = new Font("ubuntu", 10, FontStyle.Regular);
                Subtitle.ForeColor = Color.FromArgb(255, 255, 255);
                Subtitle.Location = new Point(25, Convert.ToInt32(Backpanel.Height * 0.35));
                Subtitle.Text = "Log in below to accesss your new home.";
            Username = new TextBox();
                Username.AutoSize = true;
                Username.Font = new Font("Arial", 20, FontStyle.Regular);
                Username.BackColor = Color.FromArgb(39, 41, 61);
                Username.ForeColor = Color.FromArgb(255, 255, 255);
                Username.BorderStyle = BorderStyle.None;
                Username.Width = Backpanel.Width - 50;
                Username.Location = new Point(25, Convert.ToInt32(Backpanel.Height * 0.45));
                Username.Text = "Username";
                Username.Enter += new EventHandler(MILG0IR.Input_Focused);
                Username.Leave += new EventHandler(MILG0IR.Input_Unfocused);
            Username_u = new Panel();
                Username_u.Location = new Point(25, (Username.Location.Y + Username.Height) + 1);
                Username_u.BackColor = Color.FromArgb(43, 53, 83);
                Username_u.Height = 2;
                Username_u.Width = Backpanel.Width - 50;
            Password = new TextBox();
                Password.AutoSize = true;
                Password.Font = new Font("Arial", 20, FontStyle.Regular);
                Password.BackColor = Color.FromArgb(39, 41, 61);
                Password.ForeColor = Color.FromArgb(255, 255, 255);
                Password.BorderStyle = BorderStyle.None;
                Password.Width = Backpanel.Width - 50;
                Password.Location = new Point(25, Convert.ToInt32(Backpanel.Height * 0.55));
                Password.Text = "Passsword";
                Password.Name = "Passsword";
                Password.Enter += new EventHandler(MILG0IR.Input_Focused);
                Password.Leave += new EventHandler(MILG0IR.Input_Unfocused);
            Password_u = new Panel();
                Password_u.Location = new Point(25, (Password.Location.Y + Password.Height) + 1);
                Password_u.BackColor = Color.FromArgb(43, 53, 83);
                Password_u.Height = 2;
                Password_u.Width = Backpanel.Width - 50;
            SubmitBtn = new Button();
                SubmitBtn.AutoSize = true;
                SubmitBtn.Width = Backpanel.Width - (Width / 2);
                SubmitBtn.Font = new Font("Arial", 20, FontStyle.Regular);
                SubmitBtn.Location = new Point((Width / 4), Convert.ToInt32(Backpanel.Height * 0.65));
                SubmitBtn.ForeColor = Color.FromArgb(255, 255, 255);
                SubmitBtn.FlatStyle = FlatStyle.Flat;
                SubmitBtn.Text = "Login";
                SubmitBtn.TabStop = false;
                SubmitBtn.Click += new EventHandler(Connx);
            Controls.Add(Backpanel);
                Backpanel.Controls.Add(Title);
                Backpanel.Controls.Add(Subtitle);
                Backpanel.Controls.Add(Username);
                Backpanel.Controls.Add(Username_u);
                Backpanel.Controls.Add(Password);
                Backpanel.Controls.Add(Password_u);
                Backpanel.Controls.Add(SubmitBtn);
        }
        private void Connx(object sender, EventArgs e) {
            string uri = Settings.Default.URI;
            string user = Username.Text;
            string pass = Password.Text;
            if (user!= "") {
                try {
                    var myRequest = new Http(uri + "api/index.php", "POST", "$=login&_=key&e="+user+ "&p="+pass);
                    if (myRequest.GetResponse() == "success") {
                        MessageBox.Show("Login success.");
                        Close();
                        new MILG0IR_home().Show();
                    } else {
                        try {
                            var search_code = new Http(uri + "api/index.php", "GET", "$=search_code&_=key&code=" + myRequest.GetResponse());
                            MessageBox.Show(search_code.ToString());
                        } catch (WebException ex) {
                            MessageBox.Show(ex.Message + "", ex.Status + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                } catch (WebException ex) {
                    MessageBox.Show(ex.Message + "", ex.Status + "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } else {

            }
        }

        private void MILG0IR_login_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }
    }
}
