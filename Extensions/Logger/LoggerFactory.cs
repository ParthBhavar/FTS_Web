namespace Logger
{
    /// <summary>
    /// Create DI logger factory for Nlog for passing type in parameter
    /// </summary>
    public class LoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// Create the logger.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ILogging CreateLogger<T>() where T : class
        {
            return new Logging(typeof(T).Name);
        }
    }
}
