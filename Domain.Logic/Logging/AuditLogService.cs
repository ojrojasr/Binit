using Binit.Framework;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using DAL;
using Domain.Entities.Log;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Domain.Logic.Logging
{
    public class AuditLogService : Service<AuditLog>
    {
        public AuditLogService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
        IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {
        }
    }
}