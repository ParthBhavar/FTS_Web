using Cygnet.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cygnet.CacheManager
{
    /// <summary>
    /// Generic redis cache manager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRedisCacheManager<T> : IGenericCache<T>
    {
        private ConnectionMultiplexer _connection;
        private readonly IAppConfiguration _appConfiguration;
        private readonly RedisDictionary<string, T> _redisDictionary;
        private readonly RedisDictionary<string, List<T>> _redisDictionaryList;
        public GenericRedisCacheManager(IAppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
            if (_connection == null)
            {
                _connection = ConnectionMultiplexer.Connect(_appConfiguration.RedisSettings.Configuration);
            }

            _redisDictionary = new RedisDictionary<string, T>("CygnetCache", _connection);
            _redisDictionaryList = new RedisDictionary<string, List<T>>("CygnetCacheList", _connection);
        }

        public T Get(string key)
        {
            _redisDictionary.TryGetValue(key, out var value);
            return value;
        }

        public void Set(string key, T value)
        {
            if (_redisDictionary.ContainsKey(key))
            {
                Remove(key);
            }
            _redisDictionary.TryAdd(key, value);
        }

        public List<T> GetList(string key)
        {
            _redisDictionaryList.TryGetValue(key, out var value);
            return value;
        }

        public void SetList(string key, List<T> value)
        {
            if (_redisDictionaryList.ContainsKey(key))
            {
                RemoveList(key);
            }
            _redisDictionaryList.TryAdd(key, value);
        }

        public void Remove(string key) => _redisDictionary.Remove(key);

        public void RemoveList(string key) => _redisDictionaryList.Remove(key);
    }
    
}
