using System;
using Base.Api.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using Base.Api.Helper;
using sg.com.titansoft.TiUtil.Debug;
using sg.com.titansoft.TiUtil.Threading;

namespace Base.Api.Model
{
    public abstract class CacheBase<T>
    {
        private readonly object _lockKey;
        private EnumCache cacheKey { get; set; }
        private MemoryCache _cache = MemoryCache.Default;

        protected CacheBase(EnumCache cacheKey)
        {
            this.cacheKey = cacheKey;
            _lockKey = new object();
        }

        public bool Contains(string key)
        {
            return _cache.Contains($"{cacheKey}_{key}");
        }

        public T Get(string key)
        {
            if (Contains(key))
            {
                return (T)_cache[$"{cacheKey}_{key}"];
            }

            lock (_lockKey)
            {
                if (_cache.Contains($"{cacheKey}_{key}"))
                {
                    return (T)_cache[$"{cacheKey}_{key}"];
                }
                var result = ReloadFromDb(key);
                _cache.Set($"{cacheKey}_{key}", result, GetItemPolicy());
                return result;
            }
        }

        public void ClearAll()
        {
            lock (_lockKey)
            {
                if (_cache == null) { return; }
                List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
                foreach (string cacheKey in cacheKeys.Where(x=>x.Contains($"{cacheKey}_")))
                {
                    MemoryCache.Default.Remove(cacheKey);
                }
            }
        }

        public void AddOrUpdate(string key, T item)
        {
            _cache.Set($"{cacheKey}_{key}", item, GetItemPolicy());
        }

        protected abstract T ReloadFromDb(string key);

        protected abstract CacheItemPolicy GetItemPolicy();

    }
}