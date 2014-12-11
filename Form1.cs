using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;

namespace _3DSROMEDITOR {

    public partial class Form1 : Form {

        //DLL Imports
        [DllImport("Garc.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getgarcs(string file);

        public Form1() {
            if (checkUpdate() == -1) MessageBox.Show("A newer version of this software exists.");
            InitializeComponent();
        }

        //Variables
        private const int NCSD = GlobalVars.NCSD;
        private const int NCCH = GlobalVars.NCCH;
        private int seleNode;
        bool romfsEnc = true;

        private void openToolStrip_Click(object sender, EventArgs e) {
            treeView1.Nodes.Clear();
            if (openRom.ShowDialog() == DialogResult.OK) {
                GlobalVars.inst.rom = openRom.FileName;
                GlobalVars.inst.romPath = openRom.FileName.Substring(0, openRom.FileName.LastIndexOf("\\")) + "\\";

                BinaryReader b = new BinaryReader(File.Open(GlobalVars.inst.rom, FileMode.Open));

                        //Idiot proofing
                        b.BaseStream.Position = NCSD + 0x100;
                        string ncsdMagic = b.ReadBytes(0x4).bytes2str();
                        b.BaseStream.Position = NCCH + 0x100;
                        string ncchMagic = b.ReadBytes(0x4).bytes2str();
                        if (String.Compare(ncsdMagic, "NCSD") == -1 || String.Compare(ncchMagic, "NCCH") == -1) {
                            MessageBox.Show("Error: No NCSD or NCCH detected.");
                            return;
                        }

                        //Set length
                        b.BaseStream.Position = 0;
                        GlobalVars.inst.ncsdSize = (int)b.BaseStream.Length;

                        //Set Product Code
                        b.BaseStream.Position = NCCH + 0x150;
                        GlobalVars.inst.productCode = (b.ReadBytes(0xA)).bytes2str();
                        treeView1.Nodes.Add(GlobalVars.inst.productCode);

                        //Set ROMFS offset and size
                        b.BaseStream.Position = NCCH + 0x1B0;
                        GlobalVars.inst.romfsOff = NCCH + (b.ReadBytes(0x4)).getU32() * 0x200;

                        b.BaseStream.Position = NCCH + 0x1B4;
                        GlobalVars.inst.romfsSize = (b.ReadBytes(0x4)).getU32() * 0x200;

                        b.BaseStream.Position = NCCH + 0x1B8;
                        GlobalVars.inst.romfsSuperhashSize = (b.ReadBytes(0x4)).getU32() * 0x200;

                        b.BaseStream.Position = GlobalVars.inst.romfsOff;
                        romfsEnc = (b.ReadBytes(0x4)).getU32() != 1128683081 ? true : false ; //Look for IVFC magic

                        b.Close();

                    new Thread(() => {
                        if (!File.Exists(GlobalVars.inst.romPath + GlobalVars.inst.productCode + "-romfs.bin")) {
                            //Dump decrypted ROMFS
                            if(romfsEnc){
                                if (!File.Exists(GlobalVars.inst.romPath + GlobalVars.inst.productCode + "0.romfs.xorpad")) { MessageBox.Show("Error: RomFS xorpad not found.");  return; }
                                ThreadSafe(() => statusLbl.Text = "Decrypting RomFS...");
                                if (Crypto.romFS(GlobalVars.inst.romPath, GlobalVars.inst.productCode, GlobalVars.inst.rom.Substring(GlobalVars.inst.romPath.Length), GlobalVars.inst.romfsOff, GlobalVars.inst.romfsSize, 0) == -1) { MessageBox.Show("Error decrypting RomFS"); return; }
                            
                            }else{
                                ThreadSafe(() => statusLbl.Text = "Dumping contents...");
                                if (Crypto.romFS(GlobalVars.inst.romPath, GlobalVars.inst.productCode, GlobalVars.inst.rom.Substring(GlobalVars.inst.romPath.Length), GlobalVars.inst.romfsOff, GlobalVars.inst.romfsSize, 2) == -1) { MessageBox.Show("Error dumping RomFS"); return; }
                            }
                        }
                        ThreadSafe(() => statusLbl.Text = "Loading RomFS...");
                        loadGarcs();
                        ThreadSafe(() => statusLbl.Text = "Done!");
                    }).Start();
            }
        }

        private void addTreeBranch(string tree, string[] branches) {
            List<TreeNode> tna = new List<TreeNode>();
            ContextMenuStrip docMenu = new ContextMenuStrip();

            //Create some menu items.
            ToolStripMenuItem importLbl = new ToolStripMenuItem();
            importLbl.Text = "Import";
            ToolStripMenuItem exportLbl = new ToolStripMenuItem();
            exportLbl.Text = "Export";

            //Add the menu items to the menu.
            docMenu.Items.AddRange(new ToolStripMenuItem[]{importLbl, 
        exportLbl});
            docMenu.ItemClicked += new ToolStripItemClickedEventHandler(
                docMenu_Clicked);

            for (int i = 0; i < branches.Length; i++ ) {
                TreeNode treeBranch = new TreeNode(branches[i]);
                treeBranch.ContextMenuStrip = docMenu;
                tna.Add(treeBranch);
            }
            TreeNode[] array = tna.ToArray();
            TreeNode treeN = new TreeNode(tree, array);
            treeView1.Invoke((MethodInvoker) (() => treeView1.Nodes[0].Nodes.Add(treeN)));
        }

        void docMenu_Clicked(object sender, ToolStripItemClickedEventArgs e) {
            ToolStripItem item = e.ClickedItem;
                
            if(item.Text == "Import"){
                ThreadSafe(() => statusLbl.Text = "Packing GARC...");
                FileRW.packGarcFile(seleNode);
                ThreadSafe(() => statusLbl.Text = "Done!");
            }else if (item.Text == "Export"){
                ThreadSafe(() => statusLbl.Text = "Writing GARC...");
                FileRW.writeGarcFile(seleNode);
                ThreadSafe(() => statusLbl.Text = "Done!");
            }
        }

        private void treeView1_Afterselect(object sender, TreeViewEventArgs e) {

            seleNode = e.Node.TreeView.SelectedNode.Index;

            if(e.Node.Text == "NCSD"){
                ThreadSafe(() => infoBox.Text = "NCSD:" + Environment.NewLine 
                                               + "Image size: " + GlobalVars.inst.ncsdSize + " bytes (0x" + GlobalVars.inst.ncsdSize.ToString("X") + ")" + Environment.NewLine);
            }
            else if (e.Node.Text == "NCCH") {
                ThreadSafe(() => infoBox.Text = "NCCH info here!");
            }
            else if (e.Node.Text.StartsWith("GARC")) {
                ThreadSafe(() => infoBox.Text = "GARC " + seleNode + " info here!");
            }
        }

        void loadGarcs() {
            IntPtr g = getgarcs(GlobalVars.inst.romPath + GlobalVars.inst.productCode + "-romfs.bin");

            //get length of array
            int[] temp = new int[1];
            Marshal.Copy(g, temp, 0, 1);

            //copy array using length
            GlobalVars.inst.garcOffs = new int[temp[0]+1];
            Marshal.Copy(g, GlobalVars.inst.garcOffs, 0, temp[0]+1);
            GlobalVars.inst.garcOffs = GlobalVars.inst.garcOffs.Skip(1).ToArray();

            //convert int[] to string[] and format
            string[] garcStrs = new string[GlobalVars.inst.garcOffs.Length];
            for (int i = 0; i < GlobalVars.inst.garcOffs.Length; i++) {
                garcStrs[i] = "GARC " + i.ToString("D3") + " [0x" + GlobalVars.inst.garcOffs[i].ToString("X8") + "]";
            }

            //load tree view
            addTreeBranch("NCSD", new string[] { });
            addTreeBranch("NCCH", garcStrs);
        }

        private string parseGarcInfo(uint off) {
            if (!File.Exists(GlobalVars.inst.romPath + GlobalVars.inst.productCode + "-romfs.bin")) { return null; }
            BinaryReader b = new BinaryReader(File.Open(GlobalVars.inst.romPath + GlobalVars.inst.productCode + "-romfs.bin", FileMode.Open));
            b.BaseStream.Position = (long)off;
            string str = "Magic: " + Encoding.ASCII.GetString(b.ReadBytes(4)) + "\r\n" + "Header size: " + (b.ReadBytes(4).getU32()).ToString("X8");
            b.Close();
            return str;
        }


        private void exitToolStrip_Click(object sender, EventArgs e) {
            if (Application.MessageLoop) { Application.Exit(); } else { Environment.Exit(1); }   
        }

        private void rebuildRomFS_Click(object sender, EventArgs e) {
            string romfs = null;
            if(GlobalVars.inst.romPath == null && GlobalVars.inst.productCode == null){
                if (openRomfs.ShowDialog() == DialogResult.OK) romfs = openRomfs.FileName;
                GlobalVars.inst.romPath = romfs.Substring(0, openRomfs.FileName.LastIndexOf("\\")) + "\\";
                string pcode = romfs.Substring(romfs.LastIndexOf("\\") + 1);
                GlobalVars.inst.productCode = pcode.Substring(0, pcode.IndexOf("-romfs.bin"));
            }
            new Thread(() => {
                ThreadSafe(() => statusLbl.Text = "Calculating RomFS Hashes...");
                if (Crypto.calculateRomFS(GlobalVars.inst.romPath, GlobalVars.inst.productCode) == -1) { MessageBox.Show("Error calculating RomFS hashes"); return; }
                ThreadSafe(() => statusLbl.Text = "Calculating Super Hashes...");
                if (Crypto.calculateNCCH(GlobalVars.inst.romPath, GlobalVars.inst.productCode) == -1) { MessageBox.Show("Error calculating NCCH hashes"); return; }
                if (romfsEnc) {
                    ThreadSafe(() => statusLbl.Text = "Encrypting RomFS...");
                    if (Crypto.romFS(GlobalVars.inst.romPath, GlobalVars.inst.productCode, GlobalVars.inst.rom.Substring(GlobalVars.inst.romPath.Length), GlobalVars.inst.romfsOff, GlobalVars.inst.romfsSize, 1) == -1) { MessageBox.Show("Error encrypting RomFS"); return; }
                }
                else {
                    ThreadSafe(() => statusLbl.Text = "Encrypting RomFS...");
                    if (Crypto.romFS(GlobalVars.inst.romPath, GlobalVars.inst.productCode, GlobalVars.inst.rom.Substring(GlobalVars.inst.romPath.Length), GlobalVars.inst.romfsOff, GlobalVars.inst.romfsSize, 3) == -1) { MessageBox.Show("Error encrypting RomFS"); return; }
                }
            }).Start();
            ThreadSafe(() => statusLbl.Text = "Done!");
        }

        private void ThreadSafe(MethodInvoker method) {
            if (InvokeRequired)
                Invoke(method);
            else
                method();
        }

        private void makeRSFFileToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("No.");
        }

        private void aboutBut_Click(object sender, EventArgs e) {
            MessageBox.Show("3DSRE by Reisyukaku" + Environment.NewLine
                          + "Version: " + GlobalVars.Version + Environment.NewLine);
        }
    }
}

