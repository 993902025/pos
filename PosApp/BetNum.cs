using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotPos
{
    public class BetNum
    {
        //lot玩法选号检测
        public static int lot_check_ball(string sredball, string sblueball, ref string str1, ref string str2)
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
        public  int cfof_check_ball(string sball, ref string str1, ref int str2)
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
                str2 = betnum.count_cfof_num(gameno, sfofballen) * 2;
            }            
            return 0;
        }

        private int count_cfof_num(int gameno, int sfoflen)
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
