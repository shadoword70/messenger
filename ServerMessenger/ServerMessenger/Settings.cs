using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMessenger
{
    public static class Settings
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

        private static string _mail;
        public static string Mail
        {
            get
            {
                if (!String.IsNullOrEmpty(_mail))
                {
                    return _mail;
                }

                _mail = ConfigurationManager.AppSettings.Get("Mail");
                return _mail;
            }
        }

        private static string _password;
        public static string Password
        {
            get
            {
                if (!String.IsNullOrEmpty(_password))
                {
                    return _password;
                }

                _password = ConfigurationManager.AppSettings.Get("Password");
                return _password;
            }
        }
    }
}
