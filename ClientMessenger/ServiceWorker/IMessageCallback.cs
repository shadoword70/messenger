using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceWorker
{
    public interface IMessageCallback
    {
        event EventHandler CallbackMessage;
        event EventHandler NeedUpdateUsers;
    }
}
