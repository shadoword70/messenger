using System.Configuration;
using LoggerWorker;
using Ninject.Activation;
using Ninject.Modules;
using ServerMessenger.Classes;
using ServerMessenger.Configuration;
using WcfService;
using WcfService.Workers;

namespace ServerMessenger.Helpers
{
    public class Bindings : NinjectModule
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
            Bind<ISystemWorker>().To<SystemWorker>().InSingletonScope()
                .WithConstructorArgument("mail", Settings.Mail)
                .WithConstructorArgument("password", Settings.Password);
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
