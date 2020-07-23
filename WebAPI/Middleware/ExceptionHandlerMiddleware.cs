using Binit.Framework;
using Binit.Framework.ExceptionHandling.ExceptionResponses;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Interfaces.ExceptionHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using System.Net;
using static Binit.Framework.Localization.LocalizationConstants.BinitFramework;

namespace WebAPI.Middleware
{
    /// <summary>
    /// Middleware to catch and handle exceptions accordingly for a WebAPI.
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IWebHostEnvironment env;
        private readonly IExceptionManager exceptionManager;

        public ExceptionHandlerMiddleware(IStringLocalizer<SharedResources> localizer, IWebHostEnvironment env, IExceptionManager exceptionManager)
        {
            this.localizer = localizer;
            this.env = env;
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Catches Exceptions and builds the response with a status code according to the exception type.
        /// </summary>
        public void BuildExceptionResponse(IApplicationBuilder app)
        {
            app.Run(async context =>
                {
                    // Get error from context.
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    ErrorResponse errRes = null;

                    if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error != null)
                    {
                        var ex = exceptionHandlerPathFeature.Error;

                        if (!(ex is UserException))
                        {
                            // If it's not UserException, handle it using the ExceptionManager (logs it & returns a UserException).
                            ex = this.exceptionManager.Handle(ex);
                        }

                        // Build an ErrorResponse object according to the exception type.
                        if (ex is ValidationException)
                        {
                            errRes = new ValidationErrorResponse(ex as ValidationException);
                        }
                        else if (ex is NotFoundException)
                        {
                            errRes = new ErrorResponse(ex as NotFoundException, HttpStatusCode.NotFound);
                        }
                        else if (ex is UnauthorizedException)
                        {
                            errRes = new ErrorResponse(ex as UnauthorizedException, HttpStatusCode.Unauthorized);
                        }
                        else
                        {
                            errRes = new ErrorResponse(ex as UserException, HttpStatusCode.InternalServerError);
                        }

                        // Only add the exception to the response on Development environment, for security matters.
                        if (env.IsDevelopment())
                            errRes.Exception = ex;
                    }
                    else
                    {
                        // If no exception is found, build a special response for some status codes.
                        if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                        {
                            var notFoundEx = new NotFoundException(localizer[ExceptionHandling.ResourceNotFoundGenericEx].Value);
                            errRes = new ErrorResponse(notFoundEx, HttpStatusCode.NotFound);
                        }
                        else if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                        {
                            var unauthorizedEx = new UnauthorizedException(localizer[ExceptionHandling.HandleUnauthorizedEx].Value);
                            errRes = new ErrorResponse(unauthorizedEx, HttpStatusCode.Unauthorized);
                        }
                        else
                        {
                            // Generic unexpected error.
                            var unexpectedEx = this.exceptionManager.Handle(new UserException(localizer[ExceptionHandling.HandleGenericEx].Value));
                            errRes = new ErrorResponse(unexpectedEx, context.Response.StatusCode);
                        }
                    }

                    // Build response.
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = errRes.StatusCode;
                    await context.Response.WriteAsync(errRes.ToJson());
                });
        }
    }
}