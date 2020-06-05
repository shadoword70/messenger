using DbWorker;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ServerMessenger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            using (var context = new MessengerContext()) 
            {
                context.Database.CreateIfNotExists();
            }

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MessengerContext, DbWorker.Migrations.Configuration>());
        }
    }
}
