using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Results
{
    [DataContract]
    public class AuthorizationResult
    {
        [DataMember]
        public Employee Employee { get; set; }


        [DataMember]
        public List<string> Users { get; set; }

        [DataMember]
        public ResultBody InfoBody { get; set; }
    }
}
