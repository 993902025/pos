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
        private string dataLength, dataType, dataOrd, dataOverSign, handleOrd;

        private string dataBody;

        private string mac;

        private string numSign;

        Socket serverSocket;

        public NetControl() { }

        public NetControl(string dataType, string dataBody )
        {
            this.dataType = dataType;
            this.dataBody = dataBody;
        }

        public void IniNet()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string ip, int port)
        {
            serverSocket.Connect(ip, port);
        }


    }
}
