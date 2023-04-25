using BuildingReport.Business.Logging.Abstract;
using Microsoft.Extensions.Logging;
using System;

namespace BuildingReport.Business.Logging.Concrete
{
    public class LoggerManager : ILoggerManager
    {
        private readonly ILogger<LoggerManager> _logger;

        public LoggerManager(ILogger<LoggerManager> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }

        public void LogError(string message)
        {
            _logger.LogError(message);
        }

        public void LogException(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}
