using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using DbWorker;
using LoggerWorker;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using Ninject.Parameters;
using ServerMessenger.Classes;
using ServerMessenger.Configuration;
using WcfService;

namespace ServerMessenger.Helpers
{
    public static class DIFactory
    {
        private static IKernel _kernel;

        private static void InitialKernel()
        {
            if (_kernel == null)
            {
                CreateKernel();
            }
        }

        public static T Resolve<T>()
        {
            InitialKernel();
            return _kernel.Get<T>();
        }

        public static T Resolve<T>(string name)
        {
            InitialKernel();
            return _kernel.Get<T>(name);
        }

        public static T Resolve<T>(string name, IParameter parameter)
        {
            InitialKernel();
            return _kernel.Get<T>(name, parameter);
        }

        public static T Resolve<T>(IParameter[] parameters)
        {
            InitialKernel();
            return _kernel.Get<T>(parameters);
        }

        public static T Resolve<T>(IParameter parameter)
        {
            InitialKernel();
            return _kernel.Get<T>(parameter);
        }

        private static void CreateKernel()
        {
            var module = new Bindings();
            _kernel = new StandardKernel(module);
        }


    }

    class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().ToMethod(GetLogger).InSingletonScope();
            Bind<IServiceManager>().To<ServiceManager>()
                .InSingletonScope()
                .WithConstructorArgument(typeof(ILogger))
                .WithConstructorArgument(typeof(string))
                .WithConstructorArgument(typeof(string))
                .WithConstructorArgument(typeof(string));
            Bind<IDbSystemWorker>().To<DbSystemWorker>().InSingletonScope();
            Bind<ISystemWorker>().To<SystemWorker>().InSingletonScope();
        }

        private ILogger GetLogger(IContext arg)
        {
            System.Configuration.Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            LogsSettingsConfigurationSection section = (LogsSettingsConfigurationSection)cfg.Sections["LogSettings"];

            var path = Settings.LogPath;
            Logger logger;
            if (section.LoggerItems.Count == 1)
            {
                var item = section.LoggerItems[0];
                var level = item.LogLevel;
                logger = new Logger(level, path);
            }
            else
            {
                logger = new Logger(LogLevel.Info, path);
            }

            return logger;
        }
    }
}
