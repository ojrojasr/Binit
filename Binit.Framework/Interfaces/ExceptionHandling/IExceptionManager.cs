using Binit.Framework.ExceptionHandling.Types;
using Microsoft.Extensions.Logging;
using System;

namespace Binit.Framework.Interfaces.ExceptionHandling
{
    /// <summary>
    /// Exception manager interface. 
    /// Handles and logs exceptions.
    /// </summary>
    public interface IExceptionManager
    {
        ILogger Logger { get; }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        void Log(Exception ex);

        /// <summary>
        /// Handles and returns a user friendly version of the exception.
        /// </summary>
        UserException Handle(Exception ex);
    }
}