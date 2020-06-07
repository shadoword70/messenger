using System;
using System.Configuration;

namespace ClientMessenger
{
    public static class Settings
    {
        private static string _WCFServerIP;
        public static string WCFServerIP
        {
            get
            {
                if (!String.IsNullOrEmpty(_WCFServerIP))
                {
                    return _WCFServerIP;
                }

                _WCFServerIP = ConfigurationManager.AppSettings.Get("WCFServerIP");
                return _WCFServerIP;
            }
        }
    }
}
