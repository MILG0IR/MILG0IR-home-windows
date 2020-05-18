using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MILG0IR_home_windows_x64.functions;

namespace MILG0IR_home_windows_x64 {
    public partial class MILG0IR_connect : Form {
        functionsClass MILG0IR = new functionsClass();
        public MILG0IR_connect() { InitializeComponent(); }

        private void MILG0IR_connect_Load(object sender, EventArgs e) {
            FormBorderStyle = FormBorderStyle.None;
            var col = Color.FromArgb(29, 29, 44);
            BackColor = col;
            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            MILG0IR.create_title_bar("Welcome", true, false, true);
        }
    }
}
