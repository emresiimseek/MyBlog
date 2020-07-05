using FrameworkCore.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCore.Aspect.PostSharp.CacheAspect
{
    [Serializable]
    public class CacheAspect : MethodInterceptionAspect
    {
        private Type _cacheType;
        private int _cacheByMunite;
        ICacheManager _cacheManager;
        public CacheAspect(Type cacheType, int cacheByMunite = 60)
        {
            _cacheByMunite = cacheByMunite;
            _cacheType = cacheType;
        }
        public override void RuntimeInitialize(MethodBase method)
        { if (typeof(ICacheManager).IsAssignableFrom(_cacheType) == false)
            {
                throw new Exception("Wrong Cache Manager!");
            }
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);

        }
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = string.Format("{0}.{1}.{2}", args.Method.ReflectedType.Namespace,
                args.Method.ReflectedType.Name,
                args.Method.Name);
            var arguments = args.Arguments.ToList();

            var key = string.Format("{0},({1})", methodName, string.Join(",", arguments.Select(x => x != null ? x.ToString() : "<Null>")));
            if (_cacheManager.ısAdd(key))
            {
                args.ReturnValue = _cacheManager.get<object>(key);
            }
            else
            {
                base.OnInvoke(args);
                _cacheManager.add(key, args.ReturnValue, _cacheByMunite);
            }


        }

    }
}
