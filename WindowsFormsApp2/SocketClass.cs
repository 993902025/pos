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


namespace WindowsFormsApp2
{
    public class SocketClass
    {
        public static Action<string > sendmessage;
        Socket sct;
        public static string errstring;
        byte[] result = new byte[1024];   
        
        public int inisocket(string ServerIP, int port)
        {
            sct = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(ServerIP);

            try
            {
                sct.Connect(ip, port);
                return 0;
            }
            catch (Exception ex)
            {
                errstring = ex.Message;
                MessageBox.Show(ex.Message);
                return -4; 
            }
        }

        public int sendmsg(string smsg2)
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
        public string recvmsg()
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

        public void closesock()
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
