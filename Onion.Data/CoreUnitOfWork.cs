using Onion.Core.Interfaces;
using Onion.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Onion.Data
{
    /// <summary>
    /// The Core "Unit of Work"
    ///     1) decouples the DbContext and EF from the core services and clients
    ///     2) manages the UoW
    /// </summary>
    /// <remarks>
    /// This class implements the "Unit of Work" pattern in which the "UoW" serves as a facade for querying and saving to the database.
    /// Querying is delegated to "repositories".
    /// Each EF (Data) repository serves as a container dedicated to a particular root entity type such as a <see cref="Contact"/>.
    /// A repository typically exposes "Query" methods for querying and will offer add, update, and delete methods if those features are supported.
    /// The EF (Data) repositories rely on their parent UoW to provide the interface to the data layer (which is the EF DbContext).
    /// Other repositories (Table, Blob) provide the interface for saving changes.
    /// </remarks>
    public class CoreUnitOfWork : ICoreUnitOfWork
    {
        CoreDbContext Context;
        Lazy<Repository<AggregateRoot>> LazyAggregateRoots;
        Lazy<ReadRepository<Entity>> LazyEntities;

        public CoreUnitOfWork(CoreDbContext  context)
        {
            Context = context;
            LazyAggregateRoots = new Lazy<Repository<AggregateRoot>>(() => new Repository<AggregateRoot>(context));
            LazyEntities = new Lazy<ReadRepository<Entity>>(() => new ReadRepository<Entity>(context));
        }

        public IRepository<Core.Models.AggregateRoot> AggregateRoots
        {
            get { return LazyAggregateRoots.Value; }
        }

        public IReadRepository<Entity> Entities
        {
            get { return LazyEntities.Value; }
        }

        public void Commit()
        {
            foreach (var entry in Context.ChangeTracker.Entries())
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        var entity = entry.Entity as Entity;
                        if (entity != null && entry.State == EntityState.Added && entity.Id == Guid.Empty)
                            entity.Id = Guid.NewGuid();

                        var aggregateRoot = entry.Entity as AggregateRoot;
                        if (aggregateRoot != null)
                        {
                            aggregateRoot.LastModifiedOn = DateTime.UtcNow;
                            aggregateRoot.LastModifiedBy = Thread.CurrentPrincipal.Identity.Name;
                        }
                        break;
                }

            Context.SaveChanges();
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
