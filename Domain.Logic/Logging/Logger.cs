using Domain.Entities.Log;
using Domain.Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Domain.Logic.Logging
{
    /// <summary>
    /// ILogger implementation that logs exceptions and audit data through elmah and the Data Access Layer.
    /// </summary>
    public class Logger : ILogger
    {
        #region Properties

        private readonly IServiceScopeFactory scopeFactory;
        private readonly IHttpContextAccessor httpContext;
        private readonly UserManager<ApplicationUser> userManager;

        #endregion

        #region Constructor

        public Logger(IServiceScopeFactory scopeFactory, IHttpContextAccessor httpContext, UserManager<ApplicationUser> userManager)
        {
            this.scopeFactory = scopeFactory;
            this.httpContext = httpContext;
            this.userManager = userManager;
        }

        #endregion


        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// Returns "True" if the LogLevel can be logged through this ILogger implementation.
        /// </summary>
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Error || logLevel == LogLevel.Information;
        }

        /// <summary>
        /// Logs exceptions through ElmahCore.
        /// </summary>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel))
            {
                if (exception != null)
                {
                    ElmahCore.ElmahExtensions.RiseError(this.httpContext.HttpContext, exception);
                }
                else if (state is AuditLog)
                {
                    using (var scope = scopeFactory.CreateScope())
                    {
                        var auditLogService = scope.ServiceProvider.GetRequiredService<AuditLogService>();

                        var auditLog = (state as AuditLog);
                        // Set user if is logged in.
                        auditLog.User = this.userManager.GetUserName(this.httpContext.HttpContext.User);

                        auditLogService.Create((state as AuditLog));
                    }
                }
            }
        }
    }
}