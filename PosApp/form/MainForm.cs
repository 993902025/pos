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
using System.Windows.Controls;
using LotPos.back;

namespace LotPos
{
    public partial class MainForm : Form
    {
        /* 定义类 
         */
        //public LogOnForm logonform;     //登录界面类
            //后台业务处理类
        PosConfig posconfig;        //配置文件类 
        SocketClass betsock = new SocketClass();

        /* 控件参数类
         */
        TextBox nownumbox = null;       //当期输入框控件

        /* 界面参数配置
        */
        public static int _pageNum;        //界面标记 0=加载 1=主界面 2=投注界面
        string _loginPattern;       //启动模式，由配置文件获取

        Timer dtime;


        static int sequence = 0;
        string sk = "111111";
        string sendbetstr = "";
        byte[] btmsg = new byte[1024];
        string smsg2 = "";
        string ServerIP = "10.1.1.192";
        //int selport = 9902;
        int betport = 9901;
        static int _lsh = 1;

        public MainForm()
        {
            InitializeComponent();
            dtime = new Timer()
            {
                Interval = 1000
            };
            dtime.Tick += new EventHandler(Dtime_tick);
            posconfig = new PosConfig();
            //posback = new PosBack();
            _pageNum = 1;
            _loginPattern = posconfig.LoginPattern;

        }

        
        /// <summary>
        /// 处理登录界面返回结果
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        void AtLogonForm(int pageNum)
        {
            LogOnForm logonform = new LogOnForm();
            logonform.ShowDialog();
            //确认登录
            if (logonform.DialogResult == DialogResult.OK)
            {
                //  TODO:   此处建立acceptor连接
                PosBack posback = new PosBack();
                posback.GetPra();       //模拟取参
                logonform.Close();              
            }
            //退出 or 取消登录
            else if (logonform.DialogResult == DialogResult.Cancel)
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
            }
        }

        //加载主界面
        private void Form1_Load(object sender, EventArgs e)
        {
            
            switch(_pageNum)
            { 

                case 0:
                    AtLogonForm(_pageNum);//加载登录界面
                    break;
                case 1:
                    break;
                default:
                    break;

            }
            
            _pageNum = 1;

            label_LoginPattern.Text = posconfig.LoginPattern;   //显示启动模式
            dtime.Start();      //开启显示时间

            tabControl1.Visible = true;     //标签控制页
            ShowPage_1(_pageNum);
            Update_panel_Parameters_Show(); //

            //初始化光标
            nownumbox = BetNo_A1;  
            nownumbox.Focus();      

            toolTip1.SetToolTip(tableLayoutPanel_SomePra, posconfig.ServerIP + "\r\n" + posconfig.Port);
            toolTip1.SetToolTip(Btn_Logonoff, posconfig.ServerIP + "\r\n" + posconfig.Port);
            toolTip1.SetToolTip(label_Date, posconfig.ServerIP + "\r\n" + posconfig.Port);


        }

        /// <summary>
        /// 站点玩法等参数显示
        /// </summary>
        void Update_panel_Parameters_Show()
        {
            PosBack posback = new PosBack();
            posback.GetPra();       //模拟取参
            GameName.Text = posback.gamename;
            DrawNo.Text = posback.drawno;
            AgentId.Text = posback.xszbm;
            Lsh.Text = posback.lsh;
            SmallCount.Text = posback.smallcount;
            Balance.Text = posback.balance;
            TQTime.Text = posback.tqtime;

            panel_Parameters.Visible = true;    //显示参数区域
        }
               
        /// <summary>
        /// 右上角信息栏时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Dtime_tick(object sender, EventArgs e)
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


