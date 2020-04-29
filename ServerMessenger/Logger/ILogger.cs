using System;

namespace LoggerWorker
{
    public interface ILogger
    {
        void Write(LogLevel level, string message, Exception ex = null);
        event EventHandler<string> RaiseLogger;
    }
}
