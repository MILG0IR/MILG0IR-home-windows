using System;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Text;

namespace MILG0IR_home_windows_x64.functions {
    public class Functions{
        private bool mouseDown;
        private Point lastLocation;
        public void init(int time) { }
        public void application_sleep(int time) {
            PrivateFontCollection pfc = new PrivateFontCollection();
            int fontLength = Properties.Resources.segoe_mdl2_assets.Length;
            byte[] fontdata = Properties.Resources.segoe_mdl2_assets;
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);
        }
        public void create_title_bar(String title = "MILG0IR home", Boolean minimise = true, Boolean resize = true, Boolean close = true) {
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
                MG_titlebar.BackColor = Color.FromArgb(39, 41, 61);
            if (resize == true) MG_titlebar.MouseDoubleClick += new MouseEventHandler(MG_action_resize);
                MG_titlebar.MouseDown += new MouseEventHandler(titlebar_MouseDown);
                MG_titlebar.MouseMove+= new MouseEventHandler(titlebar_MouseMove);
                MG_titlebar.MouseUp+= new MouseEventHandler(titlebar_MouseUp);
            var MG_title = new Label();
                MG_title.Text = title;
                MG_title.Anchor = AnchorStyles.Top;
                MG_title.Font = new Font("Arial", 12, FontStyle.Regular);
                MG_title.ForeColor = Color.FromArgb(255, 255, 255);
                MG_title.Location = new Point((parent.Width / 2) - (MG_title.Width / 2), (MG_titlebar.Height - MG_title.Height) * 2);
                if (resize == true) MG_title.MouseDoubleClick += new MouseEventHandler(MG_action_resize);
                MG_title.MouseDown += new MouseEventHandler(titlebar_MouseDown);
                MG_title.MouseMove += new MouseEventHandler(titlebar_MouseMove);
                MG_title.MouseUp += new MouseEventHandler(titlebar_MouseUp);
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
            var MG_resize = new Button ();
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
            MG_titlebar.Controls.Add(MG_title);
            if (minimise == true) MG_titlebar.Controls.Add(MG_minimise);
            if (resize == true) MG_titlebar.Controls.Add(MG_resize);
            if (close == true) MG_titlebar.Controls.Add(MG_close);

        }
        private void MG_action_close(object sender, EventArgs e) {
            Application.Exit();
        }
        private void MG_action_resize(object sender, EventArgs e) {
            var parent = Application.OpenForms.Cast<Form>().Last();
            if (parent.WindowState == FormWindowState.Maximized) {
                parent.WindowState = FormWindowState.Normal;
                Button btn = sender as Button;
                btn.Text = "";
            } else {
                parent.WindowState = FormWindowState.Maximized;
                Button btn = sender as Button;
                btn.Text = "";
            }
        }
        private void MG_action_minimise(object sender, EventArgs e) {
            var parent = Application.OpenForms.Cast<Form>().Last();
            parent.WindowState = FormWindowState.Minimized;
        }
        private void titlebar_MouseDown(object sender, MouseEventArgs e) {
            mouseDown = true;
            lastLocation = e.Location;
        }
        private void titlebar_MouseMove(object sender, MouseEventArgs e) {
            var parent = Application.OpenForms.Cast<Form>().Last();
            if (mouseDown) {
                parent.Location = new Point((parent.Location.X - lastLocation.X) + e.X, (parent.Location.Y - lastLocation.Y) + e.Y);
                parent.Update();
            }
        }
        private void titlebar_MouseUp(object sender, MouseEventArgs e) {
            mouseDown = false;
        }
    }
    public class Http {
        private readonly WebRequest request;
        private Stream dataStream;
        private string status;
        public String Status {
            get { return status; }
            set { status = value; }
        }
        public Http(string url) {
            request = WebRequest.Create(url);
        }
        public Http(string url, string method) : this(url) {
            if (method.Equals("GET") || method.Equals("POST")) {
                request.Method = method;
            } else {
                throw new Exception("Invalid Method Type");
            }
        }
        public Http(string url, string method, string data) : this(url, method) {
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
        }
        public string GetResponse() {
            WebResponse response = request.GetResponse();
            this.Status = ((HttpWebResponse)response).StatusDescription;
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
    }
}