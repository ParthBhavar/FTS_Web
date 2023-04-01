using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cygnet.CacheManager
{
    /// <summary>
    /// Generic in memory cache manager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericMemoryCacheManager<T> : IGenericCache<T>
    {
        private readonly IMemoryCache _memoryCache;

        public GenericMemoryCacheManager(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        public T Get(string key)
        {
            return  _memoryCache.Get<T>(key);
        }

        public void Set(string key, T value)
        {
            _memoryCache.Set(key, value);
        }

        public List<T> GetList(string key)
        {
            return _memoryCache.Get<List<T>>(key);
        }

        public void SetList(string key, List<T> value)
        {
            _memoryCache.Set(key, value);
        }


        public void Remove(string key) => _memoryCache.Remove(key);
    }
}
