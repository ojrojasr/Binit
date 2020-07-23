using Binit.Framework.Constants.DAL;
using System;
using System.Collections.Generic;

namespace Binit.Framework.Interfaces.DAL
{
    /// <summary>
    /// Interface that handles the current user's operation context.
    /// </summary>
    public interface IOperationContext
    {

        /// <summary>
        /// Identifies the type of operation (CRUD) that's being performed.
        /// </summary>
        DALOperations Operation { get; set; }

        /// <summary>
        /// Get current user Id.
        /// </summary>
        Guid GetUserId();

        /// <summary>
        /// Get current user username.
        /// </summary>
        string GetUsername();

        /// <summary>
        /// Verifies if user belongs to the current realm.
        /// </summary>
        bool UserBelongsToRealm();

        /// <summary>
        /// Verify if roles belong to realm.
        /// </summary>
        bool RolesBelongToRealm(IEnumerable<string> roles);

        /// <summary>
        /// Get current user roles.
        /// </summary>
        IEnumerable<string> GetUserRoles();

        /// <summary>
        /// Check if a user is authenticated.
        /// </summary>
        bool UserIsAuthenticated();

        /// <summary>
        /// Check if user is assigned to a specific role.
        /// </summary>
        bool UserIsInRole(string role);

        /// <summary>
        /// Check if user is assigned to at least one of the specified roles.
        /// </summary>
        bool UserIsInAnyRole(params string[] roles);

        /// <summary>
        /// Check if user is assigned to all of the specified roles.
        /// </summary>
        bool UserIsInAllRoles(params string[] roles);

        /// <summary>
        /// Get current user's tenant Id.
        /// </summary>
        Guid? GetUserTenantId();

    }
}