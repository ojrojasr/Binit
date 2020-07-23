using Binit.Framework;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using static Binit.Framework.Localization.LocalizationConstants.BinitFramework;

namespace WebApp.Middleware
{
    /// <summary>
    /// Middleware that verifies the current user belongs to the app's Realm.
    /// </summary>
    public class RealmAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public RealmAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IOperationContext operationContext,
            IAccountService accountService, IExceptionManager exceptionManager, IStringLocalizer<SharedResources> localizer)
        {
            if (operationContext.UserIsAuthenticated() && !operationContext.UserBelongsToRealm())
            {
                await context.SignOutAsync();
                await accountService.Logout();

                exceptionManager.Handle(new UnauthorizedException(localizer[ExceptionHandling.RealmAuthorizationFailedEx]));

                context.Response.Redirect("/Identity/Account/Login");
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}