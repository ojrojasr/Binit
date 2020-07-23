using Binit.Framework.Constants.DAL;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Binit.Framework.Interfaces.DAL
{
    /// <summary>
    /// Generic service interface.
    /// </summary>
    public interface IService<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Retrieves a single IEntity object from the Database by id.
        /// </summary>
        TEntity Get(Guid id);

        /// <summary>
        /// Returns an IQueriable<IEntity> representing all rows of an IEntity table.
        /// </summary>
        IQueryable<TEntity> GetAll(bool asNoTracking = false);

        /// <summary>
        /// Inserts a new IEntity object into the database.
        /// </summary>
        void Create(TEntity entity);

        /// <summary>
        /// Updates an existent IEntity object from the database.
        /// </summary>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes (logical) an existent IEntity object from the database by id.
        /// </summary>
        void Delete(Guid id);

        /// <summary>
        /// Deletes (logical) an existent IEntity object from the database.
        /// </summary>
        void Delete(TEntity entity);

        /// <summary>
        /// Asynchronously retrieves a single IEntity object from the Database by id.
        /// </summary>
        Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// Asynchronously inserts a new IEntity object into the database.
        /// </summary>
        Task CreateAsync(TEntity entity);

        /// <summary>
        /// Asynchronously updates an existent IEntity object from the database.
        /// </summary>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Asynchronously deletes (logical) an existent IEntity object from the database by id.
        /// </summary>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Asynchronously deletes (logical) an existent IEntity object from the database.
        /// </summary>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Registers data about the operation executed for the Entity for audit purposes.
        /// </summary>
        void RegisterOperation(TEntity entity, DALOperations operation);
    }
}