using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Contracts;

namespace ServiceWorker
{
    public interface IServiceManager
    {
        ResultBody Registration(string name);
        ResultBody Disconnect(string name);
        void SendMessage(string name, string message);
        event EventHandler Disconnected;

    }
}
