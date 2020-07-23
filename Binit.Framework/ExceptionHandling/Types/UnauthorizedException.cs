using System;

namespace Binit.Framework.ExceptionHandling.Types
{
    public class UnauthorizedException : UserException
    {
        public UnauthorizedException(string message)
            : base(message)
        {
        }
        public UnauthorizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}