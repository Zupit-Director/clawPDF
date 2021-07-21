using NLog;

namespace zupit.zupitPDF.Shared.Helper.Logging
{
    internal interface ILogger
    {
        void ChangeLogLevel(LogLevel logLevel);

        string GetLogPath();
    }
}