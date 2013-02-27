using Onion.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Core.Interfaces
{
    /// <summary>
    /// Repository interface for aggregate roots.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadRepository<T> : IDisposable where T : Entity
    {
        /// <summary>
        /// Query for aggregate roots
        /// </summary>
        /// <param name="includes">specify which child entities to include with aggregate roots</param>
        /// <returns></returns>
        IQueryable<T> Get(params Expression<Func<T, object>>[] includes);
    }
}
