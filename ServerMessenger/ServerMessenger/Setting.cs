using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMessenger
{
    public static class Setting
    {
        private static string _logPath;
        public static string LogPath
        {
            get
            {
                if (!String.IsNullOrEmpty(_logPath))
                {
                    return _logPath;
                }

                _logPath = ConfigurationManager.AppSettings.Get("LogPath");
                return _logPath;
            }
        }
    }
}
