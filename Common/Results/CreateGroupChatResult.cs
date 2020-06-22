using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Results
{
    [DataContract]
    public class CreateGroupChatResult
    {
        [DataMember]
        public Guid ChatGuid { get; set; }
    }
}
