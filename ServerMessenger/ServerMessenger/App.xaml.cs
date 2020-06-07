using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ServerHelper;
using ServerMessenger.Helpers;

namespace ServerMessenger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DIFactory.LoadModule(new Bindings());
            DIFactory.LoadModule(new WcfService.Helpers.Bindings());
        }
    }
}
