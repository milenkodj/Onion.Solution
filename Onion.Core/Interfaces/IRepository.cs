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
    public interface IRepository<T> : IReadRepository<T> where T : Entity
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
