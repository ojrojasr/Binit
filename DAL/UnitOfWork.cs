using Binit.Framework.Extensions;
using Binit.Framework.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Unit of Work class.
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Properties

        private bool disposed;
        private readonly DbContext modelDbContext;
        private readonly DbContext logDbContext;

        #endregion

        #region Constructor

        public UnitOfWork(DbContext modelDbContext, DbContext logDbContext)
        {
            this.modelDbContext = modelDbContext;
            this.logDbContext = logDbContext;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Exposes the DbContext corresponding to the specified TEntity as readonly.
        /// </summary>
        public DbContext GetDbContext<TEntity>() where TEntity : class, IEntity
        {
            if (Reflection.ClassImplementsInterface<TEntity, ILog>())
                return GetLogDbContext();
            else
                return GetModelDbContext();
        }


        /// <summary>
        /// Exposes the ModelDbContext
        /// </summary>
        public DbContext GetModelDbContext()
        {
            return this.modelDbContext;
        }

        /// <summary>
        /// Exposes the ModelDbContext
        /// </summary>
        public DbContext GetLogDbContext()
        {
            return this.logDbContext;
        }

        /// <summary>
        /// Instantiates a new Repository for a given IEntity type.
        /// </summary>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {
            IRepository<TEntity> repository;

            repository = new Repository<TEntity>(this.GetDbContext<TEntity>());

            return repository as IRepository<TEntity>;
        }

        /// <summary>
        /// Saves all the changes registered to the DbContext
        /// </summary>
        public int SaveChanges<TEntity>() where TEntity : class, IEntity
        {
            return this.GetDbContext<TEntity>().SaveChanges();
        }

        /// <summary>
        /// Asynchronously saves all the changes registered to the DbContext
        /// </summary>
        public async Task<int> SaveChangesAsync<TEntity>() where TEntity : class, IEntity
        {
            return await this.GetDbContext<TEntity>().SaveChangesAsync();
        }

        /// <summary>
        /// Disposes the UnitOfWork checking if it's already disposed first.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected Methods
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.modelDbContext.Dispose();
                    this.logDbContext.Dispose();
                }
            }

            this.disposed = true;
        }
        #endregion
    }
}