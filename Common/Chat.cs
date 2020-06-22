using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Chat
    {
        public List<Guid> UserGuids { get; set; }
        public Guid ChatGuid { get; set; }
        public string ChatName { get; set; }
    }
}
