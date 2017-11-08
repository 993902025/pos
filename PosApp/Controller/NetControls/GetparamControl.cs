using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotPos.Controller.NetControls
{
    class GetparamControl : AbsNetControl
    {
        public GetparamControl()
        {
            IniSocketSendPacket();
        }

        override public void IniSocketSendPacket()
        {
            packetDict[DATATYPE] = "1";
            packetDict[DATAORD] = "1";
            packetDict[DATAOVERSIGN] = "0";
            packetDict[HANDLEORD] = "123";
            packetDict[HANDCODE] = "GETPARAM";
            packetDict[DATABODY] = "";
            packetDict[MAC] = "abcdef0123456789";
            packetDict[NUMSIGN] = "";
        }

        override public void send()
        {
            string str = packetDict[DATATYPE] + "|"
            + packetDict[DATAORD] + "|"
            + packetDict[DATAOVERSIGN] + "|"
            + packetDict[HANDLEORD] + "|"
            + packetDict[HANDCODE] + "|"
            + packetDict[DATABODY] + "|"
            + packetDict[MAC];

            dataLen = "@" + str.Length.ToString().PadLeft(4, '0');
            str = dataLen + "|" + str;

            SocketInstance socket = new SocketInstance();

            socket.write(str);
        }

        public string GetBody()
        {
            string result = "";
            result = "33010002" + "$" + "10002" + "$" + "0" + "$";
            return result;
        }
    }
}
