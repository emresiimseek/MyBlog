using FrameworkCore.Abstract;
using FrameworkCore.CrossCuttingConcerns.Security;
using MyBlog.EntityFramework.Abstract;
using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FrameworkCore.Concrete
{
    public class EntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
          where TEntity : class, new()
         where TContext : DbContext, new()
    {
        private Identity identity;
        public EntityRepositoryBase()
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated != false)
            {
                identity = (Identity)Thread.CurrentPrincipal.Identity;
            }
            identity = new Identity { Id = -1 };
        }

        public int Add(TEntity entity)
        {
            if (entity is IEntity)
            {
                IEntity myEntity = entity as IEntity;
                DateTime dateTime = DateTime.Now;
                myEntity.CreatedOn = dateTime;
                myEntity.ModifiedOn = dateTime;
                myEntity.ModifiedUsername = identity.Id.ToString();
            }
            TContext context = SingletonContext<TContext>.CreateContext();
            context.Configuration.LazyLoadingEnabled = false;
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            return context.SaveChanges();
        }
        public int Delete(TEntity entity)
        {
            TContext context = SingletonContext<TContext>.CreateContext();
            var deleteEntity = context.Entry(entity);
            deleteEntity.State = EntityState.Deleted;
            return context.SaveChanges();
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            TContext context = SingletonContext<TContext>.CreateContext();
            return context.Set<TEntity>().SingleOrDefault(filter);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {

            TContext context = SingletonContext<TContext>.CreateContext();
            return filter == null ?
                    context.Set<TEntity>().ToList() :
                    context.Set<TEntity>().Where(filter).ToList();
            
        }

        public void Update(TEntity entity)
        {
            if (entity is IEntity)
            {
                IEntity myEntity = entity as IEntity;
                myEntity.ModifiedOn = DateTime.Now;
                myEntity.ModifiedUsername = identity.Id.ToString();
            }

            TContext context = SingletonContext<TContext>.CreateContext();
            var uptadedEntity = context.Entry(entity);
            uptadedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
