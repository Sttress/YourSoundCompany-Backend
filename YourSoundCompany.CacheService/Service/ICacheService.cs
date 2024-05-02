using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourSoundCompany.CacheService.Service
{
    public interface ICacheService
    {
        Task<T> Get<T>(string key);
        Task Set<T>(string key, T value, TimeSpan cacheExpiration);
        Task Remove(string key);
    }
}
