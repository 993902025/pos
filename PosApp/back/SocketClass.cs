using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace LotPos
{
    public class SocketClass
    {
        public static Action<string > sendmessage;
        public static Socket sct;
        public string serverIP;
        public int port;
        public static string errstring;
        byte[] result = new byte[1024];   



        
        public int Inisocket(string ServerIP, int port)
        {
            //IPAddress ip = IPAddress.Parse(ServerIP);
            try
            {
                sct = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sct.Connect(serverIP, port);
                return 0;
            }
            catch (Exception ex)
            {               
                MessageBox.Show(ex.Message);
                return -4; 
            }
        }

        public int Sendmsg(string smsg2)
        { 
            try
            {
                sct.Send(Encoding.ASCII.GetBytes(smsg2));
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public string Recvmsg()
        { 
            try
            {
                byte[] brecmsg = new byte[1024];
                string srecmsg = "";
               
                sct.Receive(brecmsg);
                srecmsg = Encoding.ASCII.GetString(brecmsg);
                return srecmsg;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "-1";
            }
        }

        public void Closesock()
        {
            try
            {
                sct.Shutdown(SocketShutdown.Both);
            }
            catch
            {
                
            }
        }

    }
}
