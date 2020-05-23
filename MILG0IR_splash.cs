using System;
using System.Drawing;
using System.Drawing.Design;
using System.Net;
using System.Security.Cryptography;
using System.Windows.Forms;
using MILG0IR_home_windows.functions;
using MILG0IR_home_windows.Properties;

namespace MILG0IR_home_windows_x64 {
    public partial class MILG0IR_splash : Form {
        readonly Functions MILG0IR = new Functions();
        public MILG0IR_splash() { InitializeComponent(); }
        private void MILG0IR_splash_Load(object sender, EventArgs e) {
            progressBar.Visible = false;
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.FromArgb(39, 41, 61);
            Size = new Size(450, 450);
            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            var pos = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (Width / 2), (Screen.PrimaryScreen.WorkingArea.Height / 2) - (Height / 2));
            Location = pos;

            Logo.BackgroundImageLayout = ImageLayout.Zoom;
            var scale = Convert.ToInt32(Height * 0.75);
            var img = Resources.milg0ir_logo_basic_512x512_transparent;
            Logo.Size = new Size(scale, scale);
            Logo.BackgroundImage = img;
            var imgX = (Width / 2) - (Logo.Width / 2);
            var imgY = (Height / 2) - (Logo.Height / 2);
            var imgpos = new Point(imgX, imgY);
            Logo.Location = imgpos;

            timer.Start();

        }
        private void Timer1_Tick(object sender, EventArgs e) {
            progressBar.Increment(1);
            if (progressBar.Value == 15) {
                if(Settings.Default.API_URI != "") {
                    if (MILG0IR.Test_Connection()) {
                        if (MILG0IR.Test_Login()) {
                            MILG0IR.ChangeForm(new MILG0IR_home());
                        } else {
                            MILG0IR.ChangeForm(new MILG0IR_login());
                        }
                    } else {
                        MILG0IR.ChangeForm(new MILG0IR_connect());
                    }
                } else {
                    MILG0IR.ChangeForm(new MILG0IR_connect());
                }
            }
        }

        private void MILG0IR_splash_FormClosed(object sender, FormClosedEventArgs e) {
            MILG0IR.Close();
        }
    }
}
