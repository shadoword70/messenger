using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Request
{
    [DataContract]
    public class CreateGroupChatRequest
    {
        [DataMember]
        public string ChatName { get; set; }

        [DataMember]
        public List<Guid> UserGuids { get; set; }

        [DataMember]
        public Guid CreatorGuid { get; set; }
    }
}
