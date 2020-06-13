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
        Task<AuthorizationResult> Authorization(string login, string password);
        ResultBody Disconnect(Guid userGuid);
        void SendMessage(Guid selfGuid, Guid chatOrUserGuid, string message);
        void UpdatePhoto(Guid userGuid, byte[] photo);
        Task<GetChatResult> GetChat(Guid chatGuid);
        event EventHandler Disconnected;

    }
}
