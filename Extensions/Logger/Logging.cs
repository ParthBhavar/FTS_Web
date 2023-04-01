using System;
using NLog;
namespace Logger
{
    public class Logging : ILogging
    {
        #region Declaration

        /// <summary>
        ///     The logger
        /// </summary>
        private readonly NLog.Logger _logger;

        /// <summary>
        ///     The logger name
        /// </summary>
        private readonly string _typeName;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NLogLogger"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public Logging(string type)
        {
            _typeName = type;
            _logger = LogManager.GetLogger("Logger");
        }

        #endregion

        #region public methods        
        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(string message)
        {
            _logger.Debug(string.Format("{0} {1}", _typeName, message));
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void Error(string message, Exception ex)
        {
            string errorMessage = $"{message}, ExceptionMessage => {ex.Message}, StackTrace: ${ex.StackTrace}";
            var logEventInfo = new LogEventInfo(LogLevel.Error, "Logger", errorMessage);
            logEventInfo.Properties["Type"] = _typeName;
            logEventInfo.Exception = ex;
            _logger.Log(typeof(Logging), logEventInfo);
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            var logEventInfo = new LogEventInfo(LogLevel.Info, "Logger", message);
            logEventInfo.Properties["Type"] = _typeName;
            _logger.Log(typeof(Logging), logEventInfo);
        }
        #endregion
    }
}
