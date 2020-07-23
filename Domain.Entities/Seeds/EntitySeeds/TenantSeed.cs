using Binit.Framework.Constants.Authentication;
using Domain.Entities.Model;
using System;

namespace Domain.Entities.Seeds.EntitySeeds
{
    public class TenantSeed : SeedBase
    {
        protected override void Execute()
        {

            var tenantsToAdd = new Tenant[1] {
                new Tenant {
                    Id = Tenants.MasterId,
                    CreatedDate = DateTime.Now,
                    Code = "master",
                    Name = "Master",
                    Description = "Master tenant"
                }
            };

            this.AddedElements.Add(typeof(Tenant), tenantsToAdd);
            this.modelBuilder.Entity<Tenant>().HasData(tenantsToAdd);
        }
    }
}
