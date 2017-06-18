using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace LotPos
{
    class PosFile
    {
        public string _filename;
        public string _str_result; //存读到的值
        string _path;

        //F:\123\C#/pos/PosApp/bin/Debug/LotPos.exe
        public  PosFile()
        {
            _path = Application.StartupPath;
            _filename = Process.GetCurrentProcess().ProcessName + ".txt";

            if (!File.Exists(@".\" +  _filename))
            {
                try
                {
                    Console.WriteLine("初始化模拟参数文件：" + @".\" + _filename);
                    string inistr = "gamename=\r\n\r\ndrawno=\r\n\r\nxszbm=\r\n\r\nlsh=\r\n\r\nsmallcount=\r\n\r\nbalance=\r\n\r\ntqtime=\r\n".ToUpper();
                    FileStream newfile = File.Create(@".\" + _filename);
                    newfile.Write(Encoding.UTF8.GetBytes(inistr),0, inistr.Length);
                    newfile.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //public string ReadFile()
        //{
        //    try
        //    {
        //        StreamReader sread = new StreamReader(_path + @"\" + _filename);

        //        do
        //        {
        //            _str_result += sread.ReadLine().ToString();
        //        }
        //        while ( !sread.EndOfStream );
        //    }
        //    catch (Exception exc)
        //    {

        //        return exc.ToString();
        //        throw;
        //    }
        //    return _str_result;            
        //}


        /// <summary>
        /// 用于单机模拟取参,传入要查询的key，获取对应的value
        /// 文件格式，处理了忽略空格
        /// key1=value1
        /// key2=value2
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AnalysisFile(string key, ref string value)
        {
            string temstr = string.Empty;
            try
            {
                if (!File.Exists(_path + _filename))
                {
                    StreamReader sread = new StreamReader(@".\" + _filename);
                    while (!sread.EndOfStream)
                    {
                        temstr = Regex.Replace(sread.ReadLine(), @"\s", "");
                        
                        if ( temstr.IndexOf(key,StringComparison.OrdinalIgnoreCase) >= 0 && temstr.Length >= key.Length + 1)//使用IndexOf()进行字符串 temstr 内查找 key (将第二个参数设置为StringComparison.OrdinalIgnoreCase 以忽略大小写)*
                        {
                            value = temstr.Substring(key.Length + 1, temstr.Length - key.Length - 1);
                            break;
                        }
                    }
                    sread.Close();
                }                
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
                throw;
            }
        }
    }
}
