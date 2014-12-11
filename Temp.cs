using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _3DSROMEDITOR {
    class Temp {

        private static string romfs_file = null;
        private static string patched_file;
        private static string crr_file;
        private static bool opened_romfs = false;
        private static bool opened_crr = false;
        private static UInt64 doffset_0;
        private static UInt64 doffset_1;
        private static UInt64 doffset_2;
        private static UInt64 hoffset_0;
        private static UInt64 hoffset_1;
        private static UInt64 hoffset_2;
        private static UInt64 length_0;
        private static UInt64 length_1;
        private static UInt64 length_2;
        private static uint[] bsize;
        private static UInt64 master_size;

        private static ulong align(ulong input, ulong alignsize) {
            if (input == 0) return 0;
            return ((input - 1) / alignsize + 1) * alignsize;
        }

        internal static void rehashRomfsTEMP(string path, string prodCode) {
            
            romfs_file = (path == null && prodCode == null) ? "" : path + prodCode + "-romfs.bin";
            bool valid = true;
            bsize = new uint[3];
            byte[] buffer = new byte[0x200];
            using (FileStream fs = new FileStream(romfs_file, FileMode.Open, FileAccess.Read)) {
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
            }
            if (BitConverter.ToUInt32(buffer, 0) != 0x43465649) //IVFC
                {
                valid = false;
                romfs_file = "";
            }
            if (valid) {
                int lastdot = romfs_file.LastIndexOf('.');
                patched_file = romfs_file.Substring(0, lastdot) + "_patched" + romfs_file.Substring(lastdot);
                opened_romfs = true;
                master_size = BitConverter.ToUInt64(buffer, 0x8);
                uint baseoffset = 0x60;
                bsize[0] = (uint)(1 << (int)(BitConverter.ToUInt32(buffer, 0x1C)));
                bsize[1] = (uint)(1 << (int)(BitConverter.ToUInt32(buffer, 0x34)));
                bsize[2] = (uint)(1 << (int)(BitConverter.ToUInt32(buffer, 0x4C)));
                ulong bodyoffset = align(baseoffset + master_size, bsize[2]);
                ulong bodysize = BitConverter.ToUInt32(buffer, 0x44);
                doffset_2 = bodyoffset;
                length_2 = align(bodysize, bsize[2]);

                hoffset_1 = align(bodyoffset + bodysize, bsize[2]);
                hoffset_2 = hoffset_1 + BitConverter.ToUInt32(buffer, 0x24) - BitConverter.ToUInt64(buffer, 0xC);

                doffset_1 = hoffset_2;
                length_1 = align(BitConverter.ToUInt64(buffer, 0x2C), bsize[1]);

                doffset_0 = hoffset_1;
                length_0 = align(BitConverter.ToUInt64(buffer, 0x14), bsize[0]);
            }
            patchHash();
        }


        private static void patchHash() {
            if (File.Exists(patched_file)) {
                File.Delete(patched_file);
            }
            File.Copy(romfs_file, patched_file);
            byte[] buffer = new byte[bsize[2]];
            byte[] goodhash = new byte[0x20];
            byte[] hashbuffer = new byte[0x20];
            int times2 = (int)(Math.Ceiling(length_2 / (double)bsize[2]));
            int times1 = (int)(Math.Ceiling(length_1 / (double)bsize[1]));
            int times0 = (int)(Math.Ceiling(length_0 / (double)bsize[0]));
            SHA256Managed Hasher = new SHA256Managed();
            try {
                using (FileStream fs2 = new FileStream(romfs_file, FileMode.Open, FileAccess.Read)) {
                    using (FileStream fs = new FileStream(romfs_file, FileMode.Open, FileAccess.Read), fs3 = new FileStream(patched_file, FileMode.Open, FileAccess.ReadWrite)) {
                        fs.Seek((long)doffset_2, SeekOrigin.Begin);
                        fs2.Seek((long)hoffset_2, SeekOrigin.Begin);

                        for (int i = 0; i < times2; i++) {
                            fs.Read(buffer, 0, (int)bsize[2]);
                            fs2.Read(goodhash, 0, 0x20);
                            hashbuffer = Hasher.ComputeHash(buffer);
                            if (!Enumerable.SequenceEqual(goodhash, hashbuffer)) {
                                fs3.Seek(fs2.Position - 0x20, SeekOrigin.Begin);
                                fs3.Write(hashbuffer, 0, 0x20);
                            }
                        }
                        fs.Close();
                        fs3.Close();
                    }
                    fs2.Close();
                }
                File.Delete(romfs_file);
                File.Copy(patched_file, romfs_file);
                using (FileStream fs2 = new FileStream(romfs_file, FileMode.Open, FileAccess.Read)) {
                    using (FileStream fs = new FileStream(romfs_file, FileMode.Open, FileAccess.Read), fs3 = new FileStream(patched_file, FileMode.Open, FileAccess.ReadWrite)) {
                        fs.Seek((long)doffset_1, SeekOrigin.Begin);
                        fs2.Seek((long)hoffset_1, SeekOrigin.Begin);
                        for (int i = 0; i < times1; i++) {
                            fs.Read(buffer, 0, (int)bsize[1]);
                            fs2.Read(goodhash, 0, 0x20);
                            hashbuffer = Hasher.ComputeHash(buffer);
                            if (!Enumerable.SequenceEqual(goodhash, hashbuffer)) {
                                fs3.Seek(fs2.Position - 0x20, SeekOrigin.Begin);
                                fs3.Write(hashbuffer, 0, 0x20);
                            }
                        }
                        fs.Close();
                        fs3.Close();
                    }
                    fs2.Close();
                }
                File.Delete(romfs_file);
                File.Copy(patched_file, romfs_file);
                using (FileStream fs2 = new FileStream(romfs_file, FileMode.Open, FileAccess.Read)) {
                    using (FileStream fs = new FileStream(romfs_file, FileMode.Open, FileAccess.Read), fs3 = new FileStream(patched_file, FileMode.Open, FileAccess.ReadWrite)) {
                        fs.Seek((long)doffset_0, SeekOrigin.Begin);
                        fs2.Seek((long)hoffset_0, SeekOrigin.Begin);
                        for (int i = 0; i < times0; i++) {
                            fs.Read(buffer, 0, (int)bsize[0]);
                            fs2.Read(goodhash, 0, 0x20);
                            hashbuffer = Hasher.ComputeHash(buffer);
                            if (!Enumerable.SequenceEqual(goodhash, hashbuffer)) {
                                fs3.Seek(fs2.Position - 0x20, SeekOrigin.Begin);
                                fs3.Write(hashbuffer, 0, 0x20);
                            }
                        }
                        fs.Close();
                        fs3.Close();
                    }
                    fs2.Close();
                }
                File.Delete(romfs_file);
                File.Copy(patched_file, romfs_file);
                File.Delete(patched_file);
            }
            catch (IOException exceptionsad) {

            }
        }
    }
}
