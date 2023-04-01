using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    /// <summary>
    /// Creating DI ILoggerFactory for NLog
    /// </summary>
    public interface ILoggerFactory
    {
        ILogging CreateLogger<T>() where T : class;
    }
}
