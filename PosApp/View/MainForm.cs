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
using JunKeyBoardLib;
using LotPos.Model;
using LotPos.Controller;
using LotPos.Controller.NetControls;
using System.Threading;

namespace LotPos
{
    public partial class MainForm : Form
    {
        AppModel app;
        Model.MenuModel menu;
        ViewControl viewcontrol;


        /* 定义类 
         */
        //public LogOnForm logonform;     //登录界面类

        SocketClass betsock = new SocketClass();

        //JunKeyBoard jkb;

        /* 控件参数类
         */
        TextBox nownumbox = null;       //当期输入框控件

        int betcount;

        int heartbeatdt = 0;

        string sendbetstr = "";
        static int sequence = 0;
        string sk = "111111";
        byte[] btmsg = new byte[1024];
        string smsg2 = "";
        string ServerIP = "10.1.1.192";
        //int selport = 9902;
        int betport = 9901;
        static int _lsh = 1;

        public MainForm()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// 加载主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            app = AppModel.Instance;

            viewcontrol = new ViewControl();

            viewcontrol.LoadLogForm(app.isStartOnline); //加载登录界面   

            tabControl.Visible = true;     //标签控制页

            ShowPage_1(1);
            Update_panel_Parameters_Show(1); //

        }

        /// <summary>
        /// 处理登录界面返回结果
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        void LoadLogOnForm(bool value)
        {
        }
        
