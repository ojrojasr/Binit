using Binit.Framework.ExceptionHandling.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;

namespace Binit.Framework.ExceptionHandling.ExceptionResponses
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public Exception Exception { get; set; }

        public ErrorResponse(UserException ex, HttpStatusCode statusCode)
        {
            this.Message = ex.Message;
            this.StatusCode = (int)statusCode;
        }

        public ErrorResponse(UserException ex, int statusCode)
        {
            this.Message = ex.Message;
            this.StatusCode = statusCode;
        }

        public ErrorResponse(string errorMessage, HttpStatusCode statusCode)
        {
            this.Message = errorMessage;
            this.StatusCode = (int)statusCode;
        }

        public string ToJson()
        {
            // We'll temporarily use Newtonsoft for json serialization until mechanism to handle circular references is fixed for System.Text.Json.Serialization
            // Tracking code: 3.0.0-issue1
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}
