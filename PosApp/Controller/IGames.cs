using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotPos.Controller
{
    /// <summary>
    /// 抽象 玩法基类 提供游戏信息、参数
    /// </summary>
    public abstract class AbsGames
    {

        private int gameCode;   

        public List<string> gameParam;
        //public Dictionary<int, string> gameParam;

        /// <summary>
        /// 玩法参数表 用于存当前玩法的参数
        /// </summary>
        public Dictionary<string, string> paramDict;

        
        public Model.AppModel appM;     //程序的类 

        /// <summary>
        /// 切换玩法投注方式的方法
        /// </summary>
        /// <returns></returns>
        public abstract int MenuSwitch();

        /// <summary>
        /// 从取参结果中获取当前玩法的参数信息
        /// </summary>
        /// <param name="gamecode">玩法标识码</param>
        public void GetGameParam(int gamecode)
        {
            paramDict = appM.allGameParamDict[gamecode];
        }

    }
}
