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
        public static string[] gamename = new string[6];
        public static string[] drawno = new string[6];
        public static string xszbm;
        public static string[] lsh = new string[6];
        public static string smallcount;
        public static string balance;
        public static string tqtime;
        public static string zdh;

        const string SEP2 = "$";

        string mac;
        string pink;

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
            xszbm = PosConfig.xszbm;
            zdh = PosConfig.zdh;
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
                        gamename[1] = pravalue[0];
                        break;
                    case 1:
                        drawno[1] = pravalue[1];
                        break;
                    case 2:
                        xszbm = pravalue[2];
                        break;
                    case 3:
                        lsh[1] = pravalue[3];
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
            const string SERVERCONNECT = "SERVERCONNECT";   //操作码
            string sendmsg = "";    //待发送串
            string recvmsg = "";    //接受串
            string[] sarray;    //解析结果存放数组

            string headmsg = PosConfig.xszbm + SEP2 + PosConfig.zdh + SEP2 + 0 + SEP2;

            PosString posstr = new PosString();
            posstr.IniMsgStr("1", SERVERCONNECT, headmsg, ref sendmsg);
            //发送
            sock.Sendmsg(sendmsg);
            //接受
            recvmsg = sock.Recvmsg();

            //解析recvmsg内容，取出acceptor的ip和port
            sarray = recvmsg.Split('|');    //此处可考虑在PosString类中添加方法具体实现
            sock.serverIP = sarray[8];    //
            sock.port = Convert.ToInt16(sarray[9]);

            return 0;
        }
        //
        /// <summary>
        /// 签到Register
        /// </summary>
        /// <param name="sock"></param>
        void Register(SocketClass sock)
        {
            const string REGISTER = "REGISTER";
            string sendmsg = "";
            string recvmsg = "";
            string[] sarray;

            string headmsg = PosConfig.xszbm + SEP2 + PosConfig.zdh + SEP2 + 0 + SEP2;

            PosString posstr = new PosString();

            posstr.IniMsgStr("1", REGISTER, headmsg, ref sendmsg);

            sock.Sendmsg(sendmsg);

            recvmsg = sock.Recvmsg();
        }

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

        

        

    }
}
