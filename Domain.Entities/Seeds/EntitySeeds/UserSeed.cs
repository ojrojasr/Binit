using Binit.Framework.Constants.Authentication;
using Binit.Framework.Constants.DAL;
using Domain.Entities.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Domain.Entities.Seeds.EntitySeeds
{
    public class UserSeed : SeedBase
    {
        protected override void Execute()
        {
            var hasher = new PasswordHasher<BackOfficeUser>();
            var adminGuid = Guid.Parse("933EC209-4AC0-4AAE-9A05-AEEA53384DF9");
            var adminUser = "administrador@binit.com.ar";
            var adminPass = "qweQWE123!#";

            var frontHasher = new PasswordHasher<FrontUser>();
            var frontGuid = Guid.Parse("8E0072FD-D3D8-4324-B3B8-AD90FFAB5552");
            var frontUser = "front@binit.com.ar";
            var frontPass = "qweQWE123!#";

            BackOfficeUser[] backOfficeUsersToAdd = new BackOfficeUser[1]
            {
                new BackOfficeUser
                {
                    CreatedDate = DateTime.Now,
                    Email = adminUser,
                    EmailConfirmed = true,
                    Id = adminGuid,
                    LastName = "Binit",
                    Name = "Administrator",
                    NormalizedEmail = adminUser.ToUpper(),
                    NormalizedUserName = adminUser.ToUpper(),
                    PasswordHash = hasher.HashPassword(null, adminPass),
                    SecurityStamp = string.Empty,
                    UserName = adminUser,
                    TenantId = Tenants.MasterId,
                    UserType = UserTypes.BackofficeUser,
                    JobTitle = "Ignite Admin"
                }
            };

            this.AddedElements.Add(typeof(BackOfficeUser), backOfficeUsersToAdd);
            this.modelBuilder.Entity<BackOfficeUser>().HasData(backOfficeUsersToAdd);

            FrontUser[] frontUsersToAdd = new FrontUser[1]
            {
                new FrontUser
                {
                    CreatedDate = DateTime.Now,
                    Email = frontUser,
                    EmailConfirmed = true,
                    Id = frontGuid,
                    LastName = "Binit",
                    Name = "Front Administrator",
                    NormalizedEmail = frontUser.ToUpper(),
                    NormalizedUserName = frontUser.ToUpper(),
                    PasswordHash = frontHasher.HashPassword(null, frontPass),
                    SecurityStamp = string.Empty,
                    UserName = frontUser,
                    TenantId = Tenants.MasterId,
                    UserType = UserTypes.FrontUser,
                    Birthdate = new DateTime(1990,1,1)
                }
            };

            this.AddedElements.Add(typeof(FrontUser), frontUsersToAdd);
            this.modelBuilder.Entity<FrontUser>().HasData(frontUsersToAdd);


            // Add roles.
            var roles = this.AddedElements[typeof(IdentityRole<Guid>)].ToList().Cast<IdentityRole<Guid>>().ToList();

            var roleAdmin = roles.FirstOrDefault(r => r.Name == Roles.BackofficeSuperAdministrator);
            if (roleAdmin != null)
            {
                IdentityUserRole<Guid>[] userRolesToAdd = new IdentityUserRole<Guid>[2]
                {
                    new IdentityUserRole<Guid>
                    {
                        RoleId = Roles.GetId(Roles.BackofficeSuperAdministrator),
                        UserId = adminGuid
                    },
                    new IdentityUserRole<Guid>
                    {
                        RoleId = Roles.GetId(Roles.FrontSuperAdministrator),
                        UserId = frontGuid
                    }
                };
                this.AddedElements.Add(typeof(IdentityUserRole<Guid>), userRolesToAdd);
                this.modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(userRolesToAdd);
            }

            // Add claims.
            IdentityUserClaim<Guid>[] userClaimsToAdd = new IdentityUserClaim<Guid>[2]
            {
                new IdentityUserClaim<Guid>
                {
                    Id = 1,
                    ClaimType = CustomClaimTypes.Tenant,
                    ClaimValue = Tenants.MasterId.ToString(),
                    UserId = adminGuid
                },
                new IdentityUserClaim<Guid>
                {
                    Id = 2,
                    ClaimType = CustomClaimTypes.Tenant,
                    ClaimValue = Tenants.MasterId.ToString(),
                    UserId = frontGuid
                }
            };

            this.AddedElements.Add(typeof(IdentityUserClaim<Guid>), userClaimsToAdd);
            this.modelBuilder.Entity<IdentityUserClaim<Guid>>().HasData(userClaimsToAdd);
        }
    }
}