using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
//using System.Data;

namespace LotPos
{
    public class PosConfig
    {

        public string strFileName;

        public string configName;

        public string configValue;
        public string ip;      //中间层dire服务器IP
        public int port;             //中间层dire服务器端口
        public static string xszbm;         //本机销售站编码
        public static string zdh;         //本机终端号

        public static string LoginPattern;     //启动模式

        public void GetIPAndPort(ref string serverip, ref int port)
        {

            serverip = ConfigurationManager.AppSettings["ip"];
            //ConfigurationSettings.AppSettings["ip"];

            port = Convert.ToInt16(ConfigurationManager.AppSettings["port"]);

            Console.WriteLine("|" + serverip + "\t" + port);

        }
        public void GetLoginPattern()
        {
            LoginPattern = ConfigurationManager.AppSettings["LoginPattern"];
        }

        public void UpdateIp(string serverip)
        {

            ConfigurationManager.AppSettings["ip"] = serverip;
        }

        public void UpdatePort(string port)
        {
            //ConfigurationSettings.AppSettings["port"] = port;

        }

        public PosConfig()
        {
            xszbm = ConfigurationManager.AppSettings["XSZBM"];
            zdh = ConfigurationManager.AppSettings["ZDH"];

            LoginPattern = ConfigurationManager.AppSettings["LoginPattern"];

            this.ip = ConfigurationManager.AppSettings["ip"];
            this.port = Convert.ToInt16(ConfigurationManager.AppSettings["port"]);
             
        }

        public string ReadConfig(string configKey)

        {

            configValue = "";

            //configValue = ConfigurationSettings.AppSettings["" + configKey + ""];

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
