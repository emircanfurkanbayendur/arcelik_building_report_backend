using Microsoft.Extensions.Logging;

namespace BuildingReport.Business.Logging.Abstract
{
    public interface ILoggerManager
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogException(Exception ex);
    }
}