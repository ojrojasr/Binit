using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers.Email.DTOs;
using Binit.Framework.Interfaces.Email;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs.ApplicationUserDTOs;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.UserController;


namespace WebAPI.Controllers
{
    [Authorize(Roles = Roles.BackofficeSuperAdministrator)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        #region Properties

        private readonly IUserService userService;
        private readonly IAccountService accountService;
        private readonly IEmailSender emailSender;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IConfiguration configuration;

        #endregion

        #region Constructor

        public UserController(IUserService userService, IAccountService accountService, IEmailSender emailSender, IStringLocalizer<SharedResources> localizer, IConfiguration configuration)
        {
            this.userService = userService;
            this.accountService = accountService;
            this.emailSender = emailSender;
            this.localizer = localizer;
            this.configuration = configuration;
        }

        #endregion

        #region Endpoints
        /// <summary>
        /// Get a list with all the users
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUserDTO>>> Get()
        {
            // Get all users.
            var users = await this.userService.GetAll().ToListAsync();

            // Return list of ApplicationUserDTO.
            return users.Select(p => new ApplicationUserDTO(p)).ToList();
        }

        /// <summary>
        /// Get a specific user by id
        /// </summary>
        /// <param name="id">Id of the user you want to get</param> 
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserDTO>> Get(string id)
        {
            // Get user by id.
            var user = await userService.GetAsync(new Guid(id));

            // Get user roles.
            var userRoles = await accountService.GetRoles(user);

            // Return ApplicationUserDTO.
            return Ok(new ApplicationUserDTO(user, userRoles.ToArray()));
        }

        /// <summary>
        /// Creates a new user without password and sends the Welcome email with a confirmation code.
        /// </summary>
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateUserReq createUserReq)
        {
            try
            {
                // Convert DTO to ApplicationUser Entity
                var user = createUserReq.ToApplicationUser();

                // Create user with roles and without password, and receive confirmation code.
                var code = await userService.CreateAsync(user, createUserReq.Roles);

                // Build callback URL for the confirmation email.
                var queryParams = new Dictionary<string, string>();
                queryParams.Add("userId", user.Id.ToString());
                queryParams.Add("code", code);

                var callbackUrl = QueryHelpers.AddQueryString(createUserReq.ConfirmEmailCallback, queryParams);

                // Send confirmation email.
                await emailSender.SendEmailAsync(user.Email, this.localizer[Lang.CreateWelcomeEmailSubject], new WelcomeDTO(configuration, localizer) { Name = user.Email, CallbackUrl = callbackUrl });

                return Ok();
            }
            catch (IdentityException idex)
            {
                // Throw new ValidationException to be handled by the ExceptionHandlerMiddleware.
                throw new ValidationException(idex.Message, idex);
            }
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id">Id of the user you want to update</param>
        /// <param name="userDTO">user you want to update</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ApplicationUserDTO userDTO)
        {
            userDTO.Id = id;

            await userService.UpdateAsync(userDTO.ToEntity(), userDTO.Roles);

            return Ok();
        }

        /// <summary>
        /// Deletes an existing user
        /// </summary>
        /// <param name="id">Id of the user you want to delete</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await userService.DeleteAsync(new Guid(id));

            return Ok();
        }

        /// <summary>
        /// Sends the password recovery email with a recovery callback link to the specified user.
        /// </summary>
        [HttpPost("password-recovery")]
        public async Task<IActionResult> PasswordRecovery([FromBody]PasswordRecoveryReq passwordRecoveryReq)
        {
            try
            {
                // Get the user's data.
                var user = await this.userService.GetAsync(new Guid(passwordRecoveryReq.Id));

                // Generate recovery code.
                var code = await accountService.GeneratePasswordToken(user.Email);

                // Generate recovery callback.
                var queryParams = new Dictionary<string, string>();
                queryParams.Add("code", code);

                var callbackUrl = QueryHelpers.AddQueryString(passwordRecoveryReq.RecoveryEmailCallback, queryParams);

                // Send email.
                await emailSender.SendEmailAsync(user.Email, this.localizer[Lang.PasswordRecoveryEmailSubject],
                new PasswordRecoveryDTO(configuration, localizer) { Name = user.Name, CallbackUrl = callbackUrl });

                return Ok();
            }
            catch (NotFoundException)
            {
                // Don't reveal that the user does not exist.
                return Ok();
            }
        }

        #endregion
    }
}
