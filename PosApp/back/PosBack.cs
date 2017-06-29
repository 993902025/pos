using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public static List<Control> tboxlist;

        List<string> prakey = new List<string> { "gamename", "drawno", "xszbm", "lsh" ,"smallcount","balance","tqtime"};
        List<string> pravalue = new List<string>();
        

        public PosBack()
        {
            tboxlist = new List<Control>();
        }

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

        public int Con_Director(string ip, int port)
        {

            //socket 类处理
            return 0;
        }
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

        //保存投注号码串的List数组
        public List<string> lstbetnum = new List<string>();
        //保存send投注号码串
        public string sendbetnum = "";

        /// <summary>
        /// 将合法数据放入号码串数组
        /// </summary>
        /// <param name="strnum"></param>
        /// <param name="wf"></param>
        /// <returns></returns>
        public int AddListBetNum(int wf, string strnum, string mul)
        {
            switch (wf)
            {
                case 0: //c515 5
                    for (int i = 0; i < lstbetnum.Count; i++)
                    {
                        if (lstbetnum[i] == strnum)
                        {
                            return -1;
                        }                        
                    }
                    break;
                case 1: //lot 6 - 1
                    for (int i = (lstbetnum.Count/7) * 7; i < lstbetnum.Count; i++)
                    {
                        if ((lstbetnum.Count % 6) == 0)
                        {
                            break;
                        }
                        if (lstbetnum[ i ] == strnum)
                        {
                            return -1;
                        }
                    }
                    break;
                default:
                    break;
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstbox"></param>
        /// <param name="mul"></param>
        /// <param name="wf"></param>
        /// <param name="fs"></param>
        /// <returns></returns>
        public int MakeBetString(List<List<TextBox>> lstbox,string mul, int wf, int fs)
        {
            bool isenough = true;       //该行号码个数是否足够，true=足够，false不足；
            string geshu = string.Empty;
            for (int i = 0; i < lstbox.Count; i++)
            {
                List<TextBox> lstcon = lstbox[i];
                geshu = (lstcon.Count - 1).ToString();
                //遍历检验每一行的值合法性 是否有空 或者是否 有重号
                for (int j = 0; j < lstcon.Count; j++)
                {
                    Control conl = lstcon[j];
                    if (conl.Text == string.Empty)
                    {
                        if (lstcon == lstbox[0])
                        {                           
                            return -1;
                        }
                        else
                        {
                            isenough = false;
                            break;
                        }
                    }
                    for (int k = 0; k < lstcon.Count - 1; k++)
                    {
                        if (conl.Tag != null && conl.Tag.ToString() == ("blue" + i))
                        {
                            break;
                        }
                        if (j != k && conl.Text == lstcon[k].Text)
                        {
                            return -2;
                        }
                    }
                    isenough = true;
                }
                //对可以组成投注号码的行，将值取出放到字符串中保存用于传输使用
                if (isenough)   //当该行足够时，加前缀，组串
                {
                    sendbetnum += mul.PadLeft(3,'0') + geshu.PadLeft(2, '0');
                    for (int j = 0; j < lstcon.Count; j++)
                    {
                        Control conl = lstcon[j];
                        if (conl.Tag != null && conl.Tag.ToString() == ("blue" + i))
                        {
                            sendbetnum += "01";
                        }
                        sendbetnum += conl.Text;
                    }
                }
            }
            Console.Write(sendbetnum);
            return 0;
        }


        public void SortBetNum()
        {
            Control strtemp;
            for (int i = tboxlist.Count -1; i > 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    //if (Convert.ToInt16(tboxlist[i].Text) < Convert.ToInt16(tboxlist[j].Text))
                    if (string.Compare(tboxlist[i].Name, tboxlist[j].Name) < 0)
                    {
                        strtemp = tboxlist[j];
                        tboxlist[j] = tboxlist[i];
                        tboxlist[i] = strtemp;
                    }
                }
            }
        }

        public void CheckBetNum()
        {
            Control strtemp;
            for (int i = 0 ; i < tboxlist.Count - 1; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    //if (Convert.ToInt16(tboxlist[i].Text) < Convert.ToInt16(tboxlist[j].Text))
                    if (string.Compare(tboxlist[i].Name, tboxlist[j].Name) > 0)
                    {
                        strtemp = tboxlist[j];
                        tboxlist[j] = tboxlist[i];
                        tboxlist[i] = strtemp;
                    }
                }
            }
        }
    }
}
