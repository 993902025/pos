using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotPos.Medel
{
    public class MenuModel
    {

        public int menuId_I { get; set; }
        public int menuId_II { get; set; }
        public int menuId_III { get; set; }

        public MenuModel() { }

        public MenuModel(int menuId_I, int menuId_II, int menuId_III)
        {
            this.menuId_I = menuId_I;
            this.menuId_II = menuId_I;
            this.menuId_III = menuId_III;
        }


    }

    public class GameModel
    {
        public int gameId, betId;   //投注玩法 投注方式
    }

    /// <summary>
    /// 程序自身
    /// </summary>
    public class AppModel
    {

        public static AppModel instance;

        public bool isStartOnline;

        private string ip;

        private int port;



        public static AppModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppModel();
                }
                return instance;
            }
        }

        private AppModel()
        {

        }

    }


    

}
