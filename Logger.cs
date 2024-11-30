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
        public enum LogType
        {
            INFO,
            WARNING,
            ERROR
        }

        struct LogEntry
        {
            public LogType type;
            public string message;
        }

        public static string CurrentDateTimeToString()
        {
            //return DateTime.Now.ToString("%d-%b-%Y %H:%M:%S");
            return DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
        }

        public static void Info(string message)
        {
            LogMessage(message, LogType.INFO);
        }

        public static void Error(string message)
        {
            LogMessage(message, LogType.ERROR);
        }

        public static void Warn(string message)
        {
            LogMessage(message, LogType.WARNING);
        }



        private static void LogMessage(string message, LogType logType)
        {
            LogEntry logEntry;
            logEntry.type = logType;
            logEntry.message = $"{logType}: [{CurrentDateTimeToString()}]: {message}";
            Debug.WriteLine(logEntry.message);
        }
    }
}
