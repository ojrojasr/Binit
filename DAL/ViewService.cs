using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// This Service is a valid example to retrieve data from a view.
    /// </summary>
    /// <typeparam name="DbView"></typeparam>
    public class ViewService<DbView> : IViewService<DbView> where DbView : class, IDbView
    {
        #region Properties

        protected readonly IUnitOfWork unitOfWork;
        protected readonly IExceptionManager exceptionManager;

        #endregion

        #region Constructor

        public ViewService(IExceptionManager exceptionManager, IUnitOfWork unitOfWork)
        {
            this.exceptionManager = exceptionManager;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        /// <summary>
        /// Method to call a View 
        /// </summary>
        /// <returns></returns>
        public IQueryable<DbView> GetAll()
        {
            try
            {
                return this.unitOfWork.GetModelDbContext().Set<DbView>().AsNoTracking();
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }
    }
}