using System;
using System.IO;

namespace FileDataOperations
{
    public class Logger
    {
        private static Logger _logger;
        private static object _padlock = new object();
        private static StreamWriter streamWriterFile;

        private static object streamLock = new object();
        string currentDate = string.Empty;
        string logFileName = string.Empty;

        private static readonly string logfolder = @"C:\BioLogs";
        private Logger()
        {
            Directory.CreateDirectory(logfolder);
        }

        private static string LoggerFileName { get; } = string.Format(@"{0}\{1}",logfolder, "logger{0}.log");

        public static event EventHandler<Exception> LoggedExceptionEvent;

        public static Logger GetLogger()
        {
            if (_logger == null)
            {
                lock (_padlock)
                {
                    if (_logger == null)
                    {
                        _logger = new Logger();
                    }
                }
            }

            return _logger;
        }

        public void Log(string message)
        {
            if (!this.currentDate.Equals(DateTime.Now.ToShortDateString()))
            {
                this.InitCurrentLogFilePath();
            }

            try
            {
                var logMessage = DateTime.Now.ToLongTimeString() + " --- " + message;
                lock (streamLock)
                {
                    using (var file = new FileStream(this.logFileName, FileMode.Append))
                    {
                        using (streamWriterFile = new StreamWriter(file))
                        {
                            streamWriterFile.WriteLine(logMessage);
                            streamWriterFile.Flush();
                        }
                    }
                }
            }
            catch
            {
                // нельзхя чтобы все умерло изза кривого логгера...
            }
        }

        private void InitCurrentLogFilePath()
        {
            int i = 0;
            string newLogFileName = string.Empty;

            do
            {
                i++;
                newLogFileName = string.Format(LoggerFileName, i);
            } while (File.Exists(newLogFileName));

            this.logFileName = newLogFileName;
            this.currentDate = DateTime.Now.ToShortDateString();
        }

        public void Log(Exception exception)
        {
            this.Log(exception.ToString());
            LoggedExceptionEvent?.Invoke(this, exception);
        }
    }
}
