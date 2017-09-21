using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LotPos.Controller
{
    public class SocketInstance
    {

        public static SocketInstance instance;

        private Socket socket;

        private string ip;

        private int port;
        
        public static SocketInstance Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SocketInstance();
                }
                return instance;
            }
        }

        private SocketInstance()
        {
            
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception exc)
            {

                throw;
            }
        }


        public void write(string str)
        {
            NetControl netcontrol = new NetControl();
            string sendstr = netcontrol.IniSocketSendPacket();
            try
            {
                socket.Send(Encoding.ASCII.GetBytes(sendstr));
            }
            catch (Exception ex)
            {
                //(ex.Message + "Sendmsg");
                
                return ;
            }
        }


        public void read(ref string result)
        {
            try
            {

                byte[] msglen = new byte[10];

                byte[] msgbyte;

                string srecmsg = "";

                socket.Receive(msglen, 5, 0);
                
                msgbyte = new byte[Convert.ToInt32(Encoding.ASCII.GetString(msglen).Trim('@'))];

                socket.Receive(msgbyte);

                srecmsg += Encoding.ASCII.GetString(msgbyte);

                result = srecmsg;

            }
            catch (Exception ex)
            { 
                return  ;
            }
        }


    }
}
