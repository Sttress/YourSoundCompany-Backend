using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace YourSoundCompany.CacheService.Service
{
    public class CacheService : ICacheService
    {
        private readonly ObjectCache _cache;

        public CacheService() 
        {
            _cache = MemoryCache.Default;
        }

        public async Task<T> Get<T>(string key)
        {
            return await Task.Run(() => (T)_cache[key]);
        }

        public async Task Set<T>(string key, T value, TimeSpan cacheExpiration)
        {
            await Task.Run(() => _cache.Set(key, value, DateTimeOffset.Now.Add(cacheExpiration)));
        }

        public async Task Remove(string key)
        {
            await Task.Run(() => _cache.Remove(key));
        }
    }
}
