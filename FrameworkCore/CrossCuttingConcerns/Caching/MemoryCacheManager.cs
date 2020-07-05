using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace FrameworkCore.CrossCuttingConcerns.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        protected ObjectCache cache { get { return MemoryCache.Default; } }

        public void add(string key, object data, int cacheTime)
        {
            if (data == null)
            {
                return;
            }
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime) };
            cache.Add(new CacheItem(key, data), policy);
        }

        public void Clear()
        {
            foreach (var item in cache)
            {
                Remove(item.Key);
            }
        }

        public T get<T>(string key)
        {
            return (T)cache[key];
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }

        public void RemoveByPAttern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keyToRemove = cache.Where(d => regex.IsMatch(d.Key)).Select(d => d.Key).ToList();
            foreach (var key in keyToRemove)
            {
                Remove(key);
            }
        }

        public bool ısAdd(string key)
        {
            return cache.Contains(key);
        }
    }
}
