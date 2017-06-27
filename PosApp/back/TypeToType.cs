using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotPos
{
    public class TypeToType
    {
        string key = "";
        public TypeToType()
        {

        }

        public string StrToKey(object sender)
        {
            key = "Btn" + Convert.ToString(sender);
            return key;
        }

        public string IntToKey(object sender)
        {
            key = "Btn" + Convert.ToString((Keys)Convert.ToInt16(sender));
            return key;
        }
    }

}
