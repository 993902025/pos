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
        public string _parfilename;    // 单机取参文件名
        public string _betfilename;    // 单机投注文件名
        string _path;               // 程序路径
        //public string _str_result; //存读到的值

        //F:\123\C#/pos/PosApp/bin/Debug/LotPos.exe
        /// <summary>
        /// 初始化模拟取参文件，如果缺少文件则创建文件模板
        /// </summary>
        public  PosFile()
        {
            _path = Application.StartupPath;//@".\"
            _parfilename = Process.GetCurrentProcess().ProcessName + "Par"+ ".txt";
            
            if (!File.Exists(_path + "\\" +  _parfilename))
            {
                try
                {
                    Console.WriteLine("\r\n初始化模拟参数文件：" + _path + "\\" + _parfilename);
                    string inistr = 
                        "gamename=Lot\r\n\r\n" +
                        "drawno=2017099\r\n\r\n" +
                        "xszbm=33010123\r\n\r\n" +
                        "lsh=123456\r\n\r\n" +
                        "smallcount=1\r\n\r\n" +
                        "balance=999999.98\r\n\r\n" +
                        "tqtime=11:44:55\r\n".ToUpper();
                    FileStream newfile = File.Create(_path + "\\" + _parfilename);
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
        //        StreamReader sread = new StreamReader(_path + @"\" + _parfilename);

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
                if (File.Exists(_path + "\\" + _parfilename))
                {
                    StreamReader sread = new StreamReader(_path + "\\" + _parfilename);
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


        public void Filebet(List<string> strlst)
        {
            DateTime dt = DateTime.Now; 
            _betfilename = Process.GetCurrentProcess().ProcessName + "Bet" + "_" + PosBack.drawno[1] + string.Format("{0:yyyy-MM-dd}", dt) + ".txt";

            if (!File.Exists(_path + "\\" + _betfilename))
            {
                try
                {
                    Console.WriteLine("\r\n初始化单机投注文件：" + _path + "\\" + _betfilename);
                    string inistr =
                        "SIGLECPKEY".PadRight(15,'\0') +
                "XSZBM".PadRight(10, '\0') +
                "ZDH".PadRight(10, '\0') +
                "DRAWNO".PadRight(10, '\0') +
                "LSH".PadRight(10, '\0') +
                "BETDATE".PadRight(25, '\0') +
                "CASHDATE".PadRight(25, '\0') +
                "BETTYPE".PadRight(10, '\0') +
                "AMOUNT".PadRight(10, '\0') +
                "COUNT".PadRight(10, '\0') +
                "MULTIPLE".PadRight(10, '\0') +
                "BETNUMBER".PadRight(50, '\0');
                    FileStream newfile = File.Create(_path + "\\" + _betfilename);
                    newfile.Write(Encoding.UTF8.GetBytes(inistr), 0, inistr.Length);
                    newfile.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            try
            {
                FileStream newfile = new FileStream(_path + "\\" + _betfilename,FileMode.Append);

                newfile.Write(Encoding.UTF8.GetBytes("\r\n"), 0, 2);

                for (int i = 0; i < strlst.Count; i++)
                {
                    newfile.Write(Encoding.UTF8.GetBytes(strlst[i]), 0, strlst[i].Length);
                }
                newfile.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        
    }
}
