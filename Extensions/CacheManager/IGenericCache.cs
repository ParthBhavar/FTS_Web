using System;
using System.Collections.Generic;
using System.Text;

namespace Cygnet.CacheManager
{
    public interface IGenericCache<T>
    {
        T Get(string key);
        void Set(string key, T value);

        List<T> GetList(string key);

        void SetList(string key, List<T> value);


        void Remove(string key);
    }
}
