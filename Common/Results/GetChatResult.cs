using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Results
{
    [DataContract]
    public class GetChatResult
    {
        [DataMember]
        public List<Message> Messages { get; set; }
    }

    [DataContract]
    public class Message
    {
        [DataMember]
        public Guid UserGuid { get; set; }
        [DataMember]
        public Guid ChatGuid { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public DateTime DateCreate { get; set; }
        [DataMember]
        public Guid Guid { get; set; }
        [DataMember]
        public Employee Employee { get; set; }
        [DataMember]
        public MessageStatus MessageStatus { get; set; }
    }

    [DataContract]
    public class MessageStatus
    {
        [DataMember]
        public Guid UserGuid { get; set; }
        [DataMember]
        public Guid MessageGuid { get; set; }
        [DataMember]
        public bool IsRead { get; set; }
    }
}
