using Binit.Framework;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers.Email.DTOs;
using Binit.Framework.Interfaces.Email;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.DTOs.AccountDTOs;
using WebAPI.Helpers;
using WebAPITools;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.AccountController;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        #region Properties

        private readonly IAccountService accountService;
        private readonly TokenManager tokenManager;
        private readonly IEmailSender emailSender;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IConfiguration configuration;
        #endregion

        #region Constructor

        public AccountController(IAccountService accountService, TokenManager tokenManager, IEmailSender emailSender, IStringLocalizer<SharedResources> localizer, IConfiguration configuration)
        {
            this.accountService = accountService;
            this.tokenManager = tokenManager;
            this.emailSender = emailSender;
            this.localizer = localizer;
            this.configuration = configuration;
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Returns the available authentication schemes.
		/// Allow anonymous
        /// </summary>
        [HttpGet("authentication-schemes")]
        [AllowAnonymous]
        public async Task<IEnumerable<AuthenticationSchemeRes>> AuthenticationSchemes()
        {
            var authSchemes = await accountService.GetExternalAuthenticationSchemes();
            var authSchemesDTO = authSchemes.Select(s => new AuthenticationSchemeRes()
            {
                Name = s.Name,
                DisplayName = s.DisplayName
            });

            return authSchemesDTO;
        }

        /// <summary>
        /// Validates the user credentials and returns a valid JWT.
		/// Allow anonymous
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginReq login)
        {
            await this.HttpContext.SignOutAsync();

            // To ensure a clean login.
            await this.accountService.Logout();

            try
            {
                var result = await accountService.Login(login.Email, login.Password, login.RememberMe);

                if (result.Succeeded)
                {
                    // If the login was successful, generate and return the JWT.
                    var user = await accountService.GetUser(login.Email);
                    var roles = await accountService.GetRoles(user);
                    var tenantClaim = await accountService.GetTenantClaim(user);

                    return Ok(new LoginRes()
                    {
                        Token = tokenManager.GenerateToken(user, roles, tenantClaim)
                    });
                }
                if (result.RequiresTwoFactor)
                {
                    return Forbid(this.localizer[Lang.LoginRequires2FA]);
                }
                if (result.IsLockedOut)
                {
                    return Unauthorized(this.localizer[Lang.LoginAccountLocked]);
                }
                else
                {
                    return Unauthorized(this.localizer[Lang.LoginIncorrectCredentials]);
                }
            }
            catch (NotFoundException)
            {
                return Unauthorized(this.localizer[Lang.LoginIncorrectCredentials]);
            }
            catch (Exception)
            {
                // To avoid sending too much information about the error to the client.
                return StatusCode(500, this.localizer[Lang.LoginError]);
            }
        }


        /// <summary>
        /// Logs out the current user.
        /// </summary>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync();

            await this.accountService.Logout();

            return Ok();
        }

        /// <summary>
        /// Creates a new user with password and sends the Welcome email with a confirmation code.
		/// Allow anonymous
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterReq register)
        {
            try
            {
                var user = register.ToEntity();
                var existentUser = await accountService.GetUser(user.Email);
                if (existentUser != null)
                {
                    // Don't let a potential attacker know if a user is already registered.
                    return Ok();
                }

                var code = await accountService.Register(user, register.Password);

                var queryParams = new Dictionary<string, string>();
                queryParams.Add("userId", user.Id.ToString());
                queryParams.Add("code", code);

                var callbackUrl = QueryHelpers.AddQueryString(register.ConfirmEmailCallback, queryParams);

                await emailSender.SendEmailAsync(user.Email, this.localizer[Lang.RegisterEmailSubject],
                    new WelcomeDTO(configuration, localizer) { Name = user.Email, CallbackUrl = callbackUrl });

                return Ok();
            }
            catch (IdentityException idex)
            {
                // Throw new ValidationException to be handled by the ExceptionHandlerMiddleware.
                throw new ValidationException(idex.Message, idex);
            }
        }

        /// <summary>
        /// Confirms the user's email.
        /// This endpoint should be called when a user received the Welcome email and clicked on the link.
		/// Allow anonymous
        /// </summary>
        [HttpPost("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailReq confirmEmail)
        {
            try
            {
                await this.accountService.ConfirmEmail(confirmEmail.UserId, confirmEmail.Code);
                return Ok();
            }
            catch (NotFoundException)
            {
                // Don't reveal that the user does not exist.
                return Ok();
            }
            catch (IdentityException idex)
            {
                // Throw new ValidationException to be handled by the ExceptionHandlerMiddleware.
                throw new ValidationException(idex.Message, idex);
            }
        }

        /// <summary>
        /// Sends the Forgot password email with a recovery code.
		/// Allow anonymous
        /// </summary>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordReq forgotPassword)
        {
            try
            {
                var code = await accountService.GeneratePasswordToken(forgotPassword.Email);

                var queryParams = new Dictionary<string, string>();
                queryParams.Add("code", code);

                var callbackUrl = QueryHelpers.AddQueryString(forgotPassword.ForgotPasswordEmailCallback, queryParams);

                await emailSender.SendEmailAsync(forgotPassword.Email, this.localizer[Lang.ForgotPasswordEmailSubject],
                new ForgotPasswordDTO(configuration, localizer) { Name = forgotPassword.Email, CallbackUrl = callbackUrl });

                return Ok();
            }
            catch (NotFoundException)
            {
                // Don't reveal that the user does not exist.
                return Ok();
            }
        }

        /// <summary>
        /// Resets the user's password.
        /// This endpoint should be called when a user received the Forgot password email and clicked on the link.
		/// Allow anonymous
        /// </summary>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordReq resetPassword)
        {
            try
            {
                await this.accountService.ResetPassword(resetPassword.Email, resetPassword.Code, resetPassword.Password);

                return Ok();
            }
            catch (NotFoundException)
            {
                // Don't reveal that the user does not exist.
                return Ok();
            }
            catch (IdentityException idex)
            {
                // Throw new ValidationException to be handled by the ExceptionHandlerMiddleware.
                throw new ValidationException(idex.Message, idex);
            }
        }


        /// <summary>
        /// Changes the user's password.
        /// This endpoint should be called by an authorized user logged in with a manual account (user and password)
        /// who wants to change their current password.
        /// </summary>
        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordReq changePasswordReq)
        {
            try
            {
                await this.accountService.ChangePassword(changePasswordReq.OldPassword, changePasswordReq.NewPassword);

                return Ok();
            }
            catch (IdentityException idex)
            {
                // Throw new ValidationException to be handled by the ExceptionHandlerMiddleware.
                throw new ValidationException(idex.Message, idex);
            }
        }

        /// <summary>
        /// Sets the user's first password.
        /// This endpoint should be called by an authorized user that in with a social account 
        /// who wants to allow manual login (with user and password).
        /// </summary>
        [HttpPut("set-password")]
        [Authorize]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordReq setPasswordReq)
        {
            try
            {
                await this.accountService.SetPassword(setPasswordReq.NewPassword);

                return Ok();
            }
            catch (IdentityException idex)
            {
                // Throw new ValidationException to be handled by the ExceptionHandlerMiddleware.
                throw new ValidationException(idex.Message, idex);
            }
        }

        /// <summary>
        /// Confirms user email and sets their first password.
        /// This endpoint should be called when the user confirming their email was created by another user.
        /// </summary>
        [HttpPost("create-password")]
        public async Task<IActionResult> CreatePassword([FromBody] CreatePasswordReq createPasswordReq)
        {
            try
            {
                await this.accountService.CreatePassword(createPasswordReq.UserId, createPasswordReq.Code, createPasswordReq.NewPassword);

                return Ok();
            }
            catch (IdentityException idex)
            {
                // Throw new ValidationException to be handled by the ExceptionHandlerMiddleware.
                throw new ValidationException(idex.Message, idex);
            }
        }

        /// <summary>
        /// Do a challenge of social auth with the provider passed by param
        /// </summary>
        [HttpGet("social-authentication")]
        [AllowAnonymous]
        public IActionResult SocialAuthentication(string provider)
        {
            var externalLoginHandler = Url.Action(nameof(HandleExternalLogin));
            var authenticationProperties = this.accountService.GetExternalAuthenticationProperties(provider, externalLoginHandler);
            return Challenge(authenticationProperties, provider);
        }

        /// <summary>
        /// Handle challenge response
        /// </summary>
        [HttpGet("handle-external-login")]
        [AllowAnonymous]
        public async Task<IActionResult> HandleExternalLogin()
        {
            var externalLoginInfo = await this.accountService.GetExternalLoginInfoAsync();
            var externalLoginUser = await this.accountService.FindAsync(externalLoginInfo);

            var deeplink = "";
            if (externalLoginUser == null)
            {
                deeplink = DeeplinksHelper.GetDeeplinkForCompleteInformation(this.configuration, externalLoginInfo);
            }
            else
            {
                var roles = await accountService.GetRoles(externalLoginUser);
                var tenantClaim = await accountService.GetTenantClaim(externalLoginUser);
                var token = tokenManager.GenerateToken(externalLoginUser, roles, tenantClaim);
                deeplink = $"{DeeplinksHelper.GetURL(this.configuration)}/authorize-access/{token}";
            }

            return Redirect(deeplink);
        }

        /// <summary>
        /// Completes user's social registration
        /// </summary>
        [HttpPost("complete-social-auth")]
        [AllowAnonymous]
        public async Task<IActionResult> CompleteSocialAuth([FromBody] CompleteSocialAuthReq completeSocialAuthReq)
        {
            var user = completeSocialAuthReq.ToEntity();
            var info = new ExternalLoginInfo(
                principal: new ClaimsPrincipal(),
                loginProvider: completeSocialAuthReq.LoginProvider,
                providerKey: completeSocialAuthReq.ProviderKey,
                displayName: completeSocialAuthReq.LoginProvider
            );
            var canSignIn = await this.accountService.CanSignInAsync(user, info);

            if (canSignIn)
            {
                user = await accountService.FindAsync(info);
                var roles = await accountService.GetRoles(user);
                var tenantClaim = await accountService.GetTenantClaim(user);
                return Ok(new LoginRes()
                {
                    Token = tokenManager.GenerateToken(user, roles, tenantClaim)
                });
            }
            else
            {
                return StatusCode(500, this.localizer[Lang.LoginError]);
            }
        }

        #endregion
    }
}
