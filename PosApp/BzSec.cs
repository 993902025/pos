using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LotPos
{
    public class BzSec
    {
        [DllImport(@"E:\siguoyi\123\pos\bzsec.dll", EntryPoint = "checkmac")]
        public static extern void checkmac(string str, string result, string key);

        [DllImport(@"E:\siguoyi\123\pos\bzsec.dll", EntryPoint = "encode", CallingConvention = CallingConvention.Cdecl)]
        public static extern void encode(string str, StringBuilder result, string key);

        [DllImport(@"E:\siguoyi\123\pos\bzsec.dll", EntryPoint = "calmac", CallingConvention = CallingConvention.Cdecl)]
        public static extern void calmac(string packstr, StringBuilder mac, string key);
    }
}
