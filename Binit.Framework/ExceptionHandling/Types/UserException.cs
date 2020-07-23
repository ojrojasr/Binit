using System;

namespace Binit.Framework.ExceptionHandling.Types
{
    public class UserException : Exception
    {
        public UserException(string message)
            : base(message)
        {
        }
        public UserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}