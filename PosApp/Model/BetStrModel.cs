using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotPos.Medel
{
    public class BetStrModel
    {
        private int game_Id;

        private string multiple, betNoStr, betStr;

        public BetStrModel() { }

        public BetStrModel(int game_Id, string multiple, string betNoStr)
        {
            this.game_Id = game_Id;
            this.multiple = multiple;
            this.betNoStr = betNoStr;
        }

        public void CreateBetStr()
        {
            betStr = multiple  + betNoStr.Length/2 + betNoStr;
        }

        
    }
}
