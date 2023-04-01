using System;

namespace Logger
{
    public interface ILogging
    {
        #region Properties

        /// <summary>
        ///     Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Log(string message);

        /// <summary>
        ///     Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(string message);

        /// <summary>
        ///     Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tenantId"></param>
        /// <param name="ex">The ex.</param>
        void Error(string message,Exception ex);

        #endregion
    }
}
