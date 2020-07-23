using System;
using System.Net;

namespace Binit.Framework.ExceptionHandling.Types
{
    public class ExternalServiceException : UserException
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ExternalServiceMessage { get; set; }

        public ExternalServiceException(string message, string externalServiceMessage, HttpStatusCode statusCode)
            : base(message)
        {
            this.StatusCode = statusCode;
            this.ExternalServiceMessage = externalServiceMessage;
        }

        public ExternalServiceException(string message, HttpStatusCode statusCode, string externalServiceMessage, Exception innerException)
            : base(message, innerException)
        {
            this.StatusCode = statusCode;
            this.ExternalServiceMessage = externalServiceMessage;
        }
    }
}