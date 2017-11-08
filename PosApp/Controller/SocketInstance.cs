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

        private string ip = "10.1.1.170";

        private int port = 5555;

        byte[] msglen = new byte[10];

        byte[] msgbuff = new byte[5120];

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

        public SocketInstance()
        {
            
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ip, port);
                socket.BeginReceive(msgbuff, 0, msgbuff.Length, SocketFlags.None, new AsyncCallback(read), this);
            }
            catch (Exception exc)
            {

                throw;
            }
        }

        
        public void write(string str)
        {
            //NetControl netcontrol = new NetControl();
            //string sendstr = netcontrol.IniSocketSendPacket();
            //string sendstr = netcontrol.IniSocketSendPacket();
            try
            {
                socket.Send(Encoding.ASCII.GetBytes(str));
            }
            catch (Exception ex)
            {
                //(ex.Message + "Sendmsg");
                
                return ;
            }
        }


        public void recvive(ref string result)
        {
            try
            {

                string srecmsg = "";

                socket.BeginReceive(msgbuff, 0, msgbuff.Length, SocketFlags.None, new AsyncCallback(read), this);

                //socket.Receive(msglen, 5, 0);

                byte[] msgbyte = new byte[Convert.ToInt32(Encoding.ASCII.GetString(msglen).Trim('@'))];

                socket.Receive(msgbyte);

                srecmsg += Encoding.ASCII.GetString(msgbyte);

                result = srecmsg;

            }
            catch (Exception ex)
            { 
                return  ;
            }
        }

        public void read(IAsyncResult ar)
        { 
            socket.EndReceive(ar);
            ar.AsyncWaitHandle.Close();
            //byte[] msgbyte = new byte[Convert.ToInt32(Encoding.ASCII.GetString(msgbuff))];
            Console.WriteLine("收到消息：{0}", Encoding.ASCII.GetString(msgbuff));

            //清空数据，重新开始异步接收
            msgbuff = new byte[msgbuff.Length];
            socket.BeginReceive(msgbuff, 0, msgbuff.Length, SocketFlags.None, new AsyncCallback(read), this);
        }


    }
}
