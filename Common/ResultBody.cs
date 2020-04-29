using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class ResultBody
    {
        [DataMember]
        public ResultStatus ResultStatus { get; set; }
        [DataMember]
        public List<string> ResultData { get; set; }
    }
}
