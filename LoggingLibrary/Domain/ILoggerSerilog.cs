namespace LoggingLibrary.Domain
{
    using System;

    public interface ILoggerSerilog
    {
        void Error(Exception ex, string message);

        void Error(string message);

        void Info(string message);

        void Info(string message, object[] args);

    }
}
