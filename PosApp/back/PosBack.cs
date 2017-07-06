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

        string appid;
        string mackey;
        string pinkey;

        //List<List<string>> pralst = new List<List<string>>(6);
        public static string[][] pralst;    //保存每个玩法取参结果的二维数组

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
        
        public void NewSock(SocketClass sock)
        {            
            sock.Inisocket();
        }
                
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
                   // return OnLineGetPra(sock);
                default:
                    return -20;
            }
        }

        /// <summary>
        /// 联网模式取参
        /// </summary>
        /// <returns></returns>
        public int OnLineGetPra(SocketClass sock)
        {
            const string GETPARAM = "GETPARAM";   //操作码
            string sendmsg = "";    //待发送串
            string recvmsg = "";    //接受串
            string[] sarray;    //解析结果存放数组

            string headmsg = PosConfig.xszbm + SEP2 + PosConfig.zdh + SEP2 + appid;
            
            IniMsgStr("1", GETPARAM, headmsg, ref sendmsg);
            //发送
            sock.Sendmsg(sendmsg);
            //接受
            recvmsg = sock.Recvmsg();
            //Console.WriteLine("GETPARAM recvmsg:\t" + recvmsg);
            //解析recvmsg内容，取出acceptor的ip和port

            sarray = recvmsg.Split('|');

            string[] prasplitgame = Decode(sarray[6], pinkey).Substring(2).Split('#');
            pralst = new string[prasplitgame.Length][];

            for (int i = 0; i < prasplitgame.Length; i++)
            {
                pralst[i] = prasplitgame[i].Split('$');
            }

            return 0;
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
        
        public int Con_Director()
        {
            Socket sock2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sock2.Connect(PosConfig.ServerIP, PosConfig.Port);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inisocket\t" + ex.Message);
                return -4;
            }
            const string SERVERCONNECT = "SERVERCONNECT";   //操作码
            string sendmsg = "";    //待发送串
            string recvmsg = "";    //接受串
            string[] sarray;    //解析结果存放数组
            byte[] brecmsg = new byte[4096];

            string msgbody = PosConfig.xszbm + SEP2 + PosConfig.zdh + SEP2 + 0 + SEP2;
            
            IniMsgStr("1", SERVERCONNECT, msgbody, ref sendmsg);
            //发送
            sock2.Send(Encoding.ASCII.GetBytes(sendmsg));
            //接受
            try
            {
                sock2.Receive(brecmsg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            recvmsg = Encoding.ASCII.GetString(brecmsg);            
            //解析recvmsg内容，取出acceptor的ip和port
            sarray = recvmsg.Split('|');    //此处可考虑在PosString类中添加方法具体实现
            SocketClass.serverIP = sarray[6].ToString().Split('$')[1] ;    //
            SocketClass.port = Convert.ToInt16(sarray[6].ToString().Split('$')[2]);

            sock2.Close();
            return 0;
        }
        
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
            string msgbody = "";
            int ifrecon = 0;
            if (ifrecon == 0)
            {
                msgbody = PosConfig.xszbm + SEP2 + PosConfig.zdh + SEP2 + 0 + SEP2;
            }
            else if (ifrecon == 1)
            {
                msgbody = PosConfig.xszbm + SEP2 + PosConfig.zdh + SEP2 + 1 + SEP2 + null;
            }
            
            IniMsgStr("1", REGISTER, msgbody, ref sendmsg);

            sock.Sendmsg(sendmsg);

            recvmsg = sock.Recvmsg();
            Console.WriteLine("REGISTER recvmsg:\t" + recvmsg);
            sarray = recvmsg.Split('|');
            appid = sarray[6].ToString().Split('$')[1];    //
            pinkey =sarray[6].ToString().Split('$')[2];
            mackey = sarray[6].ToString().Split('$')[3];
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

        /// <summary>
        /// 链路心跳
        /// </summary>
        /// <param name="sock"></param>
        public static void ACTIVETEST(SocketClass sock)
        {

        }

        /// <summary>
        /// 与acceptor的链接模块
        /// </summary>
        /// <param name="sock"></param>
        public void Con_Acceptor(SocketClass sock)
        {
            Register(sock);
            OnLineGetPra(sock);
        }
        
        const string SEP1 = "|";
        

        /// <summary>
        /// 生成数据包(所有)
        /// </summary>
        /// <param name="s_r"> 包类型：1为请求包，0为应答包；</param>
        /// <param name="opcode"> 操作码：包请求操作动作 </param>
        /// <param name="istr"> 包体数据域 </param>
        /// <param name="ostr"> 传出的完整数据包</param>
        public void IniMsgStr(string s_r, string opcode, string istr, ref string ostr)
        {
            string shead;
            string str;
            Random r = new Random();        //  操作序列号：随机数
            string msgbody = istr;
            string msgmac = "";

            shead = s_r + SEP1 + 1 + SEP1 + 0 + SEP1 + r.Next(9999).ToString().PadLeft(4, '0') + SEP1 + opcode + SEP1;
            str = shead + istr;

            if (opcode != "SERVERCONNECT" && opcode != "REGISTER" && opcode != "ACTIVETEST")
            {
                msgbody = Encode(istr, pinkey);
                msgmac = CalMac(str, mackey);
            }
            str = string.Empty;
            str = shead + msgbody + SEP1 + msgmac;

            ostr = "@" + str.Length.ToString().PadLeft(4, '0') + SEP1 + str;
            


        }


        public string Encode(string istr, string key)
        {
            StringBuilder result = new StringBuilder(4096);
            BzSec.encode(istr, result, key);
            return result.ToString();
        }

        public string Decode(string istr, string key)
        {
            StringBuilder result = new StringBuilder(4096);
            BzSec.decode(istr, result, key);
            return result.ToString();
        }
        
        //
        public string CalMac(string istr, string key)
        {
            StringBuilder result = new StringBuilder(4096);
            BzSec.calmac(istr, result, key);
            return result.ToString();
        }

        public string CheckMac(string istr,string msgmac, string key)
        {
            StringBuilder result = new StringBuilder(4096);
            BzSec.checkmac(istr, msgmac, key);
            Console.WriteLine(result);
            return result.ToString();
        }


    }
}
