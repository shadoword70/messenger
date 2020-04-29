using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common.Contracts;
using LoggerWorker;

namespace WcfService
{
    public class ServiceMessenger : IServiceMessenger
    {
        public ResultStatus RegistrationClient(string name)
        {
            throw new NotImplementedException();
        }

        public ResultStatus DisconnectClient(string name)
        {
            throw new NotImplementedException();
        }        

        public ResultStatus SendMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
