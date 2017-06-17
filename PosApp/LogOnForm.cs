using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class LogOnForm : Form
    {
        PosConfig posconfig;

        string ip;
        int port;

        public LogOnForm()
        {
            InitializeComponent();
        }

        private void LogOnForm_Load(object sender, EventArgs e)
        {
            
            posconfig = new PosConfig();

            posconfig.GetIPAndPort(ref ip, ref port);
            
        }

        //登录 按钮
        private void Btn_SignIn_Click(object sender, EventArgs e)
        {

            if (Con_Director(ip, port) == 0)
            {

                this.DialogResult = DialogResult.OK;

                this.Close();
                                                               
            }

        }

        int Con_Director(string ip, int port)
        {

            //socket 类处理
            return 0;

        }

        
        //set 报存
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



            panel_SetConfig_Show();

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

        void panel_SetConfig_Show()
        {
            textBox_IP.Text = ip;
            textBox_Port.Text = port.ToString();
            panel_LogOn.Visible = false;
            panel_SetConfig.Visible = true;

        }
        void panel_LogOn_Show()
        {
            panel_LogOn.Visible = true;
            panel_SetConfig.Visible = false;

        }
    }
}
