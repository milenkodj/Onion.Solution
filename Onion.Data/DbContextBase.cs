using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Data
{
    /// <summary>
    /// Base class to be extended by the bounded context DbContexts sharing the same database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbContextBase<T> : DbContext where T: DbContext
    {
        static DbContextBase()
        {
            Database.SetInitializer<T>(null);
        }

        /// <summary>
        /// Ensure all contexts share the same database (connection string)
        /// </summary>
        protected DbContextBase(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

    }
}
