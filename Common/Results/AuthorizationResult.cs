using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Common.Results
{
    [DataContract]
    public class AuthorizationResult
    {
        [DataMember]
        public Employee Employee { get; set; }

        [DataMember]
        public ResultBody InfoBody { get; set; }

        [DataMember]
        public List<User> Users { get; set; }

        //[DataMember]
        //public List<Chat> Chats { get; set; }
    }
}
