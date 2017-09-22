using LotPos.back;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotPos
{
    public partial class LogOnForm : Form
    {
        PosConfig posconfig;

        string ip;
        int port;
        string username;
        string password;
        string loginpattern;
        public LogOnForm()
        {
            InitializeComponent();
        }

        private void LogOnForm_Load(object sender, EventArgs e)
        {
            
            posconfig = new PosConfig();

            posconfig.GetIPAndPort(ref ip, ref port);

            loginpattern = PosConfig.LoginPattern;

            textBox_UserName.Focus();
        }

        //登录 按钮
        /// <summary>
        /// 登录按钮，判断用户名、密码输入合法性；执行登录；连接定向+接入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_SignIn_Click(object sender, EventArgs e)
        {
            if (textBox_UserName.Text.Length == 0 || textBox_UserName.Text.Length < 1)
            {
                //(new WarningModel("账号不合法", delegate { print("回调测试"); }));
                return;
            }
            if (textBox_PsWd.Text.Length == 0 || textBox_PsWd.Text.Length < 1)
            {
                //(new WarningModel("密码不合法"));
                return;
            }
            
            LoginBack loginback = new LoginBack();
            username = textBox_UserName.Text;
            password = textBox_PsWd.Text;
            int result = loginback.Login(username, password);

            //启动模式决定是否进行连接定向
            //if (loginpattern == "online")
            //{
            //    PosBack posback = new PosBack();
            //    switch (posback.Con_Director(sock))
            //    {
            //        case 0:

            //            ;//连接成功;
            //            break;
            //        case -1:
            //            //
            //            return;
            //        case -2:
            //            return;
            //        default:
            //            return;
            //    }                
            //}

            this.DialogResult = DialogResult.OK;   
            this.Close();
        }



        
        //set 保存
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if (textBox_IP.Text != ip && textBox_Port.Text != port.ToString())
            {
                if (MessageBox.Show("IP 和 PORT 被修改,确定保存?\n当前有效  IP  = " + ip + "\t当前有效 Port = " + port + "\n修改为？  IP  = " + textBox_IP.Text + "\t修改为？ Port = " + textBox_Port.Text, "注意！！！！！", MessageBoxButtons.YesNoCancel) == DialogResult.OK )
                {
                    posconfig.UpdateIp(textBox_IP.Text);
                    posconfig.UpdatePort(textBox_Port.Text);
                }
            }
            else if(textBox_IP.Text != ip)
            {
                if (MessageBox.Show("IP 被修改,确定保存?\n当前有效  IP  = " + ip + "\n修改为？  IP  = " + textBox_IP.Text, "注意！！！！！", MessageBoxButtons.YesNoCancel) == DialogResult.OK)
                {
                    posconfig.UpdateIp(textBox_IP.Text);
                }
            }
            else if (textBox_Port.Text != port.ToString())
            {
                if (MessageBox.Show("PORT 被修改,确定保存?\n当前有效 Port = " + port + "\n修改为？ Port = " + textBox_Port.Text, "注意！！！！！", MessageBoxButtons.YesNoCancel) == DialogResult.OK)
                {
                    posconfig.UpdatePort(textBox_Port.Text);
                }
            }



            Panel_SetConfig_Show();

        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {

            panel_LogOn.Visible = true;
            panel_SetConfig.Visible = false;

        }

        private void Btn_Set_Click(object sender, EventArgs e)
        {

            textBox_IP.Text = ip;
            textBox_Port.Text = port.ToString();
            panel_LogOn.Visible = false;
            panel_SetConfig.Visible = true;

        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        void Panel_SetConfig_Show()
        {
            textBox_IP.Text = ip;
            textBox_Port.Text = port.ToString();
            panel_LogOn.Visible = false;
            panel_SetConfig.Visible = true;

        }
        void Panel_LogOn_Show()
        {
            panel_LogOn.Visible = true;
            panel_SetConfig.Visible = false;
        }


    }
}
