using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common.Contracts;

namespace WcfService
{
    public class User
    {
        public Guid UserGuid { get; set; }
        public List<IServiceMessengerCallback> Callbacks { get; set; } = new List<IServiceMessengerCallback>();
    }
}