        //登录\注销
        private void Btn_Logonoff_Click(object sender, EventArgs e)
        {
            AtLogonForm(_pageNum);
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
        private void Button1_Click(object sender, EventArgs e)
        {
            Issuequery_msg();
        }

        //showbet button 生成投注号码 、 检查投注号码 、 生成发送串 
        private void Btn_test_Click(object sender, EventArgs e)
        {
            //PosFile pf = new PosFile();
            //textBox_test.Text += pf._filename;
            //string sball = redballstring.Text;
            //sendbetstr = ini_betstr(sball);
            TestLog("" + string.Compare(BetNo_A1.Name, BetNo_A2.Name));
        }

        //新期查询 暂未启用 lias投注版本的
        private void Issuequery_msg()
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
        private int Check_ball(string sredball, string sblueball, ref string str1, ref string str2)
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
        private string Ini_betstr(string sball)
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
            string ticket = AgentId.Text + string.Format("{0:yyyyMMddHHmmss}", dt) + (_lsh.ToString()).PadLeft(6, '0');         //票ID

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
        private void Bet_msg(string betbody)
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
        private void Button3_Click(object sender, EventArgs e)
        {
            string sball = textBox13.Text;
            sendbetstr = Ini_betstr(sball);
            Bet_msg(sendbetstr);
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
        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.Focus();//获取焦点
            textBox3.Select(textBox3.TextLength, 0);//光标定位到文本最后
            textBox3.ScrollToCaret();
        }

        //信息输出栏定位末行
        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

            textBox_test.Focus();//获取焦点
            textBox_test.Select(textBox_test.TextLength, 0);//光标定位到文本最后
            textBox_test.ScrollToCaret();
            nownumbox.Focus();  //焦点返回到上一指定控件

        }

        private void ToolTip1_Popup(object sender, PopupEventArgs e)
        {
        }

        /*
         * 
         * 
         * 
         * */
        #region 号码输入框输入的处理块
            
        /// <summary>
        /// 满足(TextBox.Text.Length >= 2)，焦点移动到下一TabIndex索引的控件 (以后可重载移动条件)
        /// </summary>
        /// <param name="sender"></param>
        /// 参数
        void CheckTextFocus(object sender)
        {
            if ( ((TextBox)sender ).Text.Length >= 2 )
            {
                SelectNextControl((Control)this.ActiveControl, true, true, true, false);
                nownumbox = (TextBox)ActiveControl;
                //TestLog("nownumbox = " + nownumbox.Name +"\tfocus to " + ActiveControl.Name);
            }
            else if ( ((TextBox)sender).Text.Length <= 0 && nownumbox != BetNo_A1)
            {
                SelectNextControl((Control)this.ActiveControl, false, true, true, false);
                nownumbox = (TextBox)ActiveControl;
                //TestLog("focus to " + ActiveControl.Name);
            }
            
        }

