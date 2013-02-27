using Onion.Core.Interfaces;
using Onion.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Data
{
    /// <summary>
    /// Generic EntityRepository<T> implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : ReadRepository<T>, IRepository<T> where T : Entity
    {
        protected DbSet<T> Set;

        public Repository(DbContext context) : base(context)
        {
            Set = context.Set<T>();
        }

        public override IQueryable<T> Get(params Expression<Func<T, object>>[] includeProperties)
        {
            if (includeProperties.Length > 0)
            {
                IQueryable<T> query = Set;
                foreach (var property in includeProperties)
                    query = query.Include(property);
                return query;
            }
            return Set;
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
                dbEntityEntry.State = EntityState.Added;
            else
                Set.Add(entity);
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
                Set.Attach(entity);

            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
                dbEntityEntry.State = EntityState.Deleted;
            else
            {
                Set.Attach(entity);
                Set.Remove(entity);
            }
        }
    }
}
