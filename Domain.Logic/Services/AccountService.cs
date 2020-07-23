using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.Constants.DAL;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.Configuration;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using DAL;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ExceptionsLang = Binit.Framework.Localization.LocalizationConstants.BinitFramework.ExceptionHandling;
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.Services.AccountService;

namespace Domain.Logic.Services
{
    /// <summary>
    /// Service to manage user account.
    /// </summary>
    public class AccountService : Service<ApplicationUser>, IAccountService
    {
        #region Properties

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITenantService tenantService;
        private readonly IRealmConfiguration realmConfiguration;

        #endregion

        #region Constructor

        public AccountService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext, IUnitOfWork unitOfWork,
        IStringLocalizer<SharedResources> localizer, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
        ITenantService tenantService, IRealmConfiguration realmConfiguration)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.tenantService = tenantService;
            this.realmConfiguration = realmConfiguration;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the current user.
        /// Note: UserManager.GetUserAsync searchs the user by Id looking for the NameIdentifier Claim.
        /// The NameIdentifier Claim must be set and the user must be logged in for this method to return something other than null.
        /// </summary>        
        public async Task<ApplicationUser> GetUser()
        {
            return await this.GetUser(base.operationContext.GetUsername());
        }

        /// <summary>
        /// Returns the current user with Photo.
        /// The NameIdentifier Claim must be set and the user must be logged in for this method to return something other than null.
        /// </summary>
        public async Task<ApplicationUser> GetUserFull()
        {
            return await base.GetAll()
            .Where(u => u.Email == operationContext.GetUsername())
            .Include(u => u.Photo)
            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the user by email. Returns null if the user doesn't exist.
        /// </summary>        
        public async Task<ApplicationUser> GetUser(string email)
        {
            try
            {
                return await this.userManager.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Returns the roles of the given user.
        /// </summary>
        public async Task<ICollection<string>> GetRoles(ApplicationUser user)
        {
            try
            {
                return await this.userManager.GetRolesAsync(user);
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Logs the user in using email and password. Returns a SignInResult object.
        /// </summary>
        public async Task<SignInResult> Login(string username, string password, bool rememberMe)
        {
            try
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await this.signInManager.PasswordSignInAsync(username, password, rememberMe, lockoutOnFailure: true);

                // Log login result.
                if (result.Succeeded)
                {
                    // If the login succeded, check if the user was deleted.
                    var user = await this.userManager.FindByEmailAsync(username);
                    if (user.Deleted)
                        throw base.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.LoginUserNotFoundEx]));

                    logger.LogTrace("User logged in.");
                }
                else if (result.RequiresTwoFactor)
                    logger.LogTrace("User logged in.");
                else if (result.IsLockedOut)
                    logger.LogTrace("User logged in.");
                else
                    logger.LogTrace("User logged in.");

                return result;
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public async Task Logout()
        {
            try
            {
                await this.signInManager.SignOutAsync();
                this.logger.LogTrace("User logged out.");
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Returns the configured external authentication schemes.
        /// </summary>
        public async Task<IList<AuthenticationScheme>> GetExternalAuthenticationSchemes()
        {
            try
            {
                if (realmConfiguration.AllowSelfSignUp)
                {
                    return (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                }

                return new List<AuthenticationScheme>();
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Registers a new user and returns the code that should be sent via email for the user to confirm their account.
        /// May throw an IdentityException if the User couldn't be registered. Check the IdentityException.Errors property for more details.
        /// May throw NotFoundException if the Realm doesn't allow self sign up.
        /// </summary>
        public async Task<string> Register(ApplicationUser user, string password)
        {
            try
            {
                // If realm doesn't allow self sign up, throw NotFound exception.
                if (!realmConfiguration.AllowSelfSignUp)
                    throw this.exceptionManager.Handle(new NotFoundException(localizer[ExceptionsLang.ResourceNotFoundGenericEx]));

                // Check if password meets requirements.
                var passwordValidator = this.userManager.PasswordValidators.FirstOrDefault();
                bool passwordIsValid = (await passwordValidator.ValidateAsync(this.userManager, user, password)) == IdentityResult.Success;

                // Set default tenant.
                user.TenantId = Tenants.MasterId;

                if (!passwordIsValid)
                    throw this.exceptionManager.Handle(new ValidationException(this.localizer[Lang.RegisterInvalidPasswordEx]));

                // Set a sequential id.
                user.Id = SequentialGuidGenerator.NewSequentialGuid();

                // Register user with password.
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    logger.LogTrace("User created a new account with password.");

                    // Set user role.
                    var addRolesResult = await userManager.AddToRolesAsync(user, new List<string> {
                        Roles.FrontEventUser,
                        Roles.FrontHolidayUser,
                        Roles.FrontProductUser,
                        Roles.FrontCategoryUser,
                        Roles.FrontHolidayTypeUser,
                        Roles.BackofficePlayUser
                    });

                    if (addRolesResult.Succeeded)
                    {
                        // Add tenant as claim.
                        var claim = new Claim(CustomClaimTypes.Tenant, user.TenantId.ToString());
                        var addTenantResult = await this.userManager.AddClaimAsync(user, claim);

                        if (addTenantResult.Succeeded)
                        {
                            // Generate email confirmation code.
                            return await userManager.GenerateEmailConfirmationTokenAsync(user);
                        }
                        else
                        {
                            throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.RegisterFailedEx], null, addTenantResult.Errors.ToList()));
                        }
                    }
                    else
                    {
                        throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.RegisterFailedEx], null, addRolesResult.Errors.ToList()));
                    }
                }

                throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.RegisterFailedEx], null, result.Errors.ToList()));
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Validates the userId and code and sets the user email as confirmed.
        /// May throw an IdentityException if the email confirmation fails.
        /// Check the IdentityException.Errors property for more details.
        /// </summary>
        public async Task ConfirmEmail(string userId, string code)
        {
            try
            {
                var user = await this.userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    logger.LogTrace("User doesn't exist - Unable to confirm email");
                    throw this.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.ConfirmEmailUserNotFoundEx]));
                }

                var result = await this.userManager.ConfirmEmailAsync(user, code);
                if (!result.Succeeded)
                {
                    throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.ConfirmEmailFailedEx], null, result.Errors.ToList()));
                }
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Validates the userId and code and sets the user email as confirmed.
        /// Sets the user password.
        /// May throw an IdentityException if the email confirmation fails.
        /// Check the IdentityException.Errors property for more details.
        /// </summary>
        public async Task CreatePassword(string userId, string code, string newPassword)
        {
            try
            {
                var user = await this.userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    logger.LogTrace("User doesn't exist - Unable to confirm email");
                    throw this.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.CreatePasswordUserNotFoundEx]));
                }

                var resultConfirmation = await this.userManager.ConfirmEmailAsync(user, code);
                if (!resultConfirmation.Succeeded)
                {
                    throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.CreatePasswordConfirmEmailFailedEx], null, resultConfirmation.Errors.ToList()));
                }

                var resultSetPassword = await this.userManager.AddPasswordAsync(user, newPassword);

                if (!resultSetPassword.Succeeded)
                {
                    throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.CreatePasswordAddPasswordFailedEx], null, resultSetPassword.Errors.ToList()));
                }
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Generates a password token that should be used to allow the user to change their password (e.g: Forgot password scenario).
        /// Returns the generated token.
        /// May throw a NotFoundException if the provided email doesn't belong to an existent user or if the user hasn't confirmed their email yet.
        /// </summary>
        public async Task<string> GeneratePasswordToken(string email)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null || !(await userManager.IsEmailConfirmedAsync(user)))
                {
                    logger.LogTrace("User doesn't exist or must confirm email - Unable to generate password token");
                    throw this.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.GeneratePasswordTokenUserNotFoundEx]));
                }

                var code = await userManager.GeneratePasswordResetTokenAsync(user);

                logger.LogTrace("Password token generated");

                return code;
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Resets the user's password.
        /// The code must be one generated using the GeneratePasswordToken method.
        /// May throw a NotFoundException if the provided email doesn't belong to an existent user.
        /// May throw IdentityException if there's any issue resetting the password. Check the IdentityException.Errors property for more details.
        /// </summary>
        public async Task ResetPassword(string email, string code, string password)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    logger.LogTrace("User doesn't exist - Unable to reset password");
                    throw this.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.ResetPasswordUserNotFoundEx]));
                }

                var result = await this.userManager.ResetPasswordAsync(user, code, password);

                if (!result.Succeeded)
                    throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.ResetPasswordFailedEx], null, result.Errors.ToList()));

                logger.LogTrace("Password reset successful.");
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        public async Task<bool> UserHasPassword()
        {
            try
            {
                var user = await this.GetUser();
                return await this.userManager.HasPasswordAsync(user);
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        public async Task<bool> UserHasPassword(ApplicationUser user)
        {
            try
            {
                return await this.userManager.HasPasswordAsync(user);
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Changes the current user's password and refreshes the signed in account.
        /// May throw a NotFoundException if there's no user logged in.
        /// May throw IdentityException if there's any issue changing the password. Check the IdentityException.Errors property for more details.
        /// </summary>
        public async Task ChangePassword(string oldPassword, string newPassword)
        {
            try
            {
                // Gets current user.
                var user = await this.GetUser();

                if (user == null)
                    throw this.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.ChangePasswordUserNotFoundEx]));

                var result = await this.userManager.ChangePasswordAsync(user, oldPassword, newPassword);

                if (!result.Succeeded)
                {
                    throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.ChangePasswordFailedEx], null, result.Errors.ToList()));
                }

                await this.signInManager.RefreshSignInAsync(user);

                logger.LogTrace("User changed their password successfully.");
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        /// <summary>
        /// Sets the current user's password and refreshes the signed in account.
        /// This method should only be used when the user sets their password for the first time.
        /// May throw a NotFoundException if there's no user logged in.
        /// May throw IdentityException if there's any issue setting the password. Check the IdentityException.Errors property for more details.
        /// </summary>
        public async Task SetPassword(string newPassword)
        {
            try
            {
                // Gets current user.
                var user = await this.GetUser();

                if (user == null)
                    throw this.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.SetPasswordUserNotFoundEx]));

                var result = await this.userManager.AddPasswordAsync(user, newPassword);

                if (!result.Succeeded)
                {
                    throw this.exceptionManager.Handle(new IdentityException(this.localizer[Lang.SetPasswordFailedEx], null, result.Errors.ToList()));
                }

                await this.signInManager.RefreshSignInAsync(user);

                logger.LogTrace("User set their password successfully.");
            }
            catch (Exception ex)
            {
                throw exceptionManager.Handle(ex);
            }
        }

        public AuthenticationProperties GetExternalAuthenticationProperties(string provider, string externalLoginHandler)
        {
            return this.signInManager.ConfigureExternalAuthenticationProperties(provider, externalLoginHandler);
        }

        public async Task<bool> CanSignInAsync(ApplicationUser user, ExternalLoginInfo info)
        {
            var canSignIn = true;
            var dbUser = await this.userManager.FindByEmailAsync(user.Email);

            if (dbUser == null)
            {
                var createResult = await userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                    throw exceptionManager.Handle(new IdentityException(localizer[Lang.SocialAuthenticationFailedEx], null, createResult.Errors.ToList()));

                var addRolesResult = await userManager.AddToRolesAsync(user, new List<string> {
                            Roles.FrontEventUser,
                            Roles.FrontHolidayUser,
                            Roles.FrontProductUser,
                            Roles.FrontCategoryUser,
                            Roles.FrontHolidayTypeUser
                });
                if (!addRolesResult.Succeeded)
                    throw exceptionManager.Handle(new IdentityException(localizer[Lang.SocialAuthenticationFailedEx], null, addRolesResult.Errors.ToList()));

                var addLoginResult = await userManager.AddLoginAsync(user, info);
                if (!addLoginResult.Succeeded)
                    throw exceptionManager.Handle(new IdentityException(localizer[Lang.SocialAuthenticationFailedEx], null, addLoginResult.Errors.ToList()));
            }
            else
            {
                canSignIn &= await this.signInManager.CanSignInAsync(dbUser);

                if (canSignIn)
                {
                    var hasLogin = (await userManager.GetLoginsAsync(dbUser)).Any(loginInfo =>
                    {
                        return loginInfo.LoginProvider == info.LoginProvider && loginInfo.ProviderKey == info.ProviderKey;
                    });

                    if (!hasLogin)
                    {
                        var addLoginResult = await userManager.AddLoginAsync(dbUser, info);
                        if (!addLoginResult.Succeeded)
                            throw exceptionManager.Handle(new IdentityException(localizer[Lang.SocialAuthenticationFailedEx], null, addLoginResult.Errors.ToList()));
                    }
                }
            }

            return canSignIn;
        }

        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await this.signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<ApplicationUser> FindAsync(ExternalLoginInfo externalLoginInfo)
        {
            return await this.userManager.FindByLoginAsync(externalLoginInfo.LoginProvider, externalLoginInfo.ProviderKey);
        }

        public async Task<Claim> GetTenantClaim(ApplicationUser user)
        {
            var userTenantClaim = (await this.userManager.GetClaimsAsync(user)).Where(c => c.Type == CustomClaimTypes.Tenant);
            return userTenantClaim.FirstOrDefault();
        }

        #endregion
    }
}