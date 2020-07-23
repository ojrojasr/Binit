using System;

namespace Binit.Framework.ExceptionHandling.Types
{
    public class EmailException : UserException
    {
        public EmailException(string message)
            : base(message)
        {
        }
        public EmailException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}