using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    public class User
    {
        public Guid UserGuid { get; set; }
        public List<OperationContext> Contexts { get; set; } = new List<OperationContext>();
    }
}
