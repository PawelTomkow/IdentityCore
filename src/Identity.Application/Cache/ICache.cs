using Identity.Core.Models;

namespace Identity.Application.Cache
{
    public interface ICache
    {
        void Add(CacheObject @object);
        CacheObject Get(string key);
    }
}