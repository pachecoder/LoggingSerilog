namespace LoggingLibrary.Logging
{
    using LoggingLibrary.Domain;
    using Serilog;
    using System;

    public class LoggerSerilog : ILoggerSerilog
    {
        public LoggerSerilog()
        {
        }

        public void Error(string message)
        {
            Log.Error(message);
        }

        public void Error(Exception ex, string message)
        {
            Log.Error(ex, message);
        }

        public void Info(string message)
        {
            Log.Information(message);
        }

        public void Info(string message, object[] args)
        {
            Log.Information(message, args);
        }
    }
}
