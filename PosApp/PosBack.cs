using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LotPos
{
    public class PosBack
    {

        /*
         * 取参参数
         * 
         */
        //string _agentpra, _pra;
        public string gamename;
        public string drawno;
        public string xszbm;
        public string lsh;
        public string smallcount;
        public string balance;
        public string tqtime;

        List<string> prakey = new List<string> { "gamename", "drawno", "xszbm", "lsh" ,"smallcount","balance","tqtime"};
        List<string> pravalue = new List<string>();
        //取参



            //联机模式取参
        //public int GetPra(ref string pra)
        //{
            
        //     _pra = pra;
        //    int re_num = 0;
        //    return re_num;
        //}

        //单机模式 模拟取参
        public int GetPra()
        {
            PosFile pf = new PosFile();
            for (int i = 0; i < prakey.Count; i++)
            {
                string temvalue = string.Empty;
                pf.AnalysisFile(prakey[i], ref temvalue);
                pravalue.Add(temvalue);
                switch (i)
                {
                    case 0:
                        gamename = pravalue[0];
                        break;
                    case 1:
                        drawno = pravalue[1];
                        break;
                    case 2:
                        xszbm = pravalue[2];
                        break;
                    case 3:
                        lsh = pravalue[3];
                        break;
                    case 4:
                        smallcount = pravalue[4];
                        break;
                    case 5:
                        balance = pravalue[5];
                        break;
                    case 6:
                        tqtime = pravalue[6];
                        break;
                    default:
                        break;
                }
            }
            
            return 0;
        }

        //登录验证

        //

            //按键是否为数字键
        public static bool IsNumber(string str)
        {
            string regextext = @"^(-?\d+)(\.\d+)?$";
            Regex regex = new Regex(regextext, RegexOptions.None);
            return regex.IsMatch(str.Trim());
        }
        //按键是否为字母键
        public static bool IsLetter(string str)
        {
            string regextext = @"^[A-Za-z]+$";
            Regex regex = new Regex(regextext, RegexOptions.None);
            return regex.IsMatch(str.Trim());
        }


        //投注F8
    }
}
