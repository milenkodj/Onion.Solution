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
    public class ReadRepository<T> : IReadRepository<T> where T : Entity
    {
        protected DbContext Context;

        public ReadRepository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            Context = context;
        }

        public virtual IQueryable<T> Get(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>();
            if (includeProperties.Length > 0)
            {
                foreach (var property in includeProperties)
                    query = query.Include(property);
            }
            return query.AsNoTracking();
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                }
            }
        }
        #endregion
    }
}
