using System.Net;
using System.IO;
using System.Reflection;

namespace _3DSROMEDITOR {
    partial class Form1 {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.title = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.openToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.makeRSFFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildRomfs = new System.Windows.Forms.ToolStripDropDownButton();
            this.rebuildRomFS = new System.Windows.Forms.ToolStripMenuItem();
            this.openRom = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.infoBox = new System.Windows.Forms.TextBox();
            this.statusLbl = new System.Windows.Forms.Label();
            this.openRomfs = new System.Windows.Forms.OpenFileDialog();
            this.helpBut = new System.Windows.Forms.ToolStripDropDownButton();
            this.aboutBut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 25);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(225, 350);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_Afterselect);
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Location = new System.Drawing.Point(12, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(0, 13);
            this.title.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2,
            this.buildRomfs,
            this.helpBut});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(704, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStrip,
            this.exitToolStrip});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // openToolStrip
            // 
            this.openToolStrip.Name = "openToolStrip";
            this.openToolStrip.Size = new System.Drawing.Size(103, 22);
            this.openToolStrip.Text = "Open";
            this.openToolStrip.Click += new System.EventHandler(this.openToolStrip_Click);
            // 
            // exitToolStrip
            // 
            this.exitToolStrip.Name = "exitToolStrip";
            this.exitToolStrip.Size = new System.Drawing.Size(103, 22);
            this.exitToolStrip.Text = "Exit";
            this.exitToolStrip.Click += new System.EventHandler(this.exitToolStrip_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.makeRSFFileToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(49, 22);
            this.toolStripDropDownButton2.Text = "Tools";
            // 
            // makeRSFFileToolStripMenuItem
            // 
            this.makeRSFFileToolStripMenuItem.Name = "makeRSFFileToolStripMenuItem";
            this.makeRSFFileToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.makeRSFFileToolStripMenuItem.Text = "Create RSF file";
            this.makeRSFFileToolStripMenuItem.Click += new System.EventHandler(this.makeRSFFileToolStripMenuItem_Click);
            // 
            // buildRomfs
            // 
            this.buildRomfs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buildRomfs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rebuildRomFS});
            this.buildRomfs.Image = ((System.Drawing.Image)(resources.GetObject("buildRomfs.Image")));
            this.buildRomfs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buildRomfs.Name = "buildRomfs";
            this.buildRomfs.Size = new System.Drawing.Size(47, 22);
            this.buildRomfs.Text = "Build";
            // 
            // rebuildRomFS
            // 
            this.rebuildRomFS.Name = "rebuildRomFS";
            this.rebuildRomFS.Size = new System.Drawing.Size(159, 22);
            this.rebuildRomFS.Text = "Re-build RomFS";
            this.rebuildRomFS.Click += new System.EventHandler(this.rebuildRomFS_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.infoBox);
            this.groupBox1.Location = new System.Drawing.Point(243, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 350);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rom Information";
            // 
            // infoBox
            // 
            this.infoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.infoBox.Location = new System.Drawing.Point(6, 19);
            this.infoBox.Multiline = true;
            this.infoBox.Name = "infoBox";
            this.infoBox.ReadOnly = true;
            this.infoBox.Size = new System.Drawing.Size(430, 325);
            this.infoBox.TabIndex = 0;
            // 
            // statusLbl
            // 
            this.statusLbl.AutoSize = true;
            this.statusLbl.Location = new System.Drawing.Point(12, 378);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(0, 13);
            this.statusLbl.TabIndex = 4;
            // 
            // helpBut
            // 
            this.helpBut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.helpBut.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutBut});
            this.helpBut.Image = ((System.Drawing.Image)(resources.GetObject("helpBut.Image")));
            this.helpBut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpBut.Name = "helpBut";
            this.helpBut.Size = new System.Drawing.Size(45, 22);
            this.helpBut.Text = "Help";
            this.helpBut.ToolTipText = "Help";
            // 
            // aboutBut
            // 
            this.aboutBut.Name = "aboutBut";
            this.aboutBut.Size = new System.Drawing.Size(152, 22);
            this.aboutBut.Text = "About";
            this.aboutBut.Click += new System.EventHandler(this.aboutBut_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 400);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.title);
            this.Controls.Add(this.treeView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "3DS Rom Editor";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private int checkUpdate() {
            int r = 0;
            try {
                WebRequest wr = WebRequest.Create(new System.Uri("http://reisyukaku.org/prog/3DSRE-ver.txt"));
                WebResponse ws = wr.GetResponse();
                StreamReader sr = new StreamReader(ws.GetResponseStream());
                string currVer = GlobalVars.Version;
                string newVer = sr.ReadLine();
                r = (currVer.Contains(newVer)) ? 0 : -1;
            }
            catch (WebException e) {
                System.Console.WriteLine(e.ToString());
            }
            return r;
        }
        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem openToolStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStrip;
        private System.Windows.Forms.OpenFileDialog openRom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.Label statusLbl;
        private System.Windows.Forms.TextBox infoBox;
        private System.Windows.Forms.ToolStripDropDownButton buildRomfs;
        private System.Windows.Forms.ToolStripMenuItem rebuildRomFS;
        private System.Windows.Forms.OpenFileDialog openRomfs;
        private System.Windows.Forms.ToolStripMenuItem makeRSFFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton helpBut;
        private System.Windows.Forms.ToolStripMenuItem aboutBut;
    }
}

