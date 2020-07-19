using FrameworkCore.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCore.Concrete
{
    public class EntityQueryableRepository<TEntity, TContext> : IQueryableRepository<TEntity>
         where TEntity : class, new()
        where TContext : DbContext, new()
    {
        public TEntity GetEntity(Expression<Func<TEntity, bool>> filter)
        {
            TContext context = new TContext();
            return context.Set<TEntity>().Find(filter);
            //List<User> user = (List<User>)entityQueryableRepository.GetEntity(u => u.Id == 1).ToList();

        }
    }
}
