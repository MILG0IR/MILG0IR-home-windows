namespace MILG0IR_home_windows_x64 {
    partial class MILG0IR_home {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.BackPanel = new System.Windows.Forms.Panel();
            this.Border = new System.Windows.Forms.Panel();
            this.Container = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.MG_Menu = new System.Windows.Forms.Panel();
            this.Header = new System.Windows.Forms.Panel();
            this.MenuBTN = new System.Windows.Forms.Button();
            this.BackPanel.SuspendLayout();
            this.Border.SuspendLayout();
            this.Container.SuspendLayout();
            this.Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackPanel
            // 
            this.BackPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(44)))));
            this.BackPanel.Controls.Add(this.Border);
            this.BackPanel.Controls.Add(this.Header);
            this.BackPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackPanel.Location = new System.Drawing.Point(0, 0);
            this.BackPanel.Name = "BackPanel";
            this.BackPanel.Size = new System.Drawing.Size(800, 450);
            this.BackPanel.TabIndex = 0;
            // 
            // Border
            // 
            this.Border.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(78)))), ((int)(((byte)(202)))));
            this.Border.Controls.Add(this.Container);
            this.Border.Controls.Add(this.MG_Menu);
            this.Border.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Border.Location = new System.Drawing.Point(0, 45);
            this.Border.Name = "Border";
            this.Border.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.Border.Size = new System.Drawing.Size(800, 405);
            this.Border.TabIndex = 2;
            // 
            // Container
            // 
            this.Container.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(41)))), ((int)(((byte)(61)))));
            this.Container.Controls.Add(this.button1);
            this.Container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Container.Location = new System.Drawing.Point(50, 2);
            this.Container.Name = "Container";
            this.Container.Size = new System.Drawing.Size(750, 403);
            this.Container.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // MG_Menu
            // 
            this.MG_Menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(78)))), ((int)(((byte)(202)))));
            this.MG_Menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.MG_Menu.Location = new System.Drawing.Point(0, 2);
            this.MG_Menu.Name = "MG_Menu";
            this.MG_Menu.Size = new System.Drawing.Size(50, 403);
            this.MG_Menu.TabIndex = 7;
            this.MG_Menu.Paint += new System.Windows.Forms.PaintEventHandler(this.MG_Menu_Paint);
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(44)))));
            this.Header.Controls.Add(this.MenuBTN);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(800, 45);
            this.Header.TabIndex = 1;
            // 
            // MenuBTN
            // 
            this.MenuBTN.FlatAppearance.BorderSize = 0;
            this.MenuBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MenuBTN.Font = new System.Drawing.Font("Segoe MDL2 Assets", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuBTN.Location = new System.Drawing.Point(0, 0);
            this.MenuBTN.Name = "MenuBTN";
            this.MenuBTN.Size = new System.Drawing.Size(50, 45);
            this.MenuBTN.TabIndex = 0;
            this.MenuBTN.TabStop = false;
            this.MenuBTN.Text = "";
            this.MenuBTN.UseVisualStyleBackColor = true;
            this.MenuBTN.Click += new System.EventHandler(this.Toggle_Menu);
            // 
            // MILG0IR_home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BackPanel);
            this.Name = "MILG0IR_home";
            this.Text = "MILG0IR home";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MILG0IR_home_FormClosed);
            this.Load += new System.EventHandler(this.MILG0IR_home_Load);
            this.ResizeEnd += new System.EventHandler(this.MILG0IR_home_ResizeEnd);
            this.Resize += new System.EventHandler(this.MILG0IR_home_Resize);
            this.BackPanel.ResumeLayout(false);
            this.Border.ResumeLayout(false);
            this.Container.ResumeLayout(false);
            this.Header.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BackPanel;
        private System.Windows.Forms.Panel Border;
        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Button MenuBTN;
        private new System.Windows.Forms.Panel Container;
        private System.Windows.Forms.Panel MG_Menu;
        private System.Windows.Forms.Button button1;
    }
}