using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
//using System.Data;

namespace WindowsFormsApp2
{
    public class PosConfig
    {

        public string strFileName;

        public string configName;

        public string configValue;
        public string ServerIP;
        public int Port;
        public string LoginPattern;

        public void GetIPAndPort(ref string serverip, ref int port)
        {

            serverip = ConfigurationManager.AppSettings["ServerIP"];
            //ConfigurationSettings.AppSettings["ServerIP"];

            port = Convert.ToInt16(ConfigurationManager.AppSettings["Port"]);

            Console.WriteLine("|" + serverip + "\t" + port);

        }
        public void GetLoginPattern()
        {
            LoginPattern = ConfigurationManager.AppSettings["LoginPattern"];
        }

        public void UpdateIp(string serverip)
        {

            ConfigurationManager.AppSettings["ServerIP"] = serverip;
        }

        public void UpdatePort(string port)
        {
            ConfigurationSettings.AppSettings["Port"] = port;

        }

        public PosConfig()

        {

            ServerIP = ConfigurationManager.AppSettings["ServerIP"];

            Port = Convert.ToInt16(ConfigurationManager.AppSettings["Port"]);
            //
            LoginPattern = ConfigurationManager.AppSettings["LoginPattern"];

            // TODO: 在此处添加构造函数逻辑

            //

        }

        public string ReadConfig(string configKey)

        {

            configValue = "";

            configValue = ConfigurationSettings.AppSettings["" + configKey + ""];

            return configValue;

        }

        //得到程序的config文件的名称以及其所在的全路径

        public void SetConfigName(string strConfigName)

        {

            configName = strConfigName;

            //获得配置文件的全路径

            GetFullPath();

        }

        public void GetFullPath()

        {

            //获得配置文件的全路径

            strFileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + configName;

        }

        public void SaveConfig(string configKey, string configValue)

        {

            XmlDocument doc = new XmlDocument();

            doc.Load(strFileName);

            //找出名称为“add”的所有元素

            XmlNodeList nodes = doc.GetElementsByTagName("add");

            for (int i = 0; i < nodes.Count; i++)
            {

                //获得将当前元素的key属性

                XmlAttribute att = nodes[i].Attributes["key"];

                //根据元素的第一个属性来判断当前的元素是不是目标元素

                if (att.Value == "" + configKey + "")

                {

                    //对目标元素中的第二个属性赋值

                    att = nodes[i].Attributes["value"];

                    att.Value = configValue;

                    break;

                }

            }

            //保存上面的修改

            doc.Save(strFileName);

        }

    }
}
