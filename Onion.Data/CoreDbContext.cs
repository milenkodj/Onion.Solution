using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Data
{
    public class CoreDbContext : DbContextBase<CoreDbContext>
    {
        //public CoreDbContext()
            //: this(new SettingsManager())
        //{ }

        //public CoreDbContext(ISettingsManager settingsManager)
            //: base(settingsManager.GetSetting(Constants.Database.ConnectionString))
        public CoreDbContext()
            : base("onion.database")
        { }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Configurations.Add(new ClientConfiguration());
        //    modelBuilder.Configurations.Add(new ContactConfiguration());
        //    modelBuilder.Configurations.Add(new DeviceConfiguration());
        //}
    }
}