        /// <summary>
        /// 所有 BetNo(BetNo_A1 - BetNo_XX) 的 TextChanged 事件都指向该事件实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BetNo_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != string.Empty && Convert.ToInt16(((TextBox)sender).Text)> 32)
            {
                TestLog("选号不能超过32");                
            }
            else
            {
                CheckTextFocus(sender);
            }
        }

        /// <summary>
        /// 所有 BetNo(BetNo_A1 - BetNo_XX) 的 Enter 事件都指向该事件实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BetNo_Enter(object sender, EventArgs e)
        {
            nownumbox = (TextBox)sender;
            nownumbox.Select(nownumbox.TextLength, 0);
        }
        
        ///keypress
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //TestLog(Convert.ToInt32(keyData.ToString("D")) +  "\r\n||" + keyData.ToString() + "\r\n||" + keyData);         
            if (keyData == Keys.Up)
            {

            }
            int keyvalue = Convert.ToInt32(keyData.ToString("D"));     // Convert.ToInt16(e.KeyChar);
            if ((keyvalue >= 48 && keyvalue <= 57) || ((keyvalue >= 96 && keyvalue <= 105))  || keyvalue == 262162 || keyvalue == 131089 || keyvalue == 65552)
            {
                return false;
            }
            else     //
            {   //表处理过(即该事件被抛弃，不触发输入,下面再进行具体处理;)
                object keytobtn = new object();
                keytobtn = Convert.ToString(keyData);
                KeyBtnClick(keytobtn, KeyPressEventArgs.Empty);
                return true;
                //TestLog("功能：" + e.KeyChar);
            }
        }

        //     键盘事件。
        private void PosKeyDown(object sender, KeyEventArgs e)
        {
            //TestLog(e.KeyData.ToString() + e.KeyValue.ToString());
            if ((e.KeyValue >= 48 && e.KeyValue <= 57) || (e.KeyValue >= 96 && e.KeyValue <= 105) || e.KeyValue == 262162 || e.KeyValue == 131089 || e.KeyValue == 65552)
            {
                e.Handled = false;
            }
            else// if (PosBack.IsLetter(Convert.ToString(e.KeyCode)))     //
            {
                e.Handled = true;   //表处理过(即该事件被抛弃，不触发输入,下面再进行具体处理;)
                object keytobtn = new object();
                keytobtn = Convert.ToString(e.KeyData);
                KeyBtnClick(keytobtn, KeyPressEventArgs.Empty);
                //TestLog("功能：" + e.KeyChar);
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {

            nownumbox.Focus();
        }

        /// <summary>
        /// 小键盘区域数字点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Num_Click(object sender, EventArgs e)
        {
            string numstr = ((Control)sender).Text;
            //
            nownumbox.Focus();
            if (nownumbox.Text.Length > 0)
            {
                CheckTextFocus(nownumbox);
            }
            nownumbox.Text += numstr;          
            //BetNo_TextChanged((Object)nownumbox, null);
            TestLog("NumClick " + ((Button)sender).Text);
        }
        

        /// <summary>
        /// 功能按键点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyBtnClick(object sender, EventArgs e)
        {
            TypeToType tot = new TypeToType();
            string BtnName = sender.GetType() == typeof(string) ? tot.StrToKey(sender) : ((Control)sender).Name;
            nownumbox.Focus();
            //退格BACKSPACE
            if ( BtnName == BtnBack.Name)
            {
                if (nownumbox.Text.Length > 0)
                {
                    nownumbox.Text = nownumbox.Text.Remove(nownumbox.Text.Length - 1);
                }
                else
                {
                    CheckTextFocus(nownumbox);
                }
            }
            //ESC
            if ( BtnName == BtnEscape.Name)
            {
                // 此处调用 ESC 相关
                TestLog("调用:ESC" + BtnName);
            }
            //确定Enter
            else if ( BtnName == BtnEnter.Name)
            {
                // 调用 F8Bet 投注相关
                TestLog("调用: " + BtnName); //Betqueren();
            }
            //F8Bet
            else if (BtnName == BtnF8.Name)
            {
                TestLog( "调用：Bet " + BtnName);
                if (DOF8Bet() == 0)
                {
                    Betqueren();
                }
            }
            //
            else if ( BtnName == BtnF9.Name)
            {
                TestLog("调用： " + BtnName);
            }
            else if ( BtnName == BtnA.Name)
            {
                TestLog("调用：  " + BtnName);
                panel_Bet.Visible = true;       //显示投注号码区域
            }
            else if (BtnName == BtnC.Name)
            {
                TestLog("调用：  " + BtnName);
                SwitchPage(_pageNum);
            }
        }

        #endregion

        List<TextBox> BetText = new List<TextBox>() ;
        int DOF8Bet()
        {

            PosBack posback = new PosBack();
            posback = new PosBack();
            //string key = "";
            //Control[] pancon;
            //pancon = panel_Bet.Controls.Find(key, true);
            Control.ControlCollection pans= panel_Bet.Controls;
            foreach (Control panel in pans)
            {
                if (panel.GetType() == typeof(Panel) && panel.Name == panelA.Name)
                {
                    Control.ControlCollection tboxs = panelA.Controls;
                    foreach (Control tbox in tboxs)
                    {
                        if (tbox.GetType() == typeof(TextBox) && tbox.Visible == true)
                        {
                            if (tbox.Text == string.Empty)
                            {
                                TestLog("号码不足");
                                return -1;
                            }
                            //TestLog(tbox.Name + "||" + tbox.Text);
                            if (posback.ListBetNum(tbox) != 0)
                            {
                                TestLog("号码重复！！");
                                return -2;
                            }
                        }
                    }
                    posback.SortBetNum();
                    TestLog("\r\n-----");
                    foreach (Control tbox in PosBack.tboxlist)
                    {
                        TestLog(tbox.Name + "||" + tbox.Text);
                    }
                }
                //Console.Write(conindex.Name + "||" + conindex.Text + "||" + conindex.TabIndex + "\r\n");
            }
            return 0;
            //string BetAStr = BetNo_A1.Text + BetNo_A2 + BetNo_A3 + BetNo_A4 + BetNo_A5 + BetNo_A6 + BetNo_A7 + BetNo_A8 + BetNo_A9;

        }


        public void TestLog(string str)
        {
            Console.WriteLine(str);
            textBox_test.Text += str + "\r\n";
        }

        void SwitchPage(int pagenum)
        {
            if (pagenum == 2)
            {
                panel_Bet.Visible = true;
            }
            else if (pagenum == 3)
            {

            }
                
        }

        
        private void InitBetBox()
        {
            
        }


        private void ClearBet()
        {
            
            for (int i = 0; i < 50; i++)
            {
                
            }
        }
    }

   

}
