using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Contracts;
using Common.Request;
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
        void CreateGroupChat(CreateGroupChatRequest request);
        event EventHandler Disconnected;

        void CreateChat(Guid userGuid, Guid creatorGuid);
        Task<ResultBody> ChangePassword(Guid selfGuid, string oldPassword, string newPassword);
    }
}
