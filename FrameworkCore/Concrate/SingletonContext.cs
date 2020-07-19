using FrameworkCore.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCore.Concrete
{
    public class SingletonContext<TContext> where TContext : DbContext, new()
    {
        private static TContext _db;
        private static object _lockSync = new object();
        public SingletonContext()
        {

        }


        public static TContext CreateContext()
        {
            if (_db == null)
            {
                lock (_lockSync)
                {
                    _db = new TContext();
                }

            }
            return _db;

        }
    }
}
