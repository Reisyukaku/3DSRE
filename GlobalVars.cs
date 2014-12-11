using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSROMEDITOR {
    public sealed class GlobalVars {
        public static GlobalVars inst = new GlobalVars();

        private GlobalVars() { }
        public const string Version = "0.0.1a";

        //Important Offsets
        public const int NCSD = 0x0;
        public const int NCCH = 0x4000;
        public string rom { get; set; }
        public string romPath { get; set; }

        public int ncsdSize { get; set; }
        public string productCode { get; set; }

        public uint romfsOff { get; set; }
        public uint romfsSize { get; set; }

        public int[] garcOffs { get; set; }
        public int seleNode { get; set; }
        public uint romfsSuperhashSize { get; set; }
    }
}
