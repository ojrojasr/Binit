using Binit.Framework.Constants.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Domain.Entities.Seeds.EntitySeeds
{
    public class RoleSeed : SeedBase
    {
        protected override void Execute()
        {
            IdentityRole<Guid>[] elementsToAdd = Roles.GetAllRoles(includeSuperAdmin: true)
                .Select(role =>
                {
                    var guidId = Roles.GetId(role);
                    return new IdentityRole<Guid>()
                    {
                        Id = guidId,
                        Name = role,
                        NormalizedName = role.ToUpper()
                    };
                }).ToArray();

            this.AddedElements.Add(typeof(IdentityRole<Guid>), elementsToAdd);
            this.modelBuilder.Entity<IdentityRole<Guid>>().HasData(elementsToAdd);
        }
    }
}
