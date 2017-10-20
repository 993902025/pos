using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotPos.Model
{
    public class MenuModel
    {
        public int thisPage;
        public int lastPage;
        public int nextPage;

        public object ViewInstance;




         
        public MenuModel() { }

        public MenuModel(int menuId_I, int menuId_II, int menuId_III)
        { 
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

        public bool isStartOnline;  //启动模式

        public bool isLogIn;    //登录状态

        public bool isOnline;   //网络在线状态

        public string ip;

        public int port;
        
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
            this.isStartOnline = false;
            this.isLogIn = false;
            this.isOnline = false;
            
        }
        
    }

    public  class ViewModel
    {

        public Dictionary<string, string> paramDict;
        
        public List<string> paramList;

        public ViewModel() { }

    }

    

}
