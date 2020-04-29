using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMessenger.Configuration
{
    [ConfigurationCollection(typeof(LoggerElement))]
    public class LoggersCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new LoggerElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LoggerElement)(element)).Writer;
        }
        public LoggerElement this[int idx]
        {
            get { return (LoggerElement)BaseGet(idx); }
        }
    }
}
