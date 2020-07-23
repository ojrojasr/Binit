using Binit.Framework.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Unit of Work interface.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Instantiates a new Repository for a given IEntity type.
        /// </summary>
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

        /// <summary>
        /// Exposes the DbContext correspondent to a given TEntity as readonly.
        /// </summary>
        DbContext GetDbContext<TEntity>() where TEntity : class, IEntity;

        /// <summary>
        /// Exposes the ModelDbContext
        /// </summary>
        DbContext GetModelDbContext();

        /// <summary>
        /// Exposes the LogDbContext
        /// </summary>
        DbContext GetLogDbContext();

        /// <summary>
        /// Saves all the changes registered to the DbContext
        /// </summary>
        int SaveChanges<TEntity>() where TEntity : class, IEntity;

        /// <summary>
        /// Asynchronously saves all the changes registered to the DbContext
        /// </summary>
        Task<int> SaveChangesAsync<TEntity>() where TEntity : class, IEntity;
    }
}