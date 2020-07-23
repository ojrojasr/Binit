using Binit.Framework.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;


namespace DAL
{
    /// <summary>
    /// Generic repository class.
    /// </summary>
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        #region Properties

        private readonly DbContext dbContext;

        #endregion

        #region Constructor

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves a single IEntity object from the Database by id.
        /// </summary>
        public T Get(Guid id)
        {
            return this.dbContext.Set<T>().Find(id);
        }

        /// <summary>
        /// Asynchronously retrieves a single IEntity object from the Database by id.
        /// </summary>
        public async Task<T> GetAsync(Guid id)
        {
            return await this.dbContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Returns an IQueriable<IEntity> representing all rows of an IEntity table.
        /// </summary>
        public DbSet<T> GetAll()
        {
            return this.dbContext.Set<T>();
        }

        /// <summary>
        /// Attachs an IEntity object to the DbContext.
        /// </summary>
        public void Create(T entity)
        {
            this.dbContext.Set<T>().Add(entity);
        }

        /// <summary>
        /// Sets an IEntity object as Modified in the DbContext.
        /// </summary>
        public void Update(T entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Modified;
            this.dbContext.Entry(entity).Property(p => p.CreatedDate).IsModified = false;
            this.dbContext.Entry(entity).Property(p => p.CreatorId).IsModified = false;
        }

        /// <summary>
        /// Sets an IEntity object as Removed in the DbContext.
        /// </summary>
        public void Delete(T entity)
        {
            if (this.dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.dbContext.Set<T>().Attach(entity);
            }
            this.dbContext.Entry(entity).State = EntityState.Deleted;
            this.dbContext.Set<T>().Remove(entity);
        }

        #endregion
    }
}