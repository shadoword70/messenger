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
        public string Login { get; set; }
        public OperationContext Context { get; set; }
    }
}
