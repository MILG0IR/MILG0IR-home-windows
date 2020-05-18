﻿using System;
using System.Drawing;
using System.Windows.Forms;
using MILG0IR_home_windows_x64.Properties;

namespace MILG0IR_home_windows_x64 {
    public partial class MILG0IR_splash : Form {
        public MILG0IR_splash() { InitializeComponent(); }
        private void MILG0IR_splash_Load(object sender, EventArgs e) {
            progressBar.Visible = false;
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.FromArgb(29, 29, 44);
            Size = new Size(650, 450);
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
                if (Settings.Default.API_KEY == "" && Settings.Default.URI == "") {
                    new MILG0IR_connect().Show();
                } else {
                    MessageBox.Show("SET \n TODO - Check the connection and return an errror message ic unable to connect");
                }
                this.Hide();
            }
        }
    }
}
