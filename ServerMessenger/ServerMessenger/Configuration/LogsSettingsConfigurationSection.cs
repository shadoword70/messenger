using System.Configuration;

namespace ServerMessenger.Configuration
{
    public class LogsSettingsConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("Loggers")]
        public LoggersCollection LoggerItems
        {
            get { return ((LoggersCollection)(base["Loggers"])); }
        }
    }
}
