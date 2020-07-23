using Binit.Framework;
using Binit.Framework.Constants.DAL;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using Domain.Entities.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.DAL.Service;

namespace DAL
{
    /// <summary>
    /// Generic service class.
    /// </summary>
    public class Service<TEntity> : IService<TEntity> where TEntity : class, IEntity
    {
        #region Properties

        protected readonly IRepository<TEntity> repository;
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IExceptionManager exceptionManager;
        protected readonly IOperationContext operationContext;
        protected readonly IStringLocalizer<SharedResources> localizer;

        protected ILogger logger;

        #endregion

        #region Constructor

        public Service(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext, IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            this.exceptionManager = exceptionManager;
            this.logger = logger;
            this.operationContext = operationContext;
            this.localizer = localizer;
            this.unitOfWork = unitOfWork;
            this.repository = unitOfWork.GetRepository<TEntity>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves a single IEntity object from the Database by id.
        /// Validates if the record was deleted and the user's permission to execute the operation.
        /// Records the operation into the audit database.
        /// </summary>
        public virtual TEntity Get(Guid id)
        {
            TEntity entity;
            try
            {
                entity = repository.Get(id);

                this.CanAccess(entity, DALOperations.Get);

                return entity;
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Returns an IQueriable<IEntity> representing all rows of an IEntity table.
        /// Records the operation into the audiTEntity database.
        /// </summary>
        public virtual IQueryable<TEntity> GetAll(bool asNoTracking = false)
        {
            try
            {
                var entities = repository.GetAll().Where(e => !e.Deleted);
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
        /// Inserts a new IEntity objecTEntity into the database.
        /// Validates the user's permission to execute the operation.
        /// Records the operation into the audiTEntity database.
        /// </summary>
        public virtual void Create(TEntity entity)
        {
            try
            {
                CanAccess(entity, DALOperations.Create);

                entity.CreatedDate = DateTime.Now;
                entity.LastEditedDate = entity.CreatedDate;
                entity.CreatorId = this.operationContext.GetUserId();
                entity.LastEditorId = entity.CreatorId;

                repository.Create(entity);

                unitOfWork.SaveChanges<TEntity>();

                this.RegisterOperation(entity, DALOperations.Create);
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Updates an existenTEntity IEntity objecTEntity from the database.
        /// Validates the user's permission to execute the operation.
        /// Records the operation into the audiTEntity database.
        /// </summary>
        public virtual void Update(TEntity entity)
        {
            try
            {
                CanAccess(entity, DALOperations.Update);

                entity.LastEditedDate = DateTime.Now;
                entity.LastEditorId = this.operationContext.GetUserId();

                repository.Update(entity);

                unitOfWork.SaveChanges<TEntity>();

                this.RegisterOperation(entity, DALOperations.Update);
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Deletes (logical) an existenTEntity IEntity objecTEntity from the database by id.
        /// Validates the user's permission to execute the operation.
        /// Records the operation into the audiTEntity database.
        /// </summary>
        public virtual void Delete(Guid id)
        {
            try
            {
                TEntity entity = repository.Get(id);

                this.Delete(entity);
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Deletes (logical) an existenTEntity IEntity objecTEntity from the database.
        /// Validates the user's permission to execute the operation.
        /// Records the operation into the audiTEntity database.
        /// </summary>
        public virtual void Delete(TEntity entity)
        {
            try
            {
                CanAccess(entity, DALOperations.Delete);

                entity.LastEditedDate = DateTime.Now;
                entity.Deleted = true;
                entity.LastEditorId = this.operationContext.GetUserId();

                repository.Update(entity);

                unitOfWork.SaveChanges<TEntity>();

                this.RegisterOperation(entity, DALOperations.Delete);

            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Asynchronously retrieves a single IEntity objecTEntity from the Database by id.
        /// Validates the user's permission to execute the operation.
        /// Records the operation into the audiTEntity database.
        /// </summary>
        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            TEntity entity;
            try
            {
                entity = await repository.GetAsync(id);

                CanAccess(entity, DALOperations.Get);

                return entity;
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Asynchronously inserts a new IEntity objecTEntity into the database.
        /// Validates the user's permission to execute the operation.
        /// Records the operation into the audiTEntity database.
        /// </summary>
        public virtual async Task CreateAsync(TEntity entity)
        {
            try
            {
                CanAccess(entity, DALOperations.Create);

                entity.CreatedDate = DateTime.Now;
                entity.LastEditedDate = entity.CreatedDate;
                entity.CreatorId = this.operationContext.GetUserId();
                entity.LastEditorId = entity.CreatorId;

                repository.Create(entity);

                await unitOfWork.SaveChangesAsync<TEntity>();

                this.RegisterOperation(entity, DALOperations.Create);
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Asynchronously updates an existenTEntity IEntity objecTEntity from the database.
        /// Validates the user's permission to execute the operation.
        /// Records the operation into the audiTEntity database.
        /// </summary>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            try
            {
                CanAccess(entity, DALOperations.Update);

                entity.LastEditedDate = DateTime.Now;
                entity.LastEditorId = this.operationContext.GetUserId();

                repository.Update(entity);

                await unitOfWork.SaveChangesAsync<TEntity>();

                this.RegisterOperation(entity, DALOperations.Update);
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Asynchronously deletes (logical) an existenTEntity IEntity objecTEntity from the database by id.
        /// Validates the user's permission to execute the operation.
        /// Records the operation into the audiTEntity database.
        /// </summary>
        public virtual async Task DeleteAsync(Guid id)
        {
            try
            {
                TEntity entity = await this.GetAsync(id);

                await this.DeleteAsync(entity);

            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Asynchronously deletes (logical) an existenTEntity IEntity objecTEntity from the database.
        /// Validates the user's permission to execute the operation.
        /// Records the operation into the audiTEntity database.
        /// </summary>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            try
            {
                CanAccess(entity, DALOperations.Delete);

                entity.LastEditedDate = DateTime.Now;
                entity.Deleted = true;
                entity.LastEditorId = this.operationContext.GetUserId();

                repository.Update(entity);

                await unitOfWork.SaveChangesAsync<TEntity>();

                this.RegisterOperation(entity, DALOperations.Delete);
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Registers data about the operation executed for any auditable Entity (that implements IAuditable) for audit purposes.
        /// </summary>
        public void RegisterOperation(TEntity entity, DALOperations operation)
        {
            // Register operation only if the entity is Auditable.
            if (entity is IAuditable)
            {
                var auditLog = new AuditLog((entity as IAuditable), this.operationContext, operation);

                this.logger.Log<ILog>(LogLevel.Information, new EventId(), auditLog, null, null);
            }
        }
        #endregion

        #region Private Methods

        protected virtual void CanAccess(TEntity entity, DALOperations operation)
        {
            bool canAccess = false;
            if (entity == null || entity.Deleted)
                throw new NotFoundException(this.localizer[Lang.GetNotFoundEx]);

            try
            {
                this.operationContext.Operation = operation;

                switch (operation)
                {
                    case DALOperations.Create:
                    case DALOperations.Update:
                    case DALOperations.Delete:
                        canAccess = entity.CanWrite(this.operationContext);
                        break;
                    default:
                        this.operationContext.Operation = DALOperations.Get;
                        canAccess = entity.CanRead(this.operationContext);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(new UserException(localizer[Lang.CanAccessUnexpectedEx], ex));
            }

            if (!canAccess)
                throw exceptionManager.Handle(new UnauthorizedException(localizer[Lang.AccessDeniedEx]));
        }

        #endregion
    }
}