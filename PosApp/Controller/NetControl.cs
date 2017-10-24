using LotPos.Controller.NetControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LotPos.Controller
{
    public class NetControl : AbsNetControl
    {
        
        public NetControl() { }

        public NetControl(string dataType, string dataBody)
        {
            
        }

        public string IniSocketSendPacket()
        {
            string result = string.Empty;

            packetDict[DATATYPE] = dataType;
            packetDict[DATAORD] = dataOrd;
            packetDict[DATAOVERSIGN] = dataOverSign;
            packetDict[HANDLEORD] = handleOrd;
            packetDict[DATABODY] = dataBody;
            packetDict[MAC] = mac;
            packetDict[NUMSIGN] = numSign;

            int length = 0;
            //shead = dataType + "|" + dataOrd + "|" + dataOverSign + "|" + handleOrd + "|" + opcode + "|";
            foreach (var item in packetDict)
            {
                string value = string.Empty;
                value += packetDict.Values;
                length = value.Length;
                result += item.Value + "|";
            }
            result = length.ToString() + result;

            return result;

        }
    }

}
