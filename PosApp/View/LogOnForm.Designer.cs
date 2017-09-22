namespace LotPos
{
    partial class LogOnForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_UserName = new System.Windows.Forms.TextBox();
            this.textBox_PsWd = new System.Windows.Forms.TextBox();
            this.label_UserName = new System.Windows.Forms.Label();
            this.label_PsWd = new System.Windows.Forms.Label();
            this.Btn_SignIn = new System.Windows.Forms.Button();
            this.panel_LogOn = new System.Windows.Forms.Panel();
            this.Btn_Exit = new System.Windows.Forms.Button();
            this.Btn_Set = new System.Windows.Forms.Button();
            this.panel_SetConfig = new System.Windows.Forms.Panel();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.label_Port = new System.Windows.Forms.Label();
            this.label_IP = new System.Windows.Forms.Label();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel_LogOn.SuspendLayout();
            this.panel_SetConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_UserName
            // 
            this.textBox_UserName.Location = new System.Drawing.Point(133, 34);
            this.textBox_UserName.Name = "textBox_UserName";
            this.textBox_UserName.Size = new System.Drawing.Size(100, 21);
            this.textBox_UserName.TabIndex = 0;
            // 
            // textBox_PsWd
            // 
            this.textBox_PsWd.Location = new System.Drawing.Point(133, 98);
            this.textBox_PsWd.Name = "textBox_PsWd";
            this.textBox_PsWd.Size = new System.Drawing.Size(100, 21);
            this.textBox_PsWd.TabIndex = 1;
            // 
            // label_UserName
            // 
            this.label_UserName.AutoSize = true;
            this.label_UserName.Location = new System.Drawing.Point(67, 37);
            this.label_UserName.Name = "label_UserName";
            this.label_UserName.Size = new System.Drawing.Size(53, 12);
            this.label_UserName.TabIndex = 2;
            this.label_UserName.Text = "UserName";
            // 
            // label_PsWd
            // 
            this.label_PsWd.AutoSize = true;
            this.label_PsWd.Location = new System.Drawing.Point(67, 101);
            this.label_PsWd.Name = "label_PsWd";
            this.label_PsWd.Size = new System.Drawing.Size(53, 12);
            this.label_PsWd.TabIndex = 3;
            this.label_PsWd.Text = "PassWord";
            // 
            // Btn_SignIn
            // 
            this.Btn_SignIn.Location = new System.Drawing.Point(68, 143);
            this.Btn_SignIn.Name = "Btn_SignIn";
            this.Btn_SignIn.Size = new System.Drawing.Size(71, 23);
            this.Btn_SignIn.TabIndex = 4;
            this.Btn_SignIn.Text = "Sign In";
            this.Btn_SignIn.UseVisualStyleBackColor = true;
            this.Btn_SignIn.Click += new System.EventHandler(this.Btn_SignIn_Click);
            // 
            // panel_LogOn
            // 
            this.panel_LogOn.Controls.Add(this.Btn_Exit);
            this.panel_LogOn.Controls.Add(this.Btn_SignIn);
            this.panel_LogOn.Controls.Add(this.label_PsWd);
            this.panel_LogOn.Controls.Add(this.label_UserName);
            this.panel_LogOn.Controls.Add(this.textBox_PsWd);
            this.panel_LogOn.Controls.Add(this.textBox_UserName);
            this.panel_LogOn.Location = new System.Drawing.Point(92, 80);
            this.panel_LogOn.Name = "panel_LogOn";
            this.panel_LogOn.Size = new System.Drawing.Size(300, 200);
            this.panel_LogOn.TabIndex = 5;
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.Location = new System.Drawing.Point(162, 143);
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Size = new System.Drawing.Size(71, 23);
            this.Btn_Exit.TabIndex = 6;
            this.Btn_Exit.Text = "Exit";
            this.Btn_Exit.UseVisualStyleBackColor = true;
            this.Btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // Btn_Set
            // 
            this.Btn_Set.Location = new System.Drawing.Point(242, 50);
            this.Btn_Set.Name = "Btn_Set";
            this.Btn_Set.Size = new System.Drawing.Size(150, 30);
            this.Btn_Set.TabIndex = 5;
            this.Btn_Set.Text = "Setting";
            this.Btn_Set.UseVisualStyleBackColor = true;
            this.Btn_Set.Click += new System.EventHandler(this.Btn_Set_Click);
            // 
            // panel_SetConfig
            // 
            this.panel_SetConfig.Controls.Add(this.Btn_Cancel);
            this.panel_SetConfig.Controls.Add(this.Btn_Save);
            this.panel_SetConfig.Controls.Add(this.label_Port);
            this.panel_SetConfig.Controls.Add(this.label_IP);
            this.panel_SetConfig.Controls.Add(this.textBox_Port);
            this.panel_SetConfig.Controls.Add(this.textBox_IP);
            this.panel_SetConfig.Location = new System.Drawing.Point(92, 80);
            this.panel_SetConfig.Name = "panel_SetConfig";
            this.panel_SetConfig.Size = new System.Drawing.Size(300, 200);
            this.panel_SetConfig.TabIndex = 6;
            this.panel_SetConfig.Visible = false;
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Location = new System.Drawing.Point(166, 145);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(69, 24);
            this.Btn_Cancel.TabIndex = 5;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Btn_Save
            // 
            this.Btn_Save.Location = new System.Drawing.Point(68, 145);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(69, 24);
            this.Btn_Save.TabIndex = 4;
            this.Btn_Save.Text = "Save";
            this.Btn_Save.UseVisualStyleBackColor = true;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // label_Port
            // 
            this.label_Port.AutoSize = true;
            this.label_Port.Location = new System.Drawing.Point(66, 98);
            this.label_Port.Name = "label_Port";
            this.label_Port.Size = new System.Drawing.Size(29, 12);
            this.label_Port.TabIndex = 3;
            this.label_Port.Text = "port";
            // 
            // label_IP
            // 
            this.label_IP.AutoSize = true;
            this.label_IP.Location = new System.Drawing.Point(66, 35);
            this.label_IP.Name = "label_IP";
            this.label_IP.Size = new System.Drawing.Size(17, 12);
            this.label_IP.TabIndex = 2;
            this.label_IP.Text = "IP";
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(135, 95);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(100, 21);
            this.textBox_Port.TabIndex = 1;
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(135, 32);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(100, 21);
            this.textBox_IP.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(92, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 30);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // LogOnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Btn_Set);
            this.Controls.Add(this.panel_LogOn);
            this.Controls.Add(this.panel_SetConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "LogOnForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogOnForm";
            this.Load += new System.EventHandler(this.LogOnForm_Load);
            this.panel_LogOn.ResumeLayout(false);
            this.panel_LogOn.PerformLayout();
            this.panel_SetConfig.ResumeLayout(false);
            this.panel_SetConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_UserName;
        private System.Windows.Forms.TextBox textBox_PsWd;
        private System.Windows.Forms.Label label_UserName;
        private System.Windows.Forms.Label label_PsWd;
        private System.Windows.Forms.Button Btn_SignIn;
        private System.Windows.Forms.Panel panel_LogOn;
        private System.Windows.Forms.Panel panel_SetConfig;
        private System.Windows.Forms.Label label_Port;
        private System.Windows.Forms.Label label_IP;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_Save;
        private System.Windows.Forms.Button Btn_Set;
        private System.Windows.Forms.Button Btn_Exit;
        private System.Windows.Forms.Button button1;
    }
}