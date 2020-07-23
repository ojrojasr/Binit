using Binit.Framework.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Generic repository interface.
    /// </summary>
    public interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Retrieves a single IEntity object from the Database by id.
        /// </summary>
        T Get(Guid id);

        /// <summary>
        /// Asynchronously retrieves a single IEntity object from the Database by id.
        /// </summary>
        Task<T> GetAsync(Guid id);

        /// <summary>
        /// Returns an IQueriable<IEntity> representing all rows of an IEntity table.
        /// </summary>
        DbSet<T> GetAll();

        /// <summary>
        /// Attachs an IEntity object to the DbContext.
        /// </summary>
        void Create(T entity);

        /// <summary>
        /// Sets an IEntity object as Modified in the DbContext.
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Sets an IEntity object as Removed in the DbContext.
        /// </summary>
        void Delete(T entity);
    }
}