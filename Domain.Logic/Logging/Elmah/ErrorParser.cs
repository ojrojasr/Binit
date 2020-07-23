using ElmahCore;
using LogEntities = Domain.Entities.Log;

namespace Domain.Logic.Logging.Elmah
{
    public static class ErrorParser
    {
        public static LogEntities.ErrorLog ElmahToEntity(Error elmahError)
        {
            var errorLog = new LogEntities.ErrorLog()
            {
                ApplicationName = elmahError.ApplicationName,
                Detail = elmahError.Detail,
                HostName = elmahError.HostName,
                Message = elmahError.Message,
                Source = elmahError.Source,
                StatusCode = elmahError.StatusCode,
                CreatedDate = elmahError.Time,
                Type = elmahError.Type,
                User = elmahError.User,
                WebHostHtmlMessage = elmahError.WebHostHtmlMessage
            };

            if (elmahError.Exception != null)
                errorLog.Exception = elmahError.Exception.ToString();

            return errorLog;
        }

        public static Error EntityToElmah(LogEntities.ErrorLog errorLog)
        {
            return new Error()
            {
                ApplicationName = errorLog.ApplicationName,
                Detail = errorLog.Detail,
                HostName = errorLog.HostName,
                Message = errorLog.Message,
                Source = errorLog.Source,
                StatusCode = errorLog.StatusCode,
                Time = errorLog.CreatedDate,
                Type = errorLog.Type,
                User = errorLog.User,
                WebHostHtmlMessage = errorLog.WebHostHtmlMessage
            };
        }
    }
}