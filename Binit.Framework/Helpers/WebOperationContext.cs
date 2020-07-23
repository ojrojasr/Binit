using Binit.Framework.Constants.DAL;
using Binit.Framework.Interfaces.Configuration;
using Binit.Framework.Interfaces.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Binit.Framework.Helpers
{
    /// <summary>
    /// Operation context for all web based probjects.
    /// </summary>
    public class WebOperationContext : IOperationContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IRealmConfiguration realmConfiguration;

        public DALOperations Operation { get; set; }

        public WebOperationContext(IHttpContextAccessor httpContextAccessor, IRealmConfiguration realmConfiguration)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.realmConfiguration = realmConfiguration;
        }

        /// <summary>
        /// Get current user Id from the current HttpContext.
        /// </summary>
        public Guid GetUserId()
        {
            string id = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.Parse(id);
        }

        /// <summary>
        /// Get current user username from the current HttpContext.
        /// </summary>
        public string GetUsername()
        {
            return httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        /// <summary>
        /// Returns true if any of the user roles contains the current realm.
        /// </summary>
        public bool UserBelongsToRealm()
        {
            var roles = this.GetUserRoles();
            return this.RolesBelongToRealm(roles);
        }

        /// <summary>
        /// Returns true if any of the user roles contains the current realm.
        /// </summary>
        public bool RolesBelongToRealm(IEnumerable<string> roles)
        {
            bool anyRoleBelongsToRealm = roles.Any(r => r.StartsWith($"{realmConfiguration.Name}."));

            return anyRoleBelongsToRealm;
        }

        /// <summary>
        /// Get current user roles from the current HttpContext.
        /// </summary>
        public IEnumerable<string> GetUserRoles()
        {
            return httpContextAccessor.HttpContext.User.FindAll(ClaimTypes.Role).Select(r => r.Value);
        }

        /// <summary>
        /// Check if a user is authenticated.
        /// </summary>
        public bool UserIsAuthenticated()
        {
            var isAuthenticated = httpContextAccessor.HttpContext.User != null && httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            return isAuthenticated;
        }

        /// <summary>
        /// Check if user is assigned to a specific role.
        /// </summary>
        public bool UserIsInRole(string role)
        {
            if (!this.UserIsAuthenticated())
                return false;

            var roles = this.GetUserRoles();
            return roles.Any(r => r == role);
        }

        /// <summary>
        /// Check if user is assigned to at least one of the specified roles.
        /// </summary>

        public bool UserIsInAnyRole(params string[] roles)
        {
            if (!this.UserIsAuthenticated())
                return false;

            var userRoles = this.GetUserRoles();
            return userRoles.Any(r => roles.Contains(r));
        }

        /// <summary>
        /// Check if user is assigned to all of the specified roles.
        /// </summary>
        public bool UserIsInAllRoles(params string[] roles)
        {
            if (!this.UserIsAuthenticated())
                return false;

            var userRoles = this.GetUserRoles();
            return !roles.Except(userRoles).Any();
        }

        /// <summary>
        /// Get current user's tenant Id from the User's Claims.
        /// </summary>
        public Guid? GetUserTenantId()
        {
            var userTenantId = httpContextAccessor.HttpContext.User
                .FindAll(CustomClaimTypes.Tenant)
                .Select(r => r.Value)
                .FirstOrDefault();

            if (userTenantId != null)
                return new Guid(userTenantId);

            return null;
        }
    }
}