using System;

namespace Binit.Framework.ExceptionHandling.Types
{
    public class DatabaseException : UserException
    {
        public DatabaseException(string message)
            : base(message)
        {
        }
        public DatabaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}