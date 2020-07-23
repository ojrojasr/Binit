using Binit.Framework;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using DAL;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Logic.Services
{
    /// <summary>
    /// Tenant specific services and overrides.
    /// </summary>
    public class TenantService : Service<Tenant>, ITenantService
    {
        public TenantService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
        IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {

        }

        /// <summary>
        /// Deletes a tenant.
        /// Adds a guid to the end of the path to allow users to create tenants with the same name.
        /// </summary>
        public async override Task DeleteAsync(Guid id)
        {
            var tenant = await base.GetAsync(id);

            tenant.Code += Guid.NewGuid().ToString();

            // Delete tenant.
            await base.DeleteAsync(tenant);
        }

        /// <summary>
        /// Searches tenants comparing the provided term to the name and code.
        /// </summary>
        public Task<List<Tenant>> Search(string term)
        {
            var likeTerm = $"{term}%";
            return base.GetAll()
            .Where(t =>
                EF.Functions.Like(t.Code, likeTerm) ||
                EF.Functions.Like(t.Name, likeTerm))
            .ToListAsync();
        }

        /// <summary>
        /// Returns many tenants by id.
        /// </summary>
        public async Task<List<Tenant>> GetMany(List<string> tenantsIds)
        {
            return await base.GetAll()
            .Where(u => tenantsIds.Contains(u.Id.ToString()))
            .ToListAsync();
        }
    }
}