using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Contracts;
using Common.Results;

namespace ServiceWorker
{
    public interface IServiceManager
    {
        Task<AuthorizationResult> Authorization(string name, string password);
        ResultBody Disconnect(string name);
        void SendMessage(string name, string message);
        event EventHandler Disconnected;

    }
}
