using System;

namespace Binit.Framework.ExceptionHandling.Types
{
    public class ValidationException : UserException
    {
        public ValidationException(string message)
            : base(message)
        {
        }
        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}