using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggerWorker;

namespace ServerMessenger.Configuration
{
    public class LoggerElement : ConfigurationElement
    {
        [ConfigurationProperty("Writer", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Writer
        {
            get { return ((string)(base["Writer"])); }
            set { base["Writer"] = value; }
        }

        [ConfigurationProperty("LogLevel", DefaultValue = "Debug", IsKey = false, IsRequired = false)]
        public LogLevel LogLevel
        {
            get
            {
                return (LogLevel)base["LogLevel"];
            }
            set { base["LogLevel"] = value; }
        }
    }
}
