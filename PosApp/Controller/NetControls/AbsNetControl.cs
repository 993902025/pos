using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotPos.Controller.NetControls
{
    public abstract class AbsNetControl
    {

        public List<string> packet;

        SocketInstance socketserver;

        public AbsNetControl()
        {
            socketserver = new SocketInstance();
        }

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

        //@包长度|包类型|包序号|包结束标志|操作序列号|操作码|数据域1……$数据域n|MAC字段

        public string Communications(string dataType, string dataBody)
        {
            try
            {
                if (socketserver == null)
                {
                    return "-0";
                }

                bool send = socketserver.send(dataType, dataBody);
            }
            catch (Exception)
            {

                throw;
            }

            return "";
        }


        virtual public bool send(string dataType)
        {

            //socketserver.write();

            return true;
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
