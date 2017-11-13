using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotPos.Model
{
    public class MessageBuffer
    {
        public string type { get; set; }

        public string body { get; set; }

        public MessageBuffer(string type, string body)
        {
            this.type = type;
            this.body = body;
        }        
    }
}
