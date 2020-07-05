using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCore.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T get<T>(string key);
        void add(string key, object data, int cacheTime);
        bool ısAdd(string key);
        void Remove(string key);
        void RemoveByPAttern(string pattern);
        void Clear();
    }
}
