using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    /// <summary>
    /// TenantService specific method definitions.
    /// </summary>
    public interface ITenantService : IService<Tenant>
    {
        /// <summary>
        /// Returns tenants that match the provided term.
        /// </summary>
        Task<List<Tenant>> Search(string term);

        /// <summary>
        /// Returns many tenants by id.
        /// </summary>        
        Task<List<Tenant>> GetMany(List<string> tenantsIds);
    }
}
