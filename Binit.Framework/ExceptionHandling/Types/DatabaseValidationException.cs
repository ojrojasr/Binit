using System;

namespace Binit.Framework.ExceptionHandling.Types
{
    public class DatabaseValidationException : UserException
    {
        public DatabaseValidationException(string message)
            : base(message)
        {
        }
        public DatabaseValidationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}