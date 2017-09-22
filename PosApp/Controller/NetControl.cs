using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LotPos.Controller
{
    public class NetControl
    {
        private string dataLength;

        private string dataType;
        private string dataOrd;
        private string dataOverSign, handleOrd;

        private string dataBody;

        private string mac;

        private string numSign;

        private List<string> packet;

        private Dictionary<string, string> packetDict = new Dictionary<string, string>()
        {
            //{"dataLength","" },
            {"dataType","" },
            {"dataOrd","" },
            {"dataOverSign","" },
            {"handleOrd","" },
            {"dataBody","" },
            {"mac","" },
            {"numSign","" }
        };

        public NetControl() { }

        public NetControl(string dataType, string dataBody)
        {
            this.dataType = dataType;
            this.dataBody = dataBody;

        }

        public string IniSocketSendPacket()
        {
            string result = string.Empty;
            packetDict["dataType"] = dataType;
            packetDict["dataOrd"] = dataOrd;
            packetDict["dataOverSign"] = dataOverSign;
            packetDict["handleOrd"] = handleOrd;
            packetDict["dataBody"] = dataBody;
            packetDict["mac"] = mac;
            packetDict["numSign"] = numSign;


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
