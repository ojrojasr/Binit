using System;

namespace Binit.Framework.ExceptionHandling.Types
{
    public class NotFoundException : UserException
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}