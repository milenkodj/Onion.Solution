using Onion.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Core.Interfaces
{
    /// <summary>
    /// Unit of work pattern interface for use in services. With core bounded context scope.
    /// </summary>
    public interface ICoreUnitOfWork : IDisposable
    {
        // Repositories
        IRepository<AggregateRoot> AggregateRoots { get; }
        IReadRepository<Entity> Entities { get; }
        // ...

        // Save pending changes to the data store.
        void Commit();

    }
}
