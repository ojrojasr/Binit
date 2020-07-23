using Domain.Entities.Seeds.EntitySeeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Seeds
{
    public class SeedManager
    {
        private ModelBuilder modelBuilder { get; set; }
        private DatabaseFacade database { get; set; }

        public SeedManager(ModelBuilder modelBuilder, DatabaseFacade database)
        {
            this.modelBuilder = modelBuilder;
            this.database = database;
        }

        public void ExecuteSeed()
        {
            //For Debugging the seed
            //if (!System.Diagnostics.Debugger.IsAttached)
            //    System.Diagnostics.Debugger.Launch();

            Dictionary<Type, IList<object>> addedElements = new Dictionary<Type, IList<object>>();
            foreach (var seedClass in GetSeedFixture())
            {
                seedClass.modelBuilder = modelBuilder;
                seedClass.AddedElements = addedElements;

                seedClass.Process();
            }
        }

        public void ExecuteIgniteDbObjectSeed()
        {
            try
            {
                database.ExecuteSqlRaw(SQLObjectSeeds.SQLObjectSeeds.SpCreateCategory);
                database.ExecuteSqlRaw(SQLObjectSeeds.SQLObjectSeeds.ViewGetProductFeaturesCount);
            }
            catch (Exception)
            {

            }
        }

        private SeedBase[] GetSeedFixture()
        {
            return new SeedBase[3] {
                new RoleSeed(),
                new TenantSeed(),
                new UserSeed()
            };
        }
    }
}
