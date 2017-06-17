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


namespace LotPos
{
    public partial class MainForm : Form
    {
        /*
         * 定义类
         */
        public LogOnForm logonform;     //登录界面类
        public PosBack posback;     //后台业务处理类
        PosConfig posconfig;        //配置文件类      
        /*
         *界面参数配置 
         */
        public static short _pageNum;        //界面标记 0=加载 1=主界面 2=投注界面
        string _loginPattern;       //启动模式，由配置文件获取

        SocketClass betsock = new SocketClass();

        static int sequence = 0;
        string sk = "111111";
        string sendbetstr = "";
        byte[] btmsg = new byte[1024];
        
        string smsg2 = "";
        string ServerIP = "10.1.1.192";
        //int selport = 9902;
        int betport = 9901;
        static int _lsh = 1;
        Timer dtime;
        
        public MainForm()
        {
            InitializeComponent();
            dtime = new Timer();
            dtime.Interval = 1000;
            dtime.Tick += new EventHandler(dtime_tick);
            posconfig = new PosConfig();
            posback = new PosBack();
            logonform = new LogOnForm();
            _pageNum = 1;
            _loginPattern = posconfig.LoginPattern;

        }

        //处理登录界面返回结果
        int LogonFormResult(short pageNum)
        {
            //确认登录
            if (logonform.DialogResult == DialogResult.OK)
            {                
                try
                {
                    //  TODO:   此处建立acceptor连接
                    //          调用取参

                    //
                    logonform.Close();
                    return 0;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                    return 100;
                }
            }
            //退出 or 取消登录
            else if(logonform.DialogResult == DialogResult.Cancel)
            {
                //程序启动时将直接退出，而不是返回主界面
                if (_pageNum == 0)
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
                    return -1;
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
                return 0;
            }
            return -2;
        }

        //加载主界面
        private void Form1_Load(object sender, EventArgs e)
        {
            if (_pageNum == 0)
            {
                logonform.ShowDialog();     //加载登录界面
                if ( LogonFormResult(_pageNum) == 0) //登录结果
                {
                    //posback.GetPra();
                }      
            }
            _pageNum = 1;
            label_LoginPattern.Text = posconfig.LoginPattern;   //显示启动模式
            dtime.Start();      //开启显示时间
            posback.GetPra();       //模拟取参
            Update_panel_Parameters_Show();
            tabControl1.Visible = true;
            panel_Parameters.Visible = true;

            toolTip1.SetToolTip(tableLayoutPanel_SomePra, posconfig.ServerIP + "\r\n" + posconfig.Port);
            toolTip1.SetToolTip(Btn_Logonoff, posconfig.ServerIP + "\r\n" + posconfig.Port);
            toolTip1.SetToolTip(label_Date, posconfig.ServerIP + "\r\n" + posconfig.Port);


        }

        void Update_panel_Parameters_Show()
        {
            
            GameName.Text = posback.gamename;
            DrawNo.Text = posback.drawno;
            AgentId.Text = posback.xszbm;
            Lsh.Text = posback.lsh;
            SmallCount.Text = posback.smallcount;
            Balance.Text = posback.balance;
            TQTime.Text = posback.tqtime;

        }

        //显示时间
        void dtime_tick(object sender, EventArgs e)
        {
            //显示时间
            label_Date.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss dddd}", DateTime.Now);
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

        
        //点击标签切换玩法
        private void Page_C515_Click(object sender, EventArgs e)
        {

        }

        //投注 F8
        private void Num_F8_Click(object sender, EventArgs e)
        {
            Betqueren();
        }

        //登录\注销
        private void Btn_Logonoff_Click(object sender, EventArgs e)
        {
            logonform = new LogOnForm();
            logonform.ShowDialog();            
            LogonFormResult(_pageNum);
        }

        //投注确认 显示票面
        void Betqueren()
        {
            if (true)
            {
                groupBox_LotteryPicture.BackColor = Color.FromArgb(255, 192, 192);
            }
        }
        

        //select 
        private void button1_Click(object sender, EventArgs e)
        {
            issuequery_msg();
        }

        //showbet button 生成投注号码 、 检查投注号码 、 生成发送串 
        private void btn_test_Click(object sender, EventArgs e)
        {
            PosFile pf = new PosFile(); 
            textBox_test.Text += pf._filename;
            //string sball = redballstring.Text;
            //sendbetstr = ini_betstr(sball);
        }

        //新期查询 暂未启用 lias投注版本的
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
                textBox_test.Text += "\r\n[ERR]:" + "投注号码个数有误";
                return "";
            }
            else if (-3 == betnum.cfof_check_ball(sball, ref playtype, ref money))
            {
                textBox_test.Text += "\r\n[ERR]:" + "投注号码有重复";
                return "";
            }
            else
            {
                textBox_test.Text += "\r\n[ERR]:" + betnum.cfof_check_ball(sball, ref playtype, ref money);
                return "";
            }
        }
        #endregion

        //投注 暂未启用 lias投注版本的
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

                textBox_test.Text += "\r\nreceive message is:" + srecmsg + "\r\n";
                string[] sArray = srecmsg.Split('|');

                string srembody = sArray[7];
                BzWebSec.WebDecodeString(srembody, sk, s2);
                string srebody = s2.ToString();

                textBox_test.Text += "\r\n" + srebody;
            }
            else
            {
                textBox_test.Text += " " + "\r\n" + SocketClass.errstring;
            }

        }

        //bet button 弃用 lias投注版本的
        private void button3_Click(object sender, EventArgs e)
        {
            string sball = textBox13.Text;
            sendbetstr = ini_betstr(sball);
            bet_msg(sendbetstr);
        }

        //关闭窗口事件
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_pageNum == 0)  //完整版应该为 (_pageNum != 0 ) 调试版本省略确认框 == 0
            {
                if (MessageBox.Show("确认退出程序？", "程序退出确认", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

            }
            try
            {                
                betsock.closesock();
            }
            catch
            {
            }
        }

        //信息输出栏定位末行
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Focus();//获取焦点
            textBox3.Select(textBox3.TextLength, 0);//光标定位到文本最后
            textBox3.ScrollToCaret();
        }

        //信息输出栏定位末行
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
            textBox_test.Focus();//获取焦点
            textBox_test.Select(textBox_test.TextLength, 0);//光标定位到文本最后
            textBox_test.ScrollToCaret();
            
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
        }

        
    }
}
