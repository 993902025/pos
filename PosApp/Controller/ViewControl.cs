using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotPos.Controller
{
    public class ViewControl
    {


        public ViewControl() { }

        public void LoadLogForm(bool value)
        {

            LogOnForm logonform = new LogOnForm();

            logonform.ShowDialog();
            
            logonform.Close();

        }


    }
}
