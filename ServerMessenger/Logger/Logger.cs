using System;
using System.IO;
using System.Threading;

namespace LoggerWorker
{
    public class Logger : ILogger
    {
        public event EventHandler<string> RaiseLogger;
        private LogLevel _level;
        private string _logsFolder;

        private DateTime? _lastSessionDate;
        private string _sessionId;
        private string SessionId
        {
            get
            {
                var date = DateTime.Now;
                if (_lastSessionDate == null)
                {
                    _lastSessionDate = date;
                }
                var timeOver = (date - (DateTime)_lastSessionDate).TotalMinutes > 120;
                if (_sessionId == null || timeOver)
                {
                    _lastSessionDate = date;
                    _sessionId = date.Year + "_" + date.Month + "_" + date.Day + "_" + (int)((date - date.Date).TotalSeconds);
                }
                return _sessionId;
            }
        }

        public Logger(LogLevel level, string path)
        {
            _level = level;
            _logsFolder = path;
        }

        public void Write(LogLevel level, string message, Exception ex = null)
        {
            var log = new Log
            {
                Exception = ex,
                Message = message,
                LogLevel = level,
                ThreadId = Thread.CurrentThread.ManagedThreadId,
            };

            Write(log);
        }

        private readonly object _lockObject = new object();
        private void Write(Log log)
        {
            if (log.LogLevel <= _level)
            {
                CheckExistsFolder(_logsFolder);
                var logsPath = Path.Combine(_logsFolder, "Log_" + SessionId + ".log");

                lock (_lockObject)
                {
                    File.AppendAllText(logsPath, log.ToString());
                    RaiseLogger?.Invoke(this, log.ToString());
                }
            }
        }

        public void CheckExistsFolder(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
    }
}
