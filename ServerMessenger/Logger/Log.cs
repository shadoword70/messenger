using System;

namespace LoggerWorker
{
    public class Log
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public LogLevel LogLevel { get; set; }
        public int? ExecuteTime { get; set; }
        public int ThreadId { get; set; }


        public override string ToString()
        {
            if (Exception == null)
            {
                return String.Format("{0} | {1}\t| Thread: {2}\t| {3}\t| {4}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    LogLevel, ThreadId, Message, ExecuteTime);
            }

            if (Exception.InnerException == null)
            {
                return String.Format("{0} | {1}\t| Thread: {2}\t| {3}\t| {4}\r\nExceptionMessage: {5}\r\nExceptionStackTrace: {6}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    LogLevel, ThreadId, Message, ExecuteTime, Exception.Message, Exception.StackTrace);
            }

            return String.Format("{0} | {1}\t| Thread: {2}\t| {3}\t| {4}\r\nExceptionMessage: {5}\r\nExceptionStackTrace: {6}\r\nInnerException: {7}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                LogLevel, ThreadId, Message, ExecuteTime, Exception.Message, Exception.StackTrace, Exception.InnerException);
        }
    }
}
