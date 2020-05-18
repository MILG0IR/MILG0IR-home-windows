using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace MILG0IR_home_windows_x64.functions {
    public class functionsClass {

        public void application_minimise() {
        }
        public void application_resize() {
        }
        public void application_close() {
            Application.Exit();
        }
        public void application_sleep(int time) {
            var parent = Application.OpenForms.Cast<Form>().Last();
        }
        public void create_title_bar(String title, Boolean minimise = true, Boolean resize = true, Boolean close = true) {
            var parent = Application.OpenForms.Cast<Form>().Last();
            var MG_titlebar = new Panel();
                MG_titlebar.Dock = DockStyle.Top;
                MG_titlebar.Height = 20;
                MG_titlebar.BackColor = Color.Transparent;
            var MG_title = new Label();
                MG_title.Text = title;
                MG_title.Location = new Point((parent.Width / 2) - (MG_title.Width / 2));
            var MG_minimise = new Button();
                MG_minimise.Dock = DockStyle.Right;
                MG_minimise.BackColor = Color.Transparent;
                MG_minimise.FlatStyle = FlatStyle.Flat;
                MG_minimise.FlatAppearance.BorderSize = 0;
                MG_minimise.Click += new EventHandler(MG_minimise_Click);
            var MG_resize = new Button();
                MG_resize.Dock = DockStyle.Right;
                MG_resize.BackColor = Color.Transparent;
                MG_resize.FlatStyle = FlatStyle.Flat;
                MG_resize.FlatAppearance.BorderSize = 0;
                MG_resize.Click += new EventHandler(MG_resize_Click);
            var MG_close = new Button();
                MG_close.Dock = DockStyle.Right;
                MG_close.BackColor = Color.Transparent;
                MG_close.FlatStyle = FlatStyle.Flat;
                MG_close.FlatAppearance.BorderSize = 0;
                MG_close.Click += new EventHandler(MG_close_Click);
            parent.Controls.Add(MG_titlebar);
            MG_titlebar.Controls.Add(MG_title);
            MG_titlebar.Controls.Add(MG_minimise);
            MG_titlebar.Controls.Add(MG_resize);
            MG_titlebar.Controls.Add(MG_close);

        }
        private void MG_close_Click(object sender, EventArgs e) {
            application_close();
        }
        private void MG_resize_Click(object sender, EventArgs e) {
            var parent = Application.OpenForms.Cast<Form>().Last();
            if (parent.WindowState == FormWindowState.Maximized) {
                parent.WindowState = FormWindowState.Normal;
            } else {
                parent.WindowState = FormWindowState.Maximized;
            }
        }
        private void MG_minimise_Click(object sender, EventArgs e) {
            var parent = Application.OpenForms.Cast<Form>().Last();
            parent.WindowState = FormWindowState.Minimized;
        }
    }
}