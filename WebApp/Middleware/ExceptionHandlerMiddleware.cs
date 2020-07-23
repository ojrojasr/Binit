using Binit.Framework;
using Binit.Framework.ExceptionHandling.ExceptionResponses;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.ExceptionHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Net;
using WebApp.Models;
using ExceptionsLang = Binit.Framework.Localization.LocalizationConstants.BinitFramework.ExceptionHandling;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.WebTools.ExceptionHandlerMiddleware;

namespace WebApp.Middleware
{
    /// <summary>
    /// Middleware to catch and handle exceptions accordingly for a WebAPI.
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IWebHostEnvironment env;
        private readonly IExceptionManager exceptionManager;
        private readonly RazorViewRender razorViewRender;

        public ExceptionHandlerMiddleware(IStringLocalizer<SharedResources> localizer, IWebHostEnvironment env,
        IExceptionManager exceptionManager, RazorViewRender razorViewRender)
        {
            this.localizer = localizer;
            this.env = env;
            this.exceptionManager = exceptionManager;
            this.razorViewRender = razorViewRender;
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
                    var viewTitle = localizer[Lang.ErrorTitle].Value;

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
                            viewTitle = localizer[Lang.ErrorTitle404];
                        }
                        else if (ex is UnauthorizedException)
                        {
                            errRes = new ErrorResponse(ex as NotFoundException, HttpStatusCode.Unauthorized);
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
                            var notFoundEx = new NotFoundException(localizer[ExceptionsLang.ResourceNotFoundGenericEx].Value);
                            viewTitle = localizer[Lang.ErrorTitle404];
                            errRes = new ErrorResponse(notFoundEx, HttpStatusCode.NotFound);
                        }
                        else if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                        {
                            var unauthorizedEx = new UnauthorizedException(localizer[ExceptionsLang.HandleUnauthorizedEx].Value);
                            errRes = new ErrorResponse(unauthorizedEx, HttpStatusCode.Unauthorized);
                        }
                        else
                        {
                            // Generic unexpected error.
                            var unexpectedEx = this.exceptionManager.Handle(new UserException(localizer[ExceptionsLang.HandleGenericEx].Value));
                            errRes = new ErrorResponse(unexpectedEx, context.Response.StatusCode);
                        }
                    }

                    // Build response.
                    var viewData = new List<KeyValuePair<string, object>>();
                    viewData.Add(new KeyValuePair<string, object>("Title", viewTitle));
                    string errorView = await razorViewRender.RenderToStringAsync("Error", new ErrorViewModel(errRes), viewData);
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.StatusCode = errRes.StatusCode;
                    await context.Response.WriteAsync(errorView);
                });
        }
    }
}