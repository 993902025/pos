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

            if (!File.Exists(_path + _filename))
            {
                try
                {
                    Console.WriteLine(@".\" + _filename);
                    File.Create(@".\" + _filename).Close();
                }
                catch (Exception)
                {
                    throw;
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
                        if (temstr.IndexOf(key) >= 0)
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
