using Binit.Framework;
using Binit.Framework.ExceptionHandling.Types;
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
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.Services.FrontUserService;

namespace Domain.Logic.Services
{
    /// <summary>
    /// Service for front user administration.
    /// </summary>
    public class FrontUserService : Service<FrontUser>, IFrontUserService
    {
        #region Properties

        private readonly UserManager<ApplicationUser> userManager;

        #endregion

        #region Constructor

        public FrontUserService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
        IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer, UserManager<ApplicationUser> userManager)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {
            this.userManager = userManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new user and returns the code that should be sent via email for the user to confirm their account.
        /// May throw an IdentityException if the User couldn't be registered.
        /// Check the IdentityException.Errors property for more details.
        /// </summary>
        public async Task<string> CreateAsync(FrontUser user, IEnumerable<string> roles)
        {
            // Register user without password.
            var createResult = await userManager.CreateAsync(user);

            if (!createResult.Succeeded)
                throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.CreateAsyncFailedEx], null, createResult.Errors.ToList()));

            // Set user roles
            var addRolesResult = await userManager.AddToRolesAsync(user, roles);

            if (!addRolesResult.Succeeded)
                throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.CreateAsyncAddRolesFailedEx], null, addRolesResult.Errors.ToList()));

            logger.LogTrace("Created a new account without password.");

            // Generate email confirmation code.
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

            return code;
        }

        /// <summary>
        /// Updates the user and their roles.
        /// May throw an IdentityException if the User couldn't be registered or the roles couldn't be set.
        /// Check the IdentityException.Errors property for more details.
        /// </summary>
        public async Task UpdateAsync(FrontUser user, IEnumerable<string> roles)
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
            var createResult = await userManager.UpdateAsync(currentUser);

            if (!createResult.Succeeded)
                throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.UpdateAsyncFailedEx], null, createResult.Errors.ToList()));

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

            logger.LogTrace("Updated front user.");
        }

        ///<summary>
        /// Get user by Id including all its dependencies.
        ///</summary>
        public async Task<FrontUser> GetFullAsync(Guid userId)
        {
            var user = await this.GetAll()
            .Where(u => u.Id == userId)
            .Include(u => u.Tenant)
            .FirstOrDefaultAsync();

            return user;
        }

        #endregion
    }
}