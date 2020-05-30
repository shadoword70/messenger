using DbWorker.DbElements;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWorker
{
    class MessengerContextInitializer : CreateDatabaseIfNotExists<MessengerContext>
    {

    }

    public class MessengerContext : DbContext
    {
        public MessengerContext() : base("DbConnection")
        {
            //Database.SetInitializer<MessengerContext>(new MessengerContextInitializer());
        }

        public DbSet<User> User { get; set; }
        public DbSet<Chat> Сhat { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<MessageStatus> MessageStatus { get; set; }
        public DbSet<Party> Party { get; set; }
        public DbSet<Message> Message { get; set; }
    }
}