        /// <summary>
        /// 显示启动模式,将配置文件中内容转成中文显示
        /// "1"="单机" "2"="联网"
        /// </summary>
        /// <param name="loginPattern"> "1"="单机" "2"="联网"</param>
        void ShowPattern(string loginPattern)
        {
            switch (loginPattern)
            {
                case "1":
                    label_LoginPattern.Text = "单机";
                    break;
                case "2":
                    label_LoginPattern.Text = "联网";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 站点玩法等参数显示
        /// </summary>
        void Update_panel_Parameters_Show(int wf)
        {
            //PosBack posback = new PosBack();
            //posback.GetPra(_loginPattern);       //模拟取参
        }
        
        void ShowPage_1(int pagenum)
        {
            tabControl.SelectTab(5);
            tabControl.SelectedTab.Controls.Add(panel_Parameters);
            panel_Parameters.Show();
            panel_Bet.Show();
            panel_keyboard.Show();
        }

        /// <summary>
        /// 登录\注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Logonoff_Click(object sender, EventArgs e)
        {

            RegisterControl regcon = new RegisterControl();            
            regcon.send();
            regcon.recv();
        }

        private void SockThread()
        {
            SocketInstance sock = new SocketInstance();
        }

        /// <summary>
        /// 投注确认 显示票面
        /// </summary>
        void Betqueren()
        {
            if (true)
            {
                groupBox_LotteryPicture.BackColor = Color.FromArgb(255, 192, 192);
                
                Label lab = new Label();
                //groupBox_LotteryPicture.Controls.Add(lab);
                //BetNum betnumclass = new BetNum();
                //for (int i = 0; i < betnumclass.lstbetnum.Count; i++)
                //{
                //    lab.Text = betnumclass.lstbetnum[i];
                //}
            }
        }


        //select 
        private void Button1_Click(object sender, EventArgs e)
        {
        }

        //showbet button 生成投注号码 、 检查投注号码 、 生成发送串 
        private void Btn_test_Click(object sender, EventArgs e)
        {
            //PosFile pf = new PosFile();
            //textBox_test.Text += pf._filename;
            //string sball = redballstring.Text;
            //sendbetstr = ini_betstr(sball);
            //TestLog("");
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
            if (0 == betnum.Cfof_check_ball(sball, ref playtype, ref money))
            {
                string betdetail = (sball.Length / 2).ToString().PadLeft(2, '0') + sball;            //号码串   （倍数+号码个数+号码）
                money = 1;
                string smoney = string.Format("{0:f2}", money);
                string betmsgbody = agentid + "$" + gamename + "$" + drawno + "$" + ticket + "$" + playtype + "$" + smoney + "$" + betdetail + "$" + "$" + "$" + "$" + "$" + "01" + "$" + "$";
                textBox3.Text += "\r\nbetbody:" + betmsgbody;
                return betmsgbody;
            }
            else if (-1 == betnum.Cfof_check_ball(sball, ref playtype, ref money))
            {
                textBox_test.Text += "\r\n[ERR]:" + "投注号码个数有误";
                return "";
            }
            else if (-3 == betnum.Cfof_check_ball(sball, ref playtype, ref money))
            {
                textBox_test.Text += "\r\n[ERR]:" + "投注号码有重复";
                return "";
            }
            else
            {
                textBox_test.Text += "\r\n[ERR]:" + betnum.Cfof_check_ball(sball, ref playtype, ref money);
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
            if (0 == betsock.Inisocket())
            {
                //发送send
                betsock.Sendmsg(smsg2);
                textBox3.Text += "send message is:" + smsg2 + "\r\n";
                //接受recv
                string srecmsg = betsock.Recvmsg();

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

        //关闭窗口事件
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (false)  //完整版应该为 (_pageNum != 0 ) 调试版本省略确认框 == 0
            {
            }
            try
            {
                betsock.Closesock();
            }
            catch
            {
            }
        }

        //信息输出栏定位末行
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox obj = (TextBox)sender;
            obj.Focus();//获取焦点
            obj.Select(obj.TextLength, 0);//光标定位到文本最后
            obj.ScrollToCaret();
        }

        //信息输出栏定位末行
        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

            textBox_test.Focus();//获取焦点
            textBox_test.Select(textBox_test.TextLength, 0);//光标定位到文本最后
            textBox_test.ScrollToCaret();
            //nownumbox.Focus();  //焦点返回到上一指定控件

        }

        private void ToolTip1_Popup(object sender, PopupEventArgs e)
        {
        }
        
        /// <summary>
        /// 所有 BetNo(BetNo_A1 - BetNo_XX) 的 TextChanged 事件都指向该事件实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BetNo_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 所有 BetNo(BetNo_A1 - BetNo_XX) 的 Enter 事件都指向该事件实现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BetNo_Enter(object sender, EventArgs e)
        {
            nownumbox = (TextBox)sender;
            nownumbox.SelectionStart = nownumbox.Text.Length;
            string numstr = ((Control)sender).Text;

            betnuminputbox.Text += numstr;

            //nownumbox.Select(nownumbox.TextLength, 0);
        }


        /// <summary>
        /// 激活主界面时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            string numstr = ((Control)sender).Tag.ToString();
            
            betnuminputbox.Text += numstr;
            //BetNo_TextChanged((Object)nownumbox, null);
            TestLog("NumClick " + ((Button)sender).Tag.ToString());
        }


        /// <summary>
        /// 功能按键点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyBtnClick(object sender, EventArgs e)
        {
            string BtnName = sender.GetType() == typeof(string) ? ("Btn" + Convert.ToString(sender)) : ((Control)sender).Name;
            
            //退格BACKSPACE
            if (BtnName == BtnBack.Name)
            {

            }
            //ESC
            else if (BtnName == BtnEscape.Name)
            {
                // 此处调用 ESC 相关
                TestLog("调用:ESC" + BtnName);
            }
            //确定Enter
            else if (BtnName == BtnEnter.Name)
            {
                // 调用 F8Bet 投注相关
                TestLog("调用: " + BtnName); //Betqueren();
            }
            //F8Bet
            else if (BtnName == BtnF8.Name)
            {
                TestLog("调用：Bet " + BtnName);
            }
            //
            else if (BtnName == BtnF9.Name)
            {
                TestLog("调用： " + BtnName);
            }
            else if (BtnName == BtnA.Name)
            {
                TestLog("调用：  " + BtnName);
                panel_Bet.Visible = true;       //显示投注号码区域
            }
            else if (BtnName == BtnC.Name)
            {
                TestLog("调用：  " + BtnName);
            }
        }



        public void TestLog(string str)
        {
            Console.WriteLine(str);
            textBox_test.Text += str + "\r\n";
        }


        /// <summary>
        /// 清空投注区内号码
        /// </summary>
        private void ClearBet()
        {
        }
        

        /// <summary>
        /// 切换选择标签页时，将参数列表数据更新匹配该page，并显示在该page，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            int index = tabControl.SelectedIndex;
            Console.WriteLine(index);
            Update_panel_Parameters_Show(index);

            tabControl.SelectedTab.Controls.Add(panel_Parameters);
            tabControl.SelectedTab.Controls.Add(panel_Bet);
            panel_Parameters.Show();
            panel_Bet.Show();
        }

