using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Common.Results;

namespace Common.Contracts
{
    [ServiceContract(CallbackContract = typeof(IServiceMessengerCallback))]
    public interface IServiceMessenger
    {
        [OperationContract]
        Task<AuthorizationResult> AuthorizationClient(string login, string password);

        [OperationContract]
        ResultBody DisconnectClient(string name);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string name, string message);
    }

    [ServiceContract]
    public interface IServiceMessengerCallback
    {
        [OperationContract(IsOneWay = true)]
        void MessageCallback(DateTime date, string name, string message);

        [OperationContract(IsOneWay = true)]
        void UpdateUsers(List<string> users);
    }
}
