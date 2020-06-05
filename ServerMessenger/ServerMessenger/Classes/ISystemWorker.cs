using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ServerMessenger.Classes
{
    interface ISystemWorker
    {
        Task AddEmployee(Employee employee);
    }
}
