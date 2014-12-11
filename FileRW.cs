using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace _3DSROMEDITOR {
    class FileRW {

        //DLL Imports
        [DllImport("Garc.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void writegarc(string inFile, string outFile, int garcOff, int len);

        internal static void writeGarcFile(int index) {
            writegarc(GlobalVars.inst.romPath + GlobalVars.inst.productCode + "-romfs.bin", GlobalVars.inst.romPath + GlobalVars.inst.productCode + "-" + index.ToString("D3") + ".GARC", GlobalVars.inst.garcOffs[index], GlobalVars.inst.garcOffs[index + 1] - GlobalVars.inst.garcOffs[index]);
        }

        internal static void packGarcFile(int index) {
            uint off = (uint)GlobalVars.inst.garcOffs[index];
            uint size = index >= GlobalVars.inst.garcOffs.Length - 1 ? (uint)(GlobalVars.inst.romfsSize - GlobalVars.inst.garcOffs[index]) : (uint)(GlobalVars.inst.garcOffs[index + 1] - GlobalVars.inst.garcOffs[index]);
            MessageBox.Show("Off=0x" + off.ToString("X") + " | Size=0x" + size.ToString("X"));
        }
    }
}
