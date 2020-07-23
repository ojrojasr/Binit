using System;
using System.Linq.Expressions;

namespace Binit.Framework.Interfaces.DAL
{
    /// <summary>
    /// Tenant dependents generic service interface.
    /// </summary>
    public interface IServiceTenantDependent<TEntity> : IService<TEntity>
    where TEntity : class, IEntity, ITenantDependent
    {
        /// <summary>
        /// Validates if the current user can access the provided entity according to their tenants.
        /// </summary>
        bool CanAccessByTenant(TEntity entity);

        /// <summary>
        /// Returns an expression (for Linq operations) that validates if the current user can access the provided entity according to their tenants.
        /// Eg: `base.GetAll().Where(CanAccessByTenant());`
        /// </summary>
        Expression<Func<TEntity, bool>> CanAccessByTenant();
    }
}