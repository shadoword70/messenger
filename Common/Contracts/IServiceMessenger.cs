using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Common.Request;
using Common.Results;

namespace Common.Contracts
{
    [ServiceContract(SessionMode = SessionMode.Allowed, CallbackContract = typeof(IServiceMessengerCallback))]
    public interface IServiceMessenger
    {
        [OperationContract]
        Task<AuthorizationResult> AuthorizationClient(string login, string password);

        [OperationContract]
        ResultBody DisconnectClient(Guid userGuid);

        [OperationContract(IsOneWay = true)]
        void SendMessage(Guid selfGuid, Guid chatOrUserGuid, string message);

        [OperationContract(IsOneWay = true)]
        void UpdatePhoto(Guid userGuid, byte[] photo);

        [OperationContract]
        Task<GetChatResult> GetChat(Guid chatGuid);

        [OperationContract(IsOneWay = true)]
        void CreateGroupChat(CreateGroupChatRequest request);

        [OperationContract(IsOneWay = true)]
        void CreateChat(Guid userGuid, Guid creatorGuid);

        [OperationContract]
        Task<ResultBody> ChangePassword(Guid selfGuid, string oldPassword, string newPassword);
    }

    [ServiceContract]
    public interface IServiceMessengerCallback
    {
        [OperationContract(IsOneWay = true)]
        void MessageCallback(DateTime date, Guid chatGuid, string message, Guid selfGuid);

        [OperationContract(IsOneWay = true)]
        void UpdateChats(UpdateChatsResult chats);
    }
}
