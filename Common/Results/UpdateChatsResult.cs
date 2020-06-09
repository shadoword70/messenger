using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;

namespace Common.Results
{
    [DataContract]
    public class UpdateChatsResult
    {
        [DataMember]
        public List<User> Users { get; set; }

        [DataMember]
        public ResultBody InfoBody { get; set; }

        [DataMember]
        public List<Party> Chats { get; set; }
    }

    public class User
    {
        public Guid Guid { get; set; }

        public Guid ChatGuid { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Position { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public bool IsOnline { get; set; }

        public byte[] EmployeePhoto { get; set; }
    }
}
