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
        public static Socket sct;
        public static string serverIP;
        public static int port;
        public static string errstring;
        byte[] result = new byte[1024];
        public static bool sockSwitch;


        public SocketClass()
        {
            try
            {
                sct = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "SocketClass");
            }
        }

        
        public int Inisocket()//string ip, int port)
        {
            //IPAddress ip = IPAddress.Parse(ip);
            try
            {
                sct.Connect(serverIP, port);
                sockSwitch = true;
                return 0;
            }
            catch (Exception ex)
            {               
                MessageBox.Show(ex.Message + "Inisocket");
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
                MessageBox.Show(ex.Message + "Sendmsg");
                return -1;
            }
        }

        public string Recvmsg()
        { 
            try
            {
                byte[] msglen = new byte[10];
                byte[] msgbyte;
                string srecmsg = "";
                sct.Receive(msglen, 5, 0);
                int length = Convert.ToInt32(Encoding.ASCII.GetString(msglen).Trim('@'));
                msgbyte = new byte[length + 100];
                sct.Receive(msgbyte);
                srecmsg += Encoding.ASCII.GetString(msgbyte);
                
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
                sockSwitch = false;
            }
            catch
            {
                
            }
        }

    }
}
