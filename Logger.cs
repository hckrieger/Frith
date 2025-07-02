using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Frith
{
	public static class Logger
	{
		enum LogType
		{
			LOG_INFO,
			LOG_WARNING,
			LOG_ERROR
		};


		private struct LogEntry
		{
			public LogType type;
			public string message;
		}

		private static string CurrentDateTimeToString()
		{
			var dateTime = DateTime.Now;

			return dateTime.ToString("dd-MMM-yyyy HH:mm:ss");
		}

		public static void Info(string message)
		{
			LogEntry logEntry;
			logEntry.type = LogType.LOG_INFO;
			logEntry.message = "INFO: [" + CurrentDateTimeToString() + "]: " + message;
			Debug.WriteLine(logEntry.message);

		}

		public static void Error(string message)
		{
			LogEntry logEntry;
			logEntry.type = LogType.LOG_ERROR;
			logEntry.message = "ERROR: [" + CurrentDateTimeToString() + "]: " + message;
			Debug.WriteLine(logEntry.message);

		}

		public static void Warn(string message)
		{
			LogEntry logEntry;
			logEntry.type = LogType.LOG_INFO;
			logEntry.message = "WARNING: [" + CurrentDateTimeToString() + "]: " + message;
			Debug.WriteLine(logEntry.message);

		}
	}
}
