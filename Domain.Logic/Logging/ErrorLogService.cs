using Binit.Framework;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using DAL;
using Domain.Entities.Log;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.Services.ErrorLogService;

namespace Domain.Logic.Logging
{
    public class ErrorLogService : Service<ErrorLog>
    {
        public ErrorLogService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
        IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {
        }

        /// <summary>
        /// Gets page ordered by descending created date
        /// </summary>
        public IQueryable<ErrorLog> GetPage(int page, int pageSize)
        {
            return base.GetAll()
                .OrderByDescending(e => e.CreatedDate)
                .Skip(page * pageSize)
                .Take(pageSize);
        }

        /// <summary>
        /// Override the Create method to catch any db access errors
        /// and log them using stdErr to avoid infinite loops.
        /// </summary>
        public override void Create(ErrorLog entity)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                entity.LastEditedDate = DateTime.Now;
                repository.Create(entity);

                unitOfWork.SaveChanges<ErrorLog>();
            }
            catch (Exception ex)
            {
                // Don't throw an error to avoid an infinite loop.
                // Log the exception (stdErr) if the log fails.
                Console.Error.WriteLine(string.Format(localizer[Lang.LogErrorFailedEx], ex.ToString(), entity.Exception ?? entity.Detail));
            }
        }
    }
}