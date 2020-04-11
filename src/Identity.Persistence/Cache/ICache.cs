using Identity.Persistence.Models;

namespace Identity.Persistence.Cache
{
    public interface ICache
    {
        void Add(CacheObject @object);
        CacheObject Get(string key);
    }
}