using System;

namespace Binit.Framework.ExceptionHandling.Types
{
    public class FileIOException : UserException
    {
        public FileIOException(string message)
            : base(message)
        {
        }
        public FileIOException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}