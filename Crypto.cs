using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace _3DSROMEDITOR {
    class Crypto {

        [DllImport("Crypto.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int cryptRomFS(string path, string prodCode, string romname, uint romfsOff, uint fsSize, int mode);
        //private static extern int rehashRomfs(string path, string prodCode);

        internal static int romFS(string path, string prodCode, string romname, uint romfsOff, uint fsSize, int mode) {
            return cryptRomFS(path, prodCode, romname, romfsOff, fsSize, mode); //mode: 0=decrypt romfs and write to dir; 1=encrypt romfs and write to rom; 
                                                                                //      2=no crypt and write to dir; 3=no crypt and write to rom
        }

        internal static int calculateRomFS(string path, string prodCode) {
            Temp.rehashRomfsTEMP(path, prodCode);
            return 0;
        }

        internal static int calculateNCCH(string path, string prodCode) {
            uint hashBuffSize = 0;
            if (GlobalVars.inst.romfsSuperhashSize == 0) return -2;
            byte[] buff = new byte[hashBuffSize];
            byte[] hash = new byte[0x20];
            FileStream fr = new FileStream(path + prodCode + "-romfs.bin", FileMode.Open, FileAccess.Read);
            fr.Seek(0, SeekOrigin.Begin);
            fr.Read(buff, 0, buff.Length);
            SHA256Managed Hasher = new SHA256Managed();
            hash = Hasher.ComputeHash(buff);

            if (GlobalVars.inst.rom != null) {
                using (Stream fw = File.Open(GlobalVars.inst.rom, FileMode.Open, FileAccess.ReadWrite)) {
                    fw.Position = GlobalVars.NCCH + 0x1E0;
                    fw.Write(hash, 0, 0x20);
                    fw.Position = 0x1000 + 0x1E0;
                    fw.Write(hash, 0, 0x20);
                    fw.Close();
                }
            } else {
                MessageBox.Show("Super Hash: " + BitConverter.ToString(hash).Replace("-", ""));
            }
            fr.Close();
            return 0;
        }
    }
}