        private void TabControlChange()
        {

        }



        private void PaintPicture()
        {

            pic1.Visible = true;
            pic2.Visible = true;
            pic3.Visible = true;
            pic4.Visible = true;
            pic5.Visible = true;
            pic6.Visible = true;
            pic7.Visible = true;
            pic1.Text = "43E00E-7DC398B-000000-000000-00";
            pic2.Text = "站点\0";
            pic3.Text = "特征码 AAAAA00000BBBBB0  654321";
            pic4.Text = "流水号\0";
            pic5.Text = "销售期\0";
            pic6.Text = "兑奖期\0";
            pic7.Text = "金额\0";

        }




        
        private void AppKeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("Form.KeyPress: '" + e.KeyChar.ToString() + "' pressed.");
            if (e.KeyChar >= KeyPro.PAGEUP && e.KeyChar <= KeyPro.Z)
            {
                switch (e.KeyChar)
                {
                    case (char)KeyPro.PAGEUP: break;
                    case (char)KeyPro.PAGEDOWN: break;
                    case (char)KeyPro.END: break;
                    case (char)KeyPro.HOME: break;
                    case (char)KeyPro.LEFT: break;
                    case (char)KeyPro.UP: break;
                    case (char)KeyPro.RIGHT: break;
                    case (char)KeyPro.DOWN: break;
                    case (char)KeyPro.ZERO: break;
                    case (char)KeyPro.ONE: break;
                    case (char)KeyPro.TWO: break;
                    case (char)KeyPro.THREE: break;
                    case (char)KeyPro.FOUR: break;
                    case (char)KeyPro.FIVE: break;
                    case (char)KeyPro.SIX: break;
                    case (char)KeyPro.SEVEN: break;
                    case (char)KeyPro.EIGHT: break;
                    case (char)KeyPro.NINE: break;
                    case (char)KeyPro.A: break;
                    case (char)KeyPro.B: break;
                    case (char)KeyPro.C: break;
                    case (char)KeyPro.D: break;
                    case (char)KeyPro.E: break;
                    case (char)KeyPro.F: break;
                    case (char)KeyPro.G: break;
                    case (char)KeyPro.H: break;
                    case (char)KeyPro.I: break;
                    case (char)KeyPro.J: break;
                    case (char)KeyPro.K: break;
                    case (char)KeyPro.L: break;
                    case (char)KeyPro.M: break;
                    case (char)KeyPro.N: break;
                    case (char)KeyPro.O: break;
                    case (char)KeyPro.P: break;
                    case (char)KeyPro.Q: break;
                    case (char)KeyPro.R: break;
                    case (char)KeyPro.S: break;
                    case (char)KeyPro.T: break;
                    case (char)KeyPro.U: break;
                    case (char)KeyPro.V: break;
                    case (char)KeyPro.W: break;
                    case (char)KeyPro.X: break;
                    case (char)KeyPro.Y: break;
                    case (char)KeyPro.Z:
                        break;
                    default:
                        break;

                }
                e.Handled = true;
            }

            if (e.KeyChar >= 0 && e.KeyChar <= 9)
            {
                Console.WriteLine("Form.KeyPress: '" + e.KeyChar.ToString() + "' number.");
            }

        }
        
    }
}
