using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Binit.Framework.ExceptionHandling.Types
{
    public class IdentityException : UserException
    {


        public ICollection<IdentityError> Errors { get; set; }

        public IdentityException(string message)
            : base(message)
        {
        }

        public IdentityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public IdentityException(string message, ICollection<IdentityError> Errors)
                    : base(message)
        {
            this.Errors = Errors;
        }

        public IdentityException(string message, Exception innerException, ICollection<IdentityError> Errors)
            : base(message, innerException)
        {
            this.Errors = Errors;
        }

        public bool HasErrors()
        {
            return this.Errors != null && this.Errors.Count > 0;
        }
    }
}