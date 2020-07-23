using Binit.Framework;
using Binit.Framework.Constants.DAL;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using DAL;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.Services.UserService;

namespace Domain.Logic.Services
{
    /// <summary>
    /// Service for user administration.
    /// </summary>
    public class UserService : Service<ApplicationUser>, IUserService
    {
        #region Properties

        private readonly UserManager<ApplicationUser> userManager;

        #endregion

        #region Constructor

        public UserService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
        IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer, UserManager<ApplicationUser> userManager)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {
            this.userManager = userManager;
        }

        #endregion

        #region Methods

        ///<summary>
        /// Get user by Id including all its dependencies.
        ///</summary>
        public async Task<ApplicationUser> GetFullAsync(Guid userId)
        {
            var user = await base.GetAll()
            .Where(u => u.Id == userId)
            .Include(u => u.Tenant)
            .FirstOrDefaultAsync();

            return user;
        }

        /// <summary>
        /// Creates a new user and returns the code that should be sent via email for the user to confirm their account.
        /// May throw an IdentityException if the User couldn't be registered.
        /// Check the IdentityException.Errors property for more details.
        /// </summary>
        public async Task<string> CreateAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            try
            {
                // Set a sequential id.
                user.Id = SequentialGuidGenerator.NewSequentialGuid();

                // Register user without password.
                var createResult = await userManager.CreateAsync(user);

                if (!createResult.Succeeded)
                    throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.CreateAsyncFailedEx], null, createResult.Errors.ToList()));

                // Set user roles
                var addRolesResult = await userManager.AddToRolesAsync(user, roles);

                if (!addRolesResult.Succeeded)
                    throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.CreateAsyncAddRolesFailedEx], null, addRolesResult.Errors.ToList()));

                // Add tenant as claim.
                var claim = new Claim(CustomClaimTypes.Tenant, user.TenantId.ToString());
                var addTenantResult = await this.userManager.AddClaimAsync(user, claim);

                if (!addTenantResult.Succeeded)
                    throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.CreateAsyncAddTenantClaimFailedEx], null, addTenantResult.Errors.ToList()));

                logger.LogTrace("Created a new account without password.");

                // Generate email confirmation code.
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                return code;
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Updates the user and their roles.
        /// May throw an IdentityException if the User couldn't be registered or the roles couldn't be set.
        /// Check the IdentityException.Errors property for more details.
        /// </summary>
        public async Task UpdateAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            try
            {
                // Get current user from db.
                // Only include relationships that need to be auto-updated by EF Core.
                var currentUser = await base.GetAll()
                .Where(u => u.Id == user.Id)
                .FirstOrDefaultAsync();

                // Set values from memory user to db tracked user.
                // This makes sure the changes to many-to-many relationships are applied and tracked by EF Core.
                user.CopyTo(currentUser);

                // Update user.
                var updateResult = await userManager.UpdateAsync(currentUser);

                if (!updateResult.Succeeded)
                    throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.UpdateAsyncFailedEx], null, updateResult.Errors.ToList()));

                // Get current roles.
                var currentRoles = await userManager.GetRolesAsync(currentUser);

                // Calculate roles to remove.
                var rolesToRemove = currentRoles.Except(roles);

                // Remove user from roles.
                var removeRolesResult = await this.userManager.RemoveFromRolesAsync(currentUser, rolesToRemove);

                if (!removeRolesResult.Succeeded)
                    throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.UpdateAsyncRemoveRolesFailedEx], null, removeRolesResult.Errors.ToList()));

                // Calculate roles to add.
                var rolesToAdd = roles.Except(currentRoles);

                // Add user to their new roles.
                var addRolesResult = await this.userManager.AddToRolesAsync(currentUser, rolesToAdd);

                if (!addRolesResult.Succeeded)
                    throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.UpdateAsyncAddRolesFailedEx], null, addRolesResult.Errors.ToList()));

                // Replace tenant claims.
                var userTenantClaim = (await this.userManager.GetClaimsAsync(currentUser)).Where(c => c.Type == CustomClaimTypes.Tenant);
                var claim = new Claim(CustomClaimTypes.Tenant, user.TenantId.ToString());

                var replaceTenantResult = await this.userManager.ReplaceClaimAsync(currentUser, userTenantClaim.First(), claim);

                if (!replaceTenantResult.Succeeded)
                    throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.UpdateAsyncReplaceTenantClaimFailedEx], null, replaceTenantResult.Errors.ToList()));

                logger.LogTrace("Updated user.");
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Updates user email uppon deletion to allow deleted users 
        /// to come back to the system with the same email
        /// </summary>
        public async override Task DeleteAsync(Guid id)
        {
            if (this.operationContext.GetUserId().Equals(id))
            {
                throw new UnauthorizedException(this.localizer[Lang.AutoDeleteNotAllowed]);
            }
            try
            {
                var uniqueId = Guid.NewGuid().ToString();
                var user = await this.GetFullAsync(id);
                user.Email = user.Email + uniqueId;
                user.UserName = user.UserName + uniqueId;
                user.NormalizedEmail = user.NormalizedEmail + uniqueId;
                user.NormalizedUserName = user.NormalizedUserName + uniqueId;

                // Remove external logins.
                var externalLogins = await this.userManager.GetLoginsAsync(user);
                foreach (var login in externalLogins)
                {
                    await this.userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                }

                await this.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        public async override Task<ApplicationUser> GetAsync(Guid id)
        {
            try
            {
                return await this.userManager.FindByIdAsync(id.ToString());
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        public async Task<List<ApplicationUser>> SearchUsersAsync(string searchTerm)
        {
            var caseInsensitiveSearchTerm = $"%{searchTerm}%";

            return await base.GetAll()
            .Where(u => EF.Functions.Like(u.Name, caseInsensitiveSearchTerm) ||
                EF.Functions.Like(u.LastName, caseInsensitiveSearchTerm) ||
                EF.Functions.Like(u.Email, caseInsensitiveSearchTerm))
            .ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetMany(List<string> ids)
        {
            return await base.GetAll()
            .Where(u => ids.Contains(u.Id.ToString()))
            .ToListAsync();
        }

        #endregion
    }
}