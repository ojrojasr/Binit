using Microsoft.AspNetCore.Authorization;
using System;

namespace WebAPI.Attributes
{
    public class AuthorizeAnyRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeAnyRolesAttribute(params string[] roles)
        {
            base.Roles = String.Join(",", roles);
        }
    }
}