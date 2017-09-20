using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace LotPos.Controller
{
    public class TimeTaskControl
    {
        private const int GETTIMETASK = 3000;

        private bool isServerOk;

        private int taskId, tasktick;


        Timer timer;

        public TimeTaskControl()
        {
        }

        public TimeTaskControl(int taskId, bool isServerOk)
        {
            this.taskId = taskId;
            this.isServerOk = isServerOk;
        }

        public void SelectTimeTask()
        {
            switch (taskId)
            {
                case GETTIMETASK:
                    SetTimeTask(GETTIMETASK);
                    break;
                case 1110:
                    break;
                default:
                    //非法任务
                    break;
            };
        }


        public void SetTimeTask( int GETTIMETASK)
        {
            try
            {
                timer = new Timer(new TimerCallback(StartTimeTask), null, GETTIMETASK, GETTIMETASK);
            }
            catch (Exception exc)
            {
                //error日志
            }
        }


        public void StartTimeTask(object value)
        {
            while (!isServerOk)
            {
                timer.Change(Timeout.Infinite, GETTIMETASK);
            }
            SocketClass sock;
        }




    }


}
