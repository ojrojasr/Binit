using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Interfaces.ExceptionHandling;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Mail;
using Lang = Binit.Framework.Localization.LocalizationConstants.BinitFramework.ExceptionHandling;

namespace Binit.Framework.ExceptionHandling
{
    /// <summary>
    /// Exception manager class.
    /// </summary>
    public class ExceptionManager : IExceptionManager
    {
        private ILogger logger;
        private readonly IStringLocalizer<SharedResources> localizer;

        public ExceptionManager(ILogger logger, IStringLocalizer<SharedResources> localizer)
        {
            this.logger = logger;
            this.localizer = localizer;
        }

        public ILogger Logger
        {
            get
            {
                return logger;
            }
        }
        /// <summary>
        /// Logs the exception and returns a UserException based on the original exception if possible.
        /// </summary>
        public UserException Handle(Exception ex)
        {
            UserException uex = null;

            // Build custom messages for each known exception.
            if (ex is SmtpException)
                uex = new EmailException(this.localizer[Lang.HandleEmailEx], ex);

            if (ex is SqlException)
                uex = new DatabaseException(this.localizer[Lang.HandleSQLEx], ex);

            if (ex is DbUpdateException)
                uex = new DatabaseValidationException(this.localizer[Lang.HandleDbValidationEx], ex);

            if (ex is IOException)
                uex = new FileIOException(this.localizer[Lang.HandleIOEx], ex);

            // If it's UserException there's nothing else to do.
            if (ex is UserException)
                uex = (ex as UserException);

            if (uex != null)
            {
                // Log exception.
                this.Log(uex);

                // Return handled exception.
                return uex;
            }

            // Log original exception if it wasn't handled.
            this.Log(ex);

            // Regurn generic handled exception.
            return new UserException(this.localizer[Lang.HandleGenericEx], ex);
        }

        /// <summary>
        /// Logs the exception using ElmahCore.
        /// </summary>
        public void Log(Exception ex)
        {
            // Logs the error through ILogger.
            logger.LogError(ex, ex.Message);
        }
    }
}