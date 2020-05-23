using System;
using System.Windows.Forms;
using MILG0IR_home_windows.functions;

namespace MILG0IR_home_windows {
    public partial class MILG0IR_initialize : Form {
        readonly Functions MILG0IR = new Functions();
        public MILG0IR_initialize() { InitializeComponent(); }

        private void MILG0IR_initialize_Load(object sender, EventArgs e) {

        }
        private void MILG0IR_initialize_Shown(object sender, EventArgs e) {
            var pages = MILG0IR.ListPages();
            MessageBox.Show(pages.ToString());
        }
        private void MILG0IR_initialize_Closed(object sender, FormClosedEventArgs e) {
            MILG0IR.Close();
        }
    }
}
