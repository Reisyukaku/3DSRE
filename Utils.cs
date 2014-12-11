using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSROMEDITOR {
    public static class Utils {

        public static uint getU32(this byte[] b) {
            uint result = 0;
            for (int byteP = 0; byteP < 4; byteP++) result |= ((uint)b[byteP]) << (8 * byteP);
            return result;
        }

        public static uint getU64(this byte[] b) {
            uint result = 0;
            for (int byteP = 0; byteP < 8; byteP++) result |= ((uint)b[byteP]) << (8 * byteP);
            return result;
        }

        public static string bytes2str(this byte[] str) {
            return Encoding.ASCII.GetString(str);
        }
    }
}
