using System;
using System.Linq;
using System.Net;
using FTS.Configuration;
using FTS.Configuration.Options;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace CacheManager
{
    /// <summary>
    /// Represents Redis connection wrapper implementation
    /// </summary>
    public class RedisConnectionWrapper : IRedisConnectionWrapper, ILocker
    {
        #region Fields

        private readonly RedisServerSettings _redisSettings;

        private readonly object _lock = new object();
        private volatile ConnectionMultiplexer _connection;
        private readonly Lazy<string> _connectionString;
        private volatile RedLockFactory _redisLockFactory;

        #endregion

        #region Ctor

        public RedisConnectionWrapper(IAppConfiguration config)
        {
            _redisSettings = config.RedisServerSettings;
            _connectionString = new Lazy<string>(GetConnectionString);
            _redisLockFactory = CreateRedisLockFactory();
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get connection string to Redis cache from configuration
        /// </summary>
        /// <returns></returns>
        protected string GetConnectionString()
        {
            return _redisSettings.Configuration;
        }

        /// <summary>
        /// Get connection to Redis servers
        /// </summary>
        /// <returns></returns>
        protected ConnectionMultiplexer GetConnection()
        {
            if (_connection != null && _connection.IsConnected) return _connection;

            lock (_lock)
            {
                try
                {
                    if (_connection != null && _connection.IsConnected) return _connection;

                    //Connection disconnected. Disposing connection...
                    _connection?.Dispose();

                    //Creating new instance of Redis Connection
                    _connection = ConnectionMultiplexer.Connect(_connectionString.Value);
                }
                catch (RedisConnectionException ex)
                {
                    return null;
                }
            }
            return _connection;
        }

        /// <summary>
        /// Create instance of RedLock factory
        /// </summary>
        /// <returns>RedLock factory</returns>
        protected RedLockFactory CreateRedisLockFactory()
        {
            //get RedLock endpoints
            var configurationOptions = ConfigurationOptions.Parse(_connectionString.Value);
            var connection = GetEndPoints();
            if (connection != null)
            {
                var redLockEndPoints = GetEndPoints().Select(endPoint => new RedLockEndPoint
                {
                    EndPoint = endPoint,
                    Password = configurationOptions.Password,
                    Ssl = configurationOptions.Ssl,
                    RedisDatabase = configurationOptions.DefaultDatabase,
                    ConfigCheckSeconds = configurationOptions.ConfigCheckSeconds,
                    ConnectionTimeout = configurationOptions.ConnectTimeout,
                    SyncTimeout = configurationOptions.SyncTimeout
                }).ToList();

                //create RedLock factory to use RedLock distributed lock algorithm
                return RedLockFactory.Create(redLockEndPoints);
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Obtain an interactive connection to a database inside Redis
        /// </summary>
        /// <param name="db">Database number; pass null to use the default value</param>
        /// <returns>Redis cache database</returns>
        public IDatabase GetDatabase(int? db = null)
        {
            var connection = GetConnection();
            if (connection != null)
                return GetConnection().GetDatabase(db ?? -1);
            else
                return null;
        }

        /// <summary>
        /// Obtain a configuration API for an individual server
        /// </summary>
        /// <param name="endPoint">The network endpoint</param>
        /// <returns>Redis server</returns>
        public IServer GetServer(EndPoint endPoint)
        {
            var connection = GetConnection();
            if (connection != null)
                return GetConnection().GetServer(endPoint);
            else
                return null;
        }

        /// <summary>
        /// Gets all endpoints defined on the server
        /// </summary>
        /// <returns>Array of endpoints</returns>
        public EndPoint[] GetEndPoints()
        {
            var connection = GetConnection();
            if (connection != null)
                return GetConnection().GetEndPoints();
            else
                return null;
        }

        /// <summary>
        /// Delete all the keys of the database
        /// </summary>
        /// <param name="db">Database number; pass null to use the default value</param>
        public void FlushDatabase(int? db = null)
        {
            var endPoints = GetEndPoints();

            foreach (var endPoint in endPoints)
            {
                GetServer(endPoint).FlushDatabase(db ?? -1);
            }
        }

        /// <summary>
        /// Perform some action with Redis distributed lock
        /// </summary>
        /// <param name="resource">The thing we are locking on</param>
        /// <param name="expirationTime">The time after which the lock will automatically be expired by Redis</param>
        /// <param name="action">Action to be performed with locking</param>
        /// <returns>True if lock was acquired and action was performed; otherwise false</returns>
        public bool PerformActionWithLock(string resource, TimeSpan expirationTime, Action action)
        {
            //use RedLock library
            using (var redisLock = _redisLockFactory.CreateLock(resource, expirationTime))
            {
                //ensure that lock is acquired
                if (!redisLock.IsAcquired)
                    return false;

                //perform action
                action();

                return true;
            }
        }

        /// <summary>
        /// Release all resources associated with this object
        /// </summary>
        public void Dispose()
        {
            //dispose ConnectionMultiplexer
            _connection?.Dispose();

            //dispose RedLock factory
            _redisLockFactory?.Dispose();
        }

        #endregion
    }
}
