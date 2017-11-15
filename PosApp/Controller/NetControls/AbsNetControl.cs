using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotPos.Controller.NetControls
{
    public abstract class AbsNetControl
    {
        /// <summary>
        /// 通讯包长度 四位数字,单个数据包的长度不大于5120字节
        /// </summary>
        public const int DATALEN = 10;

        /// <summary>
        /// 通讯包类型:1为请求包，0为应答包
        /// </summary>
        public const int DATATYPE = 11;

        /// <summary>
        /// 通讯包序号:拆包情况下的序号 1,2,3,4……
        /// </summary>
        public const int DATAORD = 12;

        /// <summary>
        /// 包结束标志：是否有下一个拆分包，0无，1有
        /// </summary>
        public const int DATAOVERSIGN = 13;

        /// <summary>
        /// 操作序列号：随机数
        /// </summary>
        public const int HANDLEORD = 14;

        /// <summary>
        /// 操作码：包请求操作动作(如签到、取参等)
        /// </summary>
        public const int HANDCODE = 15;

        /// <summary>
        /// 包体：由若干数据域组成,域间采用 " $  " 分割
        /// </summary>
        public const int DATABODY = 16;

        /// <summary>
        /// MAC字段:是16字节的16进制的字符串,需要加密包才有
        /// </summary>
        public const int MAC = 17;

        /// <summary>
        /// 数字签名：需要加密的包才有
        /// </summary>
        public const int NUMSIGN = 18;

        /// <summary>
        /// 通讯包长度 四位数字,单个数据包的长度不大于5120字节
        /// </summary>
        public string dataLen = "";

        /// <summary>
        /// 通讯包类型:1为请求包，0为应答包
        /// </summary>
        public static string dataType;

        /// <summary>
        /// 通讯包序号:拆包情况下的序号 1,2,3,4……
        /// </summary>
        public static string dataOrd;

        /// <summary>
        /// 包结束标志：是否有下一个拆分包，0无，1有
        /// </summary>
        public static string dataOverSign;

        /// <summary>
        /// 操作序列号：随机数
        /// </summary>
        public static string handleOrd;

        /// <summary>
        /// 操作码：包请求操作动作(如签到、取参等)
        /// </summary>
        public static string handCode;

        /// <summary>
        /// 包体：由若干数据域组成,域间采用 " $  " 分割
        /// </summary>
        public static string dataBody;

        /// <summary>
        /// MAC字段:是16字节的16进制的字符串,需要加密包才有
        /// </summary>
        public static string mac;

        /// <summary>
        /// 数字签名：,需要加密包才有
        /// </summary>
        public static string numSign;


        public List<string> packet;

        
        SocketInstance socket = new SocketInstance();

        public Dictionary<int, string> packetDict = new Dictionary<int, string>();
        /*
         * public  Dictionary<int, string> packetDict = new Dictionary<int, string>()
        {
            //{DATALEN,   },
            {DATATYPE,dataType},
            {DATAORD, dataOrd},
            {DATAOVERSIGN, dataOverSign},
            {HANDLEORD, handleOrd},
            {HANDCODE, handCode},
            {DATABODY, dataBody},
            {MAC, mac},
            {NUMSIGN, numSign}
        };
        */

        
        public abstract void IniSocketSendPacket();

        virtual public void send()
        {
            string str = packetDict[DATATYPE] + "|"
            + packetDict[DATAORD] + "|"
            + packetDict[DATAOVERSIGN] + "|"
            + packetDict[HANDLEORD] + "|"
            + packetDict[HANDCODE] + "|"
            + packetDict[DATABODY] + "|"
            + packetDict[MAC] + "|"
            + packetDict[NUMSIGN];

            dataLen = "@" + str.Length.ToString().PadLeft(4, '0');
            str = dataLen + "|" + str;

           

            socket.write(str);
        }

        virtual public void recv()
        {
            string str = "x";
            //socket.Receive(ref str);
            Console.WriteLine(str);
        }

        public void Encode()
        {
           
        }

    }
}
