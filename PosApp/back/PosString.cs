using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotPos
{
    public class PosString
    {
        string key = "";
        const string SEP1 = "|";

        public PosString()
        {

        }

        /// <summary>
        /// 生成数据包(所有)
        /// </summary>
        /// <param name="s_r"> 包类型：1为请求包，0为应答包；</param>
        /// <param name="opcode"> 操作码：包请求操作动作 </param>
        /// <param name="istr"> 包体数据域 </param>
        /// <param name="ostr"> 传出的完整数据包</param>
        public void IniMsgStr(string s_r, string opcode, string istr, ref string ostr)
        {
            string str;
            Random r = new Random(6666);        //  操作序列号：随机数
            
            str = s_r + SEP1 + 1 + SEP1 + 0 + SEP1 + r.Next(9999).ToString().PadLeft(4, '0') + SEP1 + opcode + SEP1 + istr;
            switch (opcode)
            {

                case "GETPARAM":
                    //encode
                    break;
                default:
                    break;
            }

            str = str + SEP1;           

            ostr = "@" + str.Length.ToString().PadLeft(4,'0') + SEP1 + str;
            
        }


        public void Encode(string istr, ref string ostr)
        {
            
        }


    }
}
