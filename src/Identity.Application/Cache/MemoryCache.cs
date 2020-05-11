using System;
using Identity.Application.Settings;
using Identity.Core.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Identity.Application.Cache
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