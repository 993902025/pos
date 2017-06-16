using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;


namespace WindowsFormsApp2
{
    public partial class MainForm : Form
    {
        public LogOnForm logonform;
        int _pageNum;        //界面标记 1=主界面 2=投注界面

        SocketClass betsock = new SocketClass();

        static int sequence = 0;
        string sk = "111111";
        string sendbetstr = "";
        byte[] btmsg = new byte[1024];
        
        string smsg2 = "";
        string ServerIP = "10.1.1.192";
        int selport = 9902;
        int betport = 9901;
        static int _lsh = 1;
        
        public MainForm()
        {
            InitializeComponent();


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            logonform = new LogOnForm();
            logonform.ShowDialog();
            if (logonform.DialogResult == DialogResult.OK)
            {
                //Application.Run(new MainForm());
                try
                {

                    logonform.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                    throw;
                }
            }
            else
            {
                try
                {

                    this.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                    throw;
                }

                try
                {

                    logonform.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                    throw;
                }

                return;
            }
            //_pageNum = 1;
            //ShowPage_1(_pageNum);
        }

        void ShowPage_1(int pagenum)
        {
            panel_Parameters.Show();
            panel_Bet.Show();
            panel_keyboard.Show();
        }

        void PanelParameters()
        {

        }
        string _agentpra, _pra;
        int GetPra(string agentpra, ref string pra)
        {
            int re_num = 0;
            return re_num;
        }



        //select 
        private void button1_Click(object sender, EventArgs e)
        {
            issuequery_msg();
        }

        //showbet button 生成投注号码 、 检查投注号码 、 生成发送串 
        private void button2_Click(object sender, EventArgs e)
        {
            //string sball = redballstring.Text;
            //sendbetstr = ini_betstr(sball);
        }

        //新期查询
        private void issuequery_msg()
        {
            //SocketClass selsock = new SocketClass();

            //string shead = "1|1|0|003170|ISSUEQUERY|" + agentidbox.Text + "|";
            //string sbody = agentidbox.Text + "$" + gamenamebox.Text + "$" + "2017020$"; 
            //string s1 = shead + sbody + sk;
            //StringBuilder s2 = new StringBuilder(512);

            ////BzWebSec bzsec = new BzWebSec();//不需要new 因为bzwebsec中这些方法是static
            //BzWebSec.WebMD5String32(s1, s2);
            //string szy = s2.ToString();
            //int stp = -1;
            //stp = BzWebSec.WebEncodeString(sbody, sk, s2);
            //string msbody = s2.ToString();
            //int imsglen = shead.Length + szy.Length + msbody.Length + 1;
            //string msglen = "@" + (imsglen.ToString()).PadLeft(4, '0') + "|";
            //smsg2 = msglen + shead + msbody + "|" + szy;
            ////建socket链接
            //if ( 0 == selsock.inisocket(ServerIP, selport))
            //{
            //    //发送send
            //    selsock.sendmsg(smsg2);
            //    textBox3.Text += "send message is:" + smsg2 + "\r\n";
            //    //接受recv
            //    string srecmsg = selsock.recvmsg();
            //    textBox4.Text += "\r\nreceive message is:" + srecmsg + "\r\n";
            //    string[] sArray = srecmsg.Split('|');
            //    string srembody = sArray[7];
            //    BzWebSec.WebDecodeString(srembody, sk, s2);
            //    string srebody = s2.ToString();
            //    textBox4.Text += "\r\n" + srebody;
            //    selsock.closesock();
            //}
            //else
            //{
            //    textBox4.Text += " " + "\r\n" + SocketClass.errstring;
            //}
        }

        #region 检查选号
        private int check_ball(string sredball, string sblueball, ref string str1, ref string str2)
        {
            string sred = sredball;
            string sblu = sblueball;
            int rballen = sred.Length;
            int blulen = sblu.Length;
            if (rballen % 2 != 0)
            {                
                return -1; 
            }
            if (blulen % 2 != 0)
            {
                return -2;
            }
            string sball = sred + sblu;
            int[] ckball = new int[rballen + blulen];
            for (int i = 0; i < rballen; i++)
            {
                ckball[i] = Convert.ToInt16(sball.Substring(i, 2));
                if (i > 0)
                {
                    if (ckball[i] == ckball[i - 1])
                    {
                        return -3;//投注号码有重复
                    }
                }
            }

            str1 = "";
            str2 = "";
            return 0;
        }
        #endregion

        #region 初始化投注串
        private string ini_betstr(string sball)
        {

            //330106$$$33010620170525145853000001$1$0$$ $ $ $ $01$ $ 
            #region 注释部分,串示例说明
            /*
             *  agentid         330106
             *  gamename        ql515
             *  drawno          2017020
             *  ticket          33010620170525145853000001
             *  playtype 1
             *  money 2
             *  betdetail 
             *  name
             *  phonenumber
             *  idnumber
             *  cardnumber
             *  reserv1
             *  reserv2
             *  reserv3
             */
            #endregion
            DateTime.Now.ToShortTimeString();
            DateTime dt = DateTime.Now;

            string agentid = AgentId.Text;           //渠道编号
            string gamename = GameName.Text;         //玩法编号
            string drawno = DrawNo.Text;             //期号
            string ticket = AgentId.Text + string.Format("{0:yyyyMMddHHmmss}",dt) + (_lsh.ToString()).PadLeft(6, '0');         //票ID

            BetNum betnum = new BetNum();
            string playtype = "";                       //投注方式
            int money = 0;                              //金额
            //int ret = betnum.cfof_check_ball(sball, ref playtype, ref money);       
            //检查投注号码合法 + 计算投注方式和金额        
            if (0 == betnum.cfof_check_ball(sball, ref playtype, ref money))
            {
                string betdetail = ((Multiple.Text == string.Empty) ? "1" : Multiple.Text).PadLeft(3, '0') + (sball.Length / 2).ToString().PadLeft(2, '0') + sball;            //号码串   （倍数+号码个数+号码）
                money = money * Convert.ToInt16((Multiple.Text == string.Empty) ? "1" : Multiple.Text);
                string smoney = string.Format("{0:f2}", money);
                string betmsgbody = agentid + "$" + gamename + "$" + drawno + "$" + ticket + "$" + playtype + "$" + smoney + "$" + betdetail + "$" + "$" + "$" + "$" + "$" + "01" + "$" + "$";
                textBox3.Text += "\r\nbetbody:" + betmsgbody;
                return betmsgbody;
            }
            else if (-1 == betnum.cfof_check_ball(sball, ref playtype, ref money))
            {
                textBox4.Text += "\r\n[ERR]:" + "投注号码个数有误";
                return "";
            }
            else if (-3 == betnum.cfof_check_ball(sball, ref playtype, ref money))
            {
                textBox4.Text += "\r\n[ERR]:" + "投注号码有重复";
                return "";
            }
            else
            {
                textBox4.Text += "\r\n[ERR]:" + betnum.cfof_check_ball(sball, ref playtype, ref money);
                return "";
            }
        }
        #endregion

        //投注
        private void bet_msg(string betbody)
        {
            if (betbody.Length == 0 || betbody == "")
            {

            }
            sequence++;
            string sequenceid = sequence.ToString().PadLeft(6, '0');
            string shead = "1|1|0|" + sequenceid + "|BET|" + AgentId.Text + "|";
            string sbody = betbody;
            string s1 = shead + sbody + sk;
            StringBuilder s2 = new StringBuilder(5120);

            //BzWebSec bzsec = new BzWebSec();//不需要new 因为bzwebsec中这些方法是static
            BzWebSec.WebMD5String32(s1, s2);
            string szy = s2.ToString();
            int stp = -1;
            stp = BzWebSec.WebEncodeString(sbody, sk, s2);
            string msbody = s2.ToString();
            int imsglen = shead.Length + szy.Length + msbody.Length + 1;
            string msglen = "@" + (imsglen.ToString()).PadLeft(4, '0') + "|";
            smsg2 = msglen + shead + msbody + "|" + szy;
            //建socket链接
            if (0 == betsock.inisocket(ServerIP, betport))
            {
                //发送send
                betsock.sendmsg(smsg2);
                textBox3.Text += "send message is:" + smsg2 + "\r\n";
                //接受recv
                string srecmsg = betsock.recvmsg();

                textBox4.Text += "\r\nreceive message is:" + srecmsg + "\r\n";
                string[] sArray = srecmsg.Split('|');

                string srembody = sArray[7];
                BzWebSec.WebDecodeString(srembody, sk, s2);
                string srebody = s2.ToString();

                textBox4.Text += "\r\n" + srebody;
            }
            else
            {
                textBox4.Text += " " + "\r\n" + SocketClass.errstring;
            }

        }

        //bet button
        private void button3_Click(object sender, EventArgs e)
        {
            string sball = textBox13.Text;
            sendbetstr = ini_betstr(sball);
            bet_msg(sendbetstr);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                
                betsock.closesock();
            }
            catch
            {
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Focus();//获取焦点
            textBox3.Select(textBox3.TextLength, 0);//光标定位到文本最后
            textBox3.ScrollToCaret();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
            textBox4.Focus();//获取焦点
            textBox4.Select(textBox4.TextLength, 0);//光标定位到文本最后
            textBox4.ScrollToCaret();
            
        }

        private void Page_C515_Click(object sender, EventArgs e)
        {
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
