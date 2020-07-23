using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace DAL
{
    /// <summary>
    /// This Service is a valid example to retrieve data from a SP.
    /// </summary>
    /// <typeparam name="SPCommand"></typeparam>
    public class SPService<SPCommand> : ISPService<SPCommand> where SPCommand : class, ISPCommand
    {
        #region Properties

        protected readonly IUnitOfWork unitOfWork;
        protected readonly IExceptionManager exceptionManager;

        #endregion

        #region Constructor

        public SPService(IExceptionManager exceptionManager, IUnitOfWork unitOfWork)
        {
            this.exceptionManager = exceptionManager;
            this.unitOfWork = unitOfWork;
        }

        #endregion

        /// <summary>
        /// Method to call a SP 
        /// </summary>
        /// <returns></returns>
        public void ExecuteSP(SPCommand spCommand)
        {
            try
            {
                object[] parameters = new object[spCommand.Params.Count];
                spCommand.Params.Values.CopyTo(parameters, 0);
                var cmd = new StringBuilder($"EXECUTE {spCommand.Name} ");
                foreach (var param in spCommand.Params)
                {
                    cmd.Append($"'{param.Value.ToString()}'");
                    if (param.Value != spCommand.Params.Last().Value)
                        cmd.Append(", ");
                }
                this.unitOfWork.GetModelDbContext()
                    .Database.ExecuteSqlRaw(cmd.ToString());
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }


        /// <summary>
        /// Method to call a SP and retrieve data
        /// </summary>
        /// <returns></returns>
        public IQueryable<DbView> ExecuteSP<DbView>(SPCommand spCommand) where DbView : class, IDbView
        {
            try
            {
                object[] parameters = new object[spCommand.Params.Count];
                spCommand.Params.Values.CopyTo(parameters, 0);
                var cmd = new StringBuilder($"EXECUTE {spCommand.Name} ");
                foreach (var param in spCommand.Params)
                {
                    cmd.Append($"'{param.Value.ToString()}'");
                    if (param.Value != spCommand.Params.Last().Value)
                        cmd.Append(", ");
                }
                return this.unitOfWork.GetModelDbContext().Set<DbView>().FromSqlRaw(cmd.ToString()).AsNoTracking();
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }
    }
}
