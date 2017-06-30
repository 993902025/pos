using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
        public static string gamename;
        public static string drawno;
        public static string xszbm;
        public static string lsh;
        public static string smallcount;
        public static string balance;
        public static string tqtime;

        const string SEP2 = "$";
        

        List<string> prakey = new List<string> {
            "gamename",
            "drawno",
            "xszbm",
            "lsh" ,
            "smallcount",
            "balance",
            "tqtime" };

        List<string> pravalue = new List<string>();
        
        
        public PosBack()
        {
        }

        #region 取参 

        /// <summary>
        /// 取参导向 根据启动模式选择
        /// </summary>
        /// <param name="Pattern"></param>
        /// <returns></returns>
        public int GetPra(string Pattern)
        {
            switch (Pattern)
            {
                case "1":
                    return AloneGetPra();
                case "2":
                    return OnLineGetPra();
                default:
                    return -20;
            }
        }

        /// <summary>
        /// 联网模式取参
        /// </summary>
        /// <returns></returns>
        public int OnLineGetPra()
        {
            return -1;
        }

        
        /// <summary>
        /// 单机模式 模拟取参
        /// </summary>
        /// <returns></returns>
        public int AloneGetPra( )
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

        #endregion

        public int Con_Director(SocketClass sock)
        {
            const string SERVERCONNECT = "SERVERCONNECT";
            string sendmsg = "";
            string headmsg = PosConfig.xszbm + SEP2 + PosConfig.zdh + SEP2 + 0 + SEP2;
            PosString posstr = new PosString();
            posstr.IniMsgStr("1", SERVERCONNECT, headmsg, ref sendmsg);
            sock.Sendmsg(sendmsg);

            sock.Recvmsg();
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

        void Register(Socket sock)
        {
            const string REGISTER = "REGISTER";
            string sendmsg = "";
            string headmsg = PosConfig.xszbm + SEP2 + PosConfig.zdh + SEP2 + 0 + SEP2;

            PosString posstr = new PosString();

            posstr.IniMsgStr("1", REGISTER, headmsg, ref sendmsg);

           

            //sock.Send();

            //sock.Receive();
        }

        

    }
}
