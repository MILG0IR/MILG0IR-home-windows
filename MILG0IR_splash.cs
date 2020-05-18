using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MILG0IR_home_windows_x64.functions;
using MILG0IR_home_windows_x64.Properties;

namespace MILG0IR_home_windows_x64 {
    public partial class MILG0IR_splash : Form {
        functionsClass MILG0IR = new functionsClass();
        public MILG0IR_splash() { InitializeComponent(); }
        private void MILG0IR_splash_Load(object sender, EventArgs e) {
            progressBar.Visible = false;
            var X = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (Width / 2);
            var Y = (Screen.PrimaryScreen.WorkingArea.Height / 2) - (Height / 2);
            var pos = new Point(X, Y);
            Location = pos;
            var col = Color.FromArgb(29, 29, 44);
            BackColor = col;
            FormBorderStyle = FormBorderStyle.None;

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
        private void timer1_Tick(object sender, EventArgs e) {
            progressBar.Increment(1);
            if(progressBar.Value == 15) {
                if(Settings.Default.API_KEY == "" && Settings.Default.URI == "") {
                    new MILG0IR_connect().Show();
                } else {
                    MessageBox.Show("SET \n TODO - Check the connection and return an errror message ic unable to connect");
                }
            }
            if (progressBar.Value == 30) {
                this.Hide();
            }
        }
    }
}
