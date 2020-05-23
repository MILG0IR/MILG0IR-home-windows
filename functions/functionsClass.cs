using System;
using System.Linq;
using EasyHttp.Http;
using System.Drawing;
using Newtonsoft.Json;
using System.Drawing.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MILG0IR_home_windows.Properties;
using System.Web.Script.Serialization;
using System.Collections;
using System.Runtime.Serialization;

namespace MILG0IR_home_windows.functions {
    public class Functions {
        public string API_KEY = Settings.Default.API_KEY;
        public string API_URI = Settings.Default.API_URI;
        public string Username = Settings.Default.Username;
        public string Password = Settings.Default.Password;

        public void Input_Focused(object sender, EventArgs e) {
            TextBox elem = (TextBox)sender;
            if (elem.Text == elem.Name) {
                elem.ForeColor = Color.FromArgb(255, 255, 255);
                elem.Text = "";
            }
        }
        public void Input_Unfocused(object sender, EventArgs e) {
            TextBox elem = (TextBox)sender;
            if (elem.Text == "") {
                elem.ForeColor = Color.FromArgb(117, 117, 117);
                elem.Text = elem.Name;
            }
        }
        public HttpResponse POST(string uri, [Optional] string args) {
            var http = new HttpClient();
            var request = http.Post(uri, args, "");
            HttpResponse HttpResponse = new HttpResponse();
            HttpResponse.URI = uri;
            HttpResponse.StatusCode = request.StatusCode;
            HttpResponse.StatusDescription = request.StatusDescription;
            HttpResponse.Body = request.DynamicBody;
            Console.WriteLine(
                "---------- NEW WEB REQUEST -- POST ----------" +
                "\nURI: "+HttpResponse.URI+
                "\nStatusCode: "+HttpResponse.StatusCode+
                "\nStatusDescription: "+HttpResponse.StatusDescription+
                "\nBody: "+HttpResponse.Body
            );
            return HttpResponse;
        }
        public HttpResponse GET(string uri) {
            var http = new HttpClient();
            var request = http.Get(uri);
            HttpResponse HttpResponse = new HttpResponse();
            HttpResponse.URI = uri;
            HttpResponse.StatusCode = request.StatusCode;
            HttpResponse.StatusDescription = request.StatusDescription;
            HttpResponse.Body = request.DynamicBody;
            Console.WriteLine(
                "---------- NEW WEB REQUEST -- GET ----------" +
                "\nURI: "+HttpResponse.URI+
                "\nStatusCode: "+HttpResponse.StatusCode+
                "\nStatusDescription: "+HttpResponse.StatusDescription+
                "\nBody: " + HttpResponse.Body
            );
            return HttpResponse;
        }
        public ErrorCode Error_search(string code) {
            var response = GET(API_URI + "?$=search_code&code=" + code);
            var body = response.Body;
            var json = JsonConvert.DeserializeObject(body);
            string[] arr = ((IEnumerable)json).Cast<object>().Select(x => x.ToString()).ToArray();
            ErrorCode ErrorCode = new ErrorCode();
            ErrorCode.Code = arr[0];
            ErrorCode.Reason = arr[1];
            ErrorCode.Description = arr[2];
            Console.WriteLine(
                "---------- ERROR CODE ----------" +
                "\nCode: "+ErrorCode.Code +
                "\nReason: "+ErrorCode.Reason +
                "\nDescription: "+ErrorCode.Description
            );
            return ErrorCode;
        }
        public void Close() {
            Settings.Default.Save();
            Application.Exit();
        }

