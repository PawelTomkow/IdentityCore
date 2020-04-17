using System;
using Identity.Persistence.Models;
using Identity.Persistence.Settings;
using Microsoft.Extensions.Caching.Memory;

namespace Identity.Persistence.Cache
{
    public class MemoryCache : ICache
    {
        private readonly IMemoryCache _cache;
        private readonly CacheSettings _settings;

        public MemoryCache(IMemoryCache cache, CacheSettings settings)
        {
            _cache = cache;
            _settings = settings;
        }
        public void Add(CacheObject @object)
        {
            _cache.Set(@object.Key, @object, TimeSpan.FromSeconds(_settings.LifeTime));
        }

        public CacheObject Get(string key)
        {
            return _cache.Get<CacheObject>(key);
        }
    }
}