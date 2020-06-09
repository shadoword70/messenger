using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MessageCallbackData
    {
        public DateTime Date { get; set; }
        public Guid ChatGuid { get; set; }
        public string Message { get; set; }
        public Guid SendedUserGuid { get; set; }
    }
}