        public bool IsUrl(string url) {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute)) {
                return true;
            } else {
                return false;
            }
        }
        public void ChangeForm(Form form) {
            var previous = Application.OpenForms.Cast<Form>().Last();
            previous.Hide();
            form.Show();
        }
        public bool Test_Connection() {
            var status = GET(API_URI + "?$=confirm_api_key&_=" + API_KEY);
            if (status.Body == "success") {
                return true;
            } else {
                return false;
            }
        }
        public bool Test_Login() {
            var status = GET(API_URI + "?$=login&_=" + API_KEY + "&e=" + Username + "&p=" + Password);
            if (status.Body == "success") {
                return true;
            } else {
                return false;
            }
        }
        public object ListPages() {
            var response = GET(Settings.Default.API_URI + "?$=list_pages&_=" + Settings.Default.API_KEY);
            var body = response.Body;
            dynamic obj = JsonConvert.DeserializeObject(body);
            return obj;
        }
        public bool Connnect(string API_URI, string API_KEY) {
            var status = GET(API_URI + "?$=confirm_api_key&_=" + API_KEY);
            if (status.Body == "success") {
                return true;
            } else {
                return false;
            }
        }
        public string Login(string User, string Pass) {
            var status = GET(API_URI + "?$=login&_=" + API_KEY + "&e=" + User + "&p=" + Pass);
            if (status.Body == "success") {
                return "success";
            } else {
                return status.Body;
            }
        }
        public string Logout() {
            var status = GET(API_URI + "?$=logout&_=" + API_KEY);
            if (status.Body == "success") {
                return "success";
            } else {
                return status.ToString();
            }
        }
    }
    public class Initialize {
        public Timer MG_time;
        public Label MG_title;
        public Button MG_resize;
        private bool mouseDown;
        private Point lastLocation;
        public List<JobTimer> _timers = new List<JobTimer>();
        public class JobTimer {
            public Timer Timer { get; set; }
            public string ID { get; set; }
        }
        public void Create_title_bar(String title = "MILG0IR home", Boolean minimise = true, Boolean resize = true, Boolean close = true) {
            PrivateFontCollection pfc = new PrivateFontCollection();
            int fontLength = Properties.Resources.segoe_mdl2_assets.Length;
            byte[] fontdata = Properties.Resources.segoe_mdl2_assets;
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);

            var parent = Application.OpenForms.Cast<Form>().Last();
            var MG_titlebar = new Panel();
            MG_titlebar.Dock = DockStyle.Top;
            MG_titlebar.Height = 25;
            MG_titlebar.BackColor = Color.FromArgb(29, 29, 44);
            if (resize == true) MG_titlebar.MouseDoubleClick += new MouseEventHandler(MG_action_resize);
            MG_titlebar.MouseDown += new MouseEventHandler(Titlebar_MouseDown);
            MG_titlebar.MouseMove += new MouseEventHandler(Titlebar_MouseMove);
            MG_titlebar.MouseUp += new MouseEventHandler(Titlebar_MouseUp);
            MG_title = new Label();
            MG_title.AutoSize = true;
            MG_title.Text = title;
            MG_title.Anchor = AnchorStyles.Top;
            MG_title.Font = new Font("Arial", 12, FontStyle.Regular);
            MG_title.ForeColor = Color.FromArgb(255, 255, 255);
            MG_title.Location = new Point((parent.Width / 2) - (MG_title.Width / 2), (MG_titlebar.Height - MG_title.Height) * 2);
            if (resize == true) MG_title.MouseDoubleClick += new MouseEventHandler(MG_action_resize);
            MG_title.MouseDown += new MouseEventHandler(Titlebar_MouseDown);
            MG_title.MouseMove += new MouseEventHandler(Titlebar_MouseMove);
            MG_title.MouseUp += new MouseEventHandler(Titlebar_MouseUp);
            if (title == "MG_time") MG_title.Text = DateTime.Now.ToString("ddd, dd MMM - HH:mm");
            MG_time = new Timer();
            MG_time.Enabled = true;
            if (title == "MG_time") MG_time.Tick += new System.EventHandler(Update_time);
            var job = new JobTimer();
            job.Timer = MG_time;
            var MG_minimise = new Button();
            MG_minimise.Text = "";
            MG_minimise.Width = 45;
            MG_minimise.TabStop = false;
            MG_minimise.Dock = DockStyle.Right;
            MG_minimise.BackColor = Color.Transparent;
            MG_minimise.ForeColor = Color.FromArgb(255, 255, 255);
            MG_minimise.FlatStyle = FlatStyle.Flat;
            MG_minimise.FlatAppearance.BorderSize = 0;
            MG_minimise.Font = new Font(pfc.Families[0], MG_minimise.Font.Size);
            MG_minimise.FlatAppearance.MouseOverBackColor = Color.FromArgb(43, 53, 83);
            MG_minimise.FlatAppearance.MouseDownBackColor = Color.FromArgb(63, 63, 92);
            MG_minimise.Click += new EventHandler(MG_action_minimise);
            MG_resize = new Button();
            MG_resize.Text = "";
            if (parent.WindowState == FormWindowState.Maximized) MG_resize.Text = "";
            MG_resize.Width = 45;
            MG_resize.TabStop = false;
            MG_resize.Dock = DockStyle.Right;
            MG_resize.BackColor = Color.Transparent;
            MG_resize.ForeColor = Color.FromArgb(255, 255, 255);
            MG_resize.FlatStyle = FlatStyle.Flat;
            MG_resize.FlatAppearance.BorderSize = 0;
            MG_resize.Font = new Font(pfc.Families[0], MG_resize.Font.Size);
            MG_resize.FlatAppearance.MouseOverBackColor = Color.FromArgb(43, 53, 83);
            MG_resize.FlatAppearance.MouseDownBackColor = Color.FromArgb(63, 63, 92);
            MG_resize.Click += new EventHandler(MG_action_resize);
            var MG_close = new Button();
            MG_close.Text = "";
            MG_close.Width = 45;
            MG_close.TabStop = false;
            MG_close.Dock = DockStyle.Right;
            MG_close.BackColor = Color.Transparent;
            MG_close.ForeColor = Color.FromArgb(255, 255, 255);
            MG_close.FlatStyle = FlatStyle.Flat;
            MG_close.FlatAppearance.BorderSize = 0;
            MG_close.Font = new Font(pfc.Families[0], MG_close.Font.Size);
            MG_close.FlatAppearance.MouseOverBackColor = Color.FromArgb(127, 255, 0, 0);
            MG_close.FlatAppearance.MouseDownBackColor = Color.FromArgb(191, 255, 0, 0);
            MG_close.Click += new EventHandler(MG_action_close);
            parent.Controls.Add(MG_titlebar);
            if (title == "MG_time") _timers.Add(job); MG_time.Start();
            MG_titlebar.Controls.Add(MG_title);
            if (minimise == true) MG_titlebar.Controls.Add(MG_minimise);
            if (resize == true) MG_titlebar.Controls.Add(MG_resize);
            if (close == true) MG_titlebar.Controls.Add(MG_close);
        }
        private void Update_time(object sender, EventArgs e) {
            MG_title.Text = DateTime.Now.ToString("ddd, dd MMM - HH:mm");
        }
        private void MG_action_close(object sender, EventArgs e) {
            Settings.Default.Save();
            Application.Exit();
        }
        private void MG_action_resize(object sender, EventArgs e) {
            var parent = Application.OpenForms.Cast<Form>().Last();
            if (parent.WindowState == FormWindowState.Maximized) {
                parent.WindowState = FormWindowState.Normal;
                MG_resize.Text = "";
            } else {
                parent.WindowState = FormWindowState.Maximized;
                MG_resize.Text = "";
            }
            Settings.Default.WindowState = parent.WindowState;
        }
        private void MG_action_minimise(object sender, EventArgs e) {
            var parent = Application.OpenForms.Cast<Form>().Last();
            parent.WindowState = FormWindowState.Minimized;
        }
        private void Titlebar_MouseDown(object sender, MouseEventArgs e) {
            mouseDown = true;
            lastLocation = e.Location;
        }
        private void Titlebar_MouseMove(object sender, MouseEventArgs e) {
            var parent = Application.OpenForms.Cast<Form>().Last();
            if (mouseDown) {
                parent.Location = new Point((parent.Location.X - lastLocation.X) + e.X, (parent.Location.Y - lastLocation.Y) + e.Y);
                parent.Update();
            }
        }
        private void Titlebar_MouseUp(object sender, MouseEventArgs e) {
            mouseDown = false;
        }

    }
}