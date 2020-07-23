using Binit.Framework.ExceptionHandling.Types;
using System;
using System.Collections.Generic;
using System.Net;

namespace Binit.Framework.ExceptionHandling.ExceptionResponses
{
    public class ValidationErrorResponse : ErrorResponse
    {
        public IDictionary<string, string> Errors { get; } = new Dictionary<string, string>(StringComparer.Ordinal);

        public ValidationErrorResponse(ValidationException ex)
        : base(ex, HttpStatusCode.BadRequest)
        {
            Errors.Add("ValidationError", ex.Message);
        }
    }
}
