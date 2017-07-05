using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace LotPos
{
    public class BzWebSec
    {
        [DllImport("BzWebSec.dll", EntryPoint = "WebMD5String32")]
        public static extern void WebMD5String32(string cInfo, StringBuilder cDigest);

        [DllImport("E:\\siguoyi\\123\\pos\\LotPos\\bin\\Debug\\BzWebSec.dll", EntryPoint = "WebMD5File")]
        public static extern void WebMD5File(string cInfo, StringBuilder cDigest);

        [DllImport("E:\\siguoyi\\123\\pos\\LotPos\\bin\\Debug\\BzWebSec.dll", EntryPoint = "WebEncodeFile")]
        public static extern int WebEncodeFile(string source_fn, string key, StringBuilder result_fn);

        [DllImport("E:\\siguoyi\\123\\pos\\LotPos\\bin\\Debug\\BzWebSec.dll", EntryPoint = "WebCalculateMAC")]
        public static extern int WebCalculateMAC(string datastr, string mac);

        [DllImport("E:\\siguoyi\\123\\pos\\LotPos\\bin\\Debug\\BzWebSec.dll", EntryPoint = "WebEncodeString")]
        public static extern int WebEncodeString(string source_str, string key, StringBuilder result_str);

        [DllImport("E:\\siguoyi\\123\\pos\\LotPos\\bin\\Debug\\BzWebSec.dll", EntryPoint = "WebDecodeString")]
        public static extern int WebDecodeString(string source_str, string key, StringBuilder result_str);

        [DllImport("E:\\siguoyi\\123\\pos\\LotPos\\bin\\Debug\\BzWebSec.dll", EntryPoint = "WebMD5String16")]
        public static extern void WebMD5String16(string cInfo, string cDigest);


    }
}
