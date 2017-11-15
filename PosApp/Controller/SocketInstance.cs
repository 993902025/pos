using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LotPos.Model;
using System.ComponentModel;

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

        private object lockObj = "";
        private object lockSocket = "";

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
                

            }
            catch (Exception exc)
            {

                throw;
            }
        }

        public bool StartSocketClient()
        {
            try
            {
                //conn();

                BackgroundWorker receiveMessage = new BackgroundWorker();
                receiveMessage.DoWork += new DoWorkEventHandler(ReceiveMessage_DoWork);
                receiveMessage.RunWorkerAsync();

                return true;
            }
            catch (Exception e)
            {
                //SaveErrorLog.Save(typeof(SocketClient), e);
                return false;
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

        #region 接收数据
        private void ReceiveMessage_DoWork(object obj, DoWorkEventArgs args)
        {
            while (true)
            {
                try
                {
                    lock (lockSocket)
                    {
                        if (socket != null)
                        {

                            int buflen = socket.Available;
                            if (buflen > 6)
                            {
                                byte[] data = new byte[6];
                                socket.Receive(data, 6, SocketFlags.None);
                                string head = Encoding.UTF8.GetString(data);
                                if (head.StartsWith("@"))
                                {
                                    buflen = Convert.ToInt32(head.Substring(1, 4));
                                }
                                else
                                {
                                    buflen = buflen - 6;
                                }

                                data = new byte[buflen];
                                int receiveLength = 0;
                                while (receiveLength < buflen)
                                {
                                    if (socket.Available < 0)
                                    {
                                        Thread.Sleep(10);
                                        continue;
                                    }
                                    receiveLength += socket.Receive(data, receiveLength, buflen - receiveLength, SocketFlags.None);
                                }
                                string body = Encoding.UTF8.GetString(data);

                                //解析BODY
                                MessageBuffer buffer = GetMessageBuffer(body);
                                if (buffer != null)
                                {
                                    lock (lockObj)
                                    {
                                        bufferList.Add(buffer);
                                    }
                                }
                                else
                                {
                                    //解析数据失败
                                }
                            }
                            else
                            {
                                continue;
                            }

                        }
                        else
                        {
                            Thread.Sleep(10);
                        }
                    }

                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }

        #endregion

        #region 解析body
        private MessageBuffer GetMessageBuffer(string body)
        {
            string[] bodys = body.Split('|');
            if (bodys.Length > 3)
            {
                MessageBuffer buffer = new MessageBuffer()
                {
                    dataType = bodys[1],
                    dataBody = bodys[3]
                };

                return buffer;
            }

            return null;
        }
        #endregion

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
