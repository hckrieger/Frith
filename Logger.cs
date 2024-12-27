using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frith
{
    public static class Logger
    {
        private enum LogType
        {
            Info,
            Warning,
            Error
        }

        private struct LogEntry
        {
            public LogType Type;
            public string Message;
        }

        private static string CurrentDateTimeToString()
        {
            //return DateTime.Now.ToString("%d-%b-%Y %H:%M:%S");
            return DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
        }

        public static void Info(string message)
        {
            LogMessage(message, LogType.Info);
        }

        public static void Error(string message)
        {
            LogMessage(message, LogType.Error);
        }

        public static void Warn(string message)
        {
            LogMessage(message, LogType.Warning);
        }



        private static void LogMessage(string message, LogType logType)
        {
            LogEntry logEntry;
            logEntry.Type = logType;
            logEntry.Message = $"{logType}: [{CurrentDateTimeToString()}]: {message}";
            Debug.WriteLine(logEntry.Message);
        }
    }
}
