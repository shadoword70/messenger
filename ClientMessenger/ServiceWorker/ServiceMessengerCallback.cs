using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Contracts;

namespace ServiceWorker
{
    public class ServiceMessengerCallback : IServiceMessengerCallback, IMessageCallback
    {
        public event EventHandler CallbackMessage;
        public event EventHandler NeedUpdateUsers;
        public void MessageCallback(DateTime date, string name, string message)
        {
            var data = new MessageCallbackData();
            data.Date = date;
            data.Name = name;
            data.Message = message;
            CallbackMessage?.Invoke(data, EventArgs.Empty);
        }

        public void UpdateUsers(List<string> users)
        {
            NeedUpdateUsers?.Invoke(users, EventArgs.Empty);
        }
    }
}
