using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotPos
{
    public class BetNum
    {

        //保存投注号码串的List数组
        public List<string> lstbetnum = new List<string>();
        //保存send投注号码串
        public string sendbetnum = "";
        public string betnumstr;    
        public string fs;
        int betcount;

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
                    for (int i = (lstbetnum.Count / 7) * 7; i < lstbetnum.Count; i++)
                    {
                        if ((lstbetnum.Count % 6) == 0)
                        {
                            break;
                        }
                        if (lstbetnum[i] == strnum)
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
        /// 检验投注号码合法性，并写入待发送字符串中
        /// </summary>
        /// <param name="lstbox"></param>
        /// <param name="mul"></param>
        /// <param name="wf"></param>
        /// <param name="fs"></param>
        /// <returns></returns>
        public int MakeBetString(List<List<TextBox>> lstbox, string mul, int wf, int fs)
        {
            
            betcount = 5;       
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
                            betcount--;             //某行不足时，不对改行处理，同时注数减少一注
                            isenough = false;
                            break;
                        }
                    }
                    //检查是否有重号
                    for (int k = 0; k < lstcon.Count - 1; k++)
                    {
                        if (conl.Tag != null && conl.Tag.ToString() == ("blue" + i))    //不检查蓝球
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
                    sendbetnum += mul.PadLeft(3, '0') + geshu.PadLeft(2, '0');
                    for (int j = 0; j < lstcon.Count; j++)
                    {
                        Control conl = lstcon[j];
                        if (conl.Tag != null && conl.Tag.ToString() == ("blue" + i))
                        {
                            betnumstr += ";";
                            sendbetnum += "01";
                        }
                        sendbetnum += conl.Text;
                        betnumstr += conl.Text;
                    }
                }
                betnumstr += "|";
            }
            Console.Write(sendbetnum);
            return 0;
        }


        public void AloneBet(int wf, int fs, string mul)
        {
            DateTime dt = DateTime.Now;
            string cashdt = string.Format("{0:yyyy-MM-dd 00:00:00}", dt.AddDays(1));
            string betdt = string.Format("{0:yyyy-MM-dd HH:mm:ss.ffff}", dt);

            string codestr = "".PadRight(10,'0');//PosBack.xszbm + PosBack.zdh + PosBack.drawno[wf] + PosBack.lsh[wf] + betdt;

            //PosString postring = new PosString();
            //postring.Encode(alonestr);
            List<string>  alonestr = new List<string> {
                codestr.PadRight(15,'\0'),
                PosBack.xszbm.PadRight(10, '\0'),
                PosBack.zdh.PadRight(10, '\0'),
                PosBack.drawno[wf].PadRight(10, '\0'),
                PosBack.lsh[wf].PadRight(10, '\0'),
                betdt.PadRight(25, '\0'),
                cashdt.PadRight(25, '\0'),
                (fs.ToString()).PadRight(10, '\0'),
                (2 * Convert.ToInt16(mul) * betcount).ToString().PadRight(10, '\0'),
                betcount.ToString().PadRight(10, '\0'),
                mul.PadRight(10, '\0'),
                betnumstr.PadRight(50, '\0')
            };
            //alonestr.Add(codestr);
            //alonestr.Add(PosBack.xszbm);
            //alonestr.Add(PosBack.zdh);
            //alonestr.Add(PosBack.drawno[wf]);
            //alonestr.Add(PosBack.lsh[wf]);
            //alonestr.Add(betdt);
            //alonestr.Add(cashdt);
            //alonestr.Add(fs.ToString());
            //alonestr.Add((2 * Convert.ToInt16(mul) * betcount).ToString());
            //alonestr.Add(betcount.ToString());
            //alonestr.Add(mul);            
            //alonestr.Add(betnumstr);

            PosFile posfile = new PosFile();
            posfile.Filebet(alonestr);
            
        }


        //lot玩法选号检测
        public static int Lot_check_ball(string sredball, string sblueball, ref string str1, ref string str2)
        {
            string sred = sredball;
            string sblu = sblueball;
            int rballen = sred.Length;
            int blulen = sblu.Length;
            if (rballen % 2 != 0)
            {
                return -1;
            }
            if (blulen % 2 != 0)
            {
                return -2;
            }
            string sball = sred + sblu;
            int[] ckball = new int[rballen + blulen];
            for (int i = 0; i < rballen; i++)
            {
                ckball[i] = Convert.ToInt16(sball.Substring(i, 2));
                if (i > 0)
                {
                    if (ckball[i] == ckball[i - 1])
                    {
                        return -3;//投注号码有重复
                    }
                }
            }
            if (rballen + blulen == 10)
            {
                str1 = "1";
            }
            if (rballen + blulen > 10)
            {
                str1 = "2";
            }
            if (str1 == "1")
            {
                str2 = (2 * 1).ToString();
            }
            return 0;
        }

        //c515玩法选号检测
        public  int Cfof_check_ball(string sball, ref string str1, ref int str2)
        {
            int gameno = 1;
            string scfofball = sball;
            int sfofballen = sball.Length;

            if (sfofballen % 2 != 0 || sfofballen < 10 || sfofballen > 30)
            {
                return -1;      //投注号码个数有误
            }

            int[] ckball = new int[sfofballen / 2];
            for (int i = 0; i < sfofballen / 2; i++)
            {
                ckball[i] = Convert.ToInt16(scfofball.Substring((i * 2), 2));
                Console.WriteLine("iickball[" + i + "] = " + ckball[i].ToString().PadLeft(2, '0'));
                if (i > 0)
                {
                    foreach (int j in ckball)
                    {
                        Console.WriteLine("jjckball[" + j + "] = " + ckball[i].ToString().PadLeft(2, '0'));
                        if (ckball[i] == j)
                        {
                            return -3;//投注号码有重复
                        }
                    }
                }
                //Console.WriteLine("ckball[" + i + "] = " + ckball[i].ToString().PadLeft(2, '0'));
            }
            if (sfofballen == 10)
            {
                str1 = "1";
                str2 = 2 * 1;
            }
            if (sfofballen > 10)
            {
                str1 = "2";
                BetNum betnum = new BetNum();
                str2 = betnum.Count_cfof_num(gameno, sfofballen) * 2;
            }            
            return 0;
        }

        private int Count_cfof_num(int gameno, int sfoflen)
        {
            long tnum = 0;
            int num = 0;
            int gmno = gameno;
            int len = sfoflen / 2;
            long count1 = 1;
            long count2 = 1;
            if (gmno == 1)
            {
                for (int i = 1; i < len; i++)
                {
                    count1 = count1 * (i + 1);
                }
            }
            for (int i = 0; i < (len - 5); i++)
            {
                count2 = count2 * (i + 1);
            }
            tnum = count1 / (120 * count2);
            num = Convert.ToInt16(tnum);
            return num;
        }

    }
}
