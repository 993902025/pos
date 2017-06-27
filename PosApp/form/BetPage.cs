using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotPos.form
{
    public class BetPage
    {
        string _wf;

        List<List<TextBox>> lstTextBox;

        int _location_x;
        int _location_y;
        int _count_x;
        int _count_y;
        public BetPage(string wf)
        {
            _location_x = 40;
            _location_y = 55;
            _count_x = 0;
            _count_y = 0;

            lstTextBox = new List<List<TextBox>>();
                        
        }

        void CreatBox(string wf)
        {
            switch (wf)
            {
                case "c515":
                    _count_x = 5;
                    _count_y = 1;
                    break;
                default:
                    break;
            }

            for (int i = 1; i <= _count_y; i++)
            {
                string name1 = "A";
                for (int j = 1; j < _count_x; j++)
                {
                    string name2 = Convert.ToString(i);
                    TextBox tbox = new TextBox();

                    tbox.Name = "Bet"+name1 + name2;


                }
            }

            

        }
    }
}
