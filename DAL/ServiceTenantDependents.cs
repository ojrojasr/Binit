using Binit.Framework;
using Binit.Framework.Constants.DAL;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.DAL.ServiceTenantDependents;

namespace DAL
{
    /// <summary>
    /// Generic service class.
    /// </summary>
    public class ServiceTenantDependents<TEntity> : Service<TEntity>, IServiceTenantDependent<TEntity>
        where TEntity : class, IEntity, ITenantDependent
    {

        #region Constructor

        public ServiceTenantDependents(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
        IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns an IQueriable<IEntity> representing all the entities records accessible to this user's tenants.
        /// </summary>
        public override IQueryable<TEntity> GetAll(bool asNoTracking = false)
        {
            try
            {
                var entities = base.GetAll()
                    .Where(CanAccessByTenant());

                if (asNoTracking)
                    entities = entities.AsNoTracking();

                return entities;
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }


        /// <summary>
        /// Asynchronously inserts a new IEntity object into the database.
        /// Validates the user's permission to execute the operation.
        /// Records the operation into the audiTEntity database.
        /// </summary>
        public override async Task CreateAsync(TEntity entity)
        {
            try
            {
                var tenantId = this.operationContext.GetUserTenantId();
                if (tenantId.HasValue)
                {
                    entity.TenantId = tenantId.Value;
                }

                await base.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Inserts a new IEntity object into the database.
        /// Validates the user's permission to execute the operation.
        /// Records the operation into the audiTEntity database.
        /// </summary>
        public override void Create(TEntity entity)
        {
            try
            {
                var tenantId = this.operationContext.GetUserTenantId();
                if (tenantId.HasValue)
                {
                    entity.TenantId = tenantId.Value;
                }

                base.Create(entity);
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Updates an existent IEntity object from the database.
        /// Validates the user's permission to execute the operation.
        /// Records the operation into the audiTEntity database.
        /// </summary>
        public override void Update(TEntity entity)
        {
            try
            {
                var tenantId = this.operationContext.GetUserTenantId();
                if (tenantId.HasValue)
                {
                    entity.TenantId = tenantId.Value;
                }

                base.Update(entity);
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Asynchronously updates an existent IEntity object from the database.
        /// Validates the user's permission to execute the operation.
        /// Records the operation into the audiTEntity database.
        /// </summary>
        public override async Task UpdateAsync(TEntity entity)
        {
            try
            {
                var tenantId = this.operationContext.GetUserTenantId();
                if (tenantId.HasValue)
                {
                    entity.TenantId = tenantId.Value;
                }

                await base.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }


        /// <summary>
        /// Validates if the current user can access the provided entity according to their tenants.
        /// Retrieves the user's tenants from the current operation context.
        /// </summary>
        public bool CanAccessByTenant(TEntity entity)
        {
            var userTenantId = this.operationContext.GetUserTenantId();

            if (userTenantId == null || entity.TenantId != userTenantId)
                return false;
            return true;

        }

        /// <summary>
        /// Returns an expression (for Linq operations) that validates if the current user can access the provided entity according to their tenants.
        /// Retrieves the user's tenants from the current operation context.
        /// Eg: `base.GetAll().Where(CanAccessByTenant());`
        /// </summary>
        public Expression<Func<TEntity, bool>> CanAccessByTenant()
        {
            var userTenantId = this.operationContext.GetUserTenantId();

            return e => e.TenantId == userTenantId;
        }

        #endregion

        #region Private Methods


        /// <summary>
        /// Executes the generic Service validation and checks if the user has the required tenants to read/write the entity.
        /// </summary>
        protected override void CanAccess(TEntity entity, DALOperations operation)
        {
            if (!this.CanAccessByTenant(entity))
                throw this.exceptionManager.Handle(new UnauthorizedException(this.localizer[Lang.AccessDeniedEx]));

            base.CanAccess(entity, operation);
        }

        #endregion
    }
}