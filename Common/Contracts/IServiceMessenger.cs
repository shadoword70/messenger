using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Contracts
{
    [ServiceContract(CallbackContract = typeof(IServiceMessengerCallback))]
    public interface IServiceMessenger
    {
        [OperationContract]
        ResultBody RegistrationClient(string name);

        [OperationContract]
        ResultBody DisconnectClient(string name);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string name, string message);
    }

    public interface IServiceMessengerCallback
    {
        [OperationContract(IsOneWay = true)]
        void MessageCallback(DateTime date, string name, string message);

        [OperationContract(IsOneWay = true)]
        void UpdateUsers(List<string> users);
    }
}
