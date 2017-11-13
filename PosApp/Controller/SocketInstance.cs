using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LotPos.Model;


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
        //private byte[] readbuff = new byte[5120];
        private List<byte> cache = new List<byte>();
        private List<MessageBuffer> bufferList = new List<MessageBuffer>();

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
                socket.BeginReceive(msgbuff, 0, msgbuff.Length, SocketFlags.None, read, null);
                Thread td = new Thread(AcceptInfo);

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

        void AcceptInfo()
        {
            while (true)
            {
                socket.BeginReceive(msgbuff, 0, msgbuff.Length, SocketFlags.None, read, null);
            }
        }

        public string Receive()
        {
            try
            {
                string srecmsg = "";
                //socket.Receive(msglen, 5, 0);                
                //msglen = new byte[Convert.ToInt32(Encoding.ASCII.GetString(msglen).Trim('@'))];
                socket.Receive(msgbuff);//, Convert.ToInt32(msglen), 0);                 
                srecmsg += Encoding.ASCII.GetString(msgbuff);
                return srecmsg;
            }
            catch (Exception ex)
            { 
                throw;
            }
        }

        public void read(IAsyncResult iar)
        {
            try
            {
                //获取消息长度，结束消息接收
                int length = socket.EndReceive(iar);
                byte[] message = new byte[length];
                Buffer.BlockCopy(msgbuff, 0, message, 0, length);
                cache.AddRange(message);
                //if (!isReading)
                //{
                //    isReading = true;
                //    onData();
                //}
                //开启异步消息接收 消息到达后会直接写入缓冲区
                //尾递归 无限开启与结束 形成socket通信循环
                socket.BeginReceive(msgbuff, 0, msgbuff.Length, SocketFlags.None, read, null);
            }
            catch (Exception e)
            {
                socket.Close();
                throw;
            }      
        }

        private void onData()
        {
            //byte[] result = encode(cache);
            //if (result == null)
            //{
            //    isReading = false;
            //    return;
            //}
            //SocketModel message = mdecode(result);
            //if (message == null)
            //{
            //    isReading = false;
            //    return;
            //}
            ////消息处理
            //messages.Add(message);
            ////尾递归，防止在消息处理过程中有其他消息到达而没有经过处理
            //onData();
        }

        public byte[] encode(List<byte> cache)
        {
            byte[] result = cache.ToArray();
            return result;
        }
    }
}
