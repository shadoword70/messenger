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
    public class ServiceMessengerCallback : IServiceMessengerCallback, IMessageCallback
    {
        public event EventHandler CallbackMessage;
        public event EventHandler NeedUpdateChats;
        public void MessageCallback(DateTime date, Guid chatGuid, string message, Guid selfGuid)
        {
            var data = new MessageCallbackData();
            data.Date = date;
            data.ChatGuid = chatGuid;
            data.Message = message;
            data.SendedUserGuid = selfGuid;
            CallbackMessage?.Invoke(data, EventArgs.Empty);
        }

        public void UpdateChats(UpdateChatsResult chats)
        {
            NeedUpdateChats?.Invoke(chats, EventArgs.Empty);
        }
    }
}
