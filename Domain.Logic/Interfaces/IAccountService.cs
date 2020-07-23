using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    public interface IAccountService : IService<ApplicationUser>
    {
        /// <summary>
        /// Changes Password async from old to newone
        /// </summary>
        Task ChangePassword(string oldPassword, string newPassword);
        Task ConfirmEmail(string userId, string code);
        Task CreatePassword(string userId, string code, string newPassword);
        Task<string> GeneratePasswordToken(string email);
        Task<IList<AuthenticationScheme>> GetExternalAuthenticationSchemes();
        Task<ICollection<string>> GetRoles(ApplicationUser user);
        Task<ApplicationUser> GetUser();
        Task<ApplicationUser> GetUserFull();
        Task<ApplicationUser> GetUser(string email);
        Task<SignInResult> Login(string username, string password, bool rememberMe);
        Task Logout();
        Task<string> Register(ApplicationUser user, string password);
        Task ResetPassword(string email, string code, string password);
        Task SetPassword(string newPassword);
        Task<bool> UserHasPassword();
        Task<bool> UserHasPassword(ApplicationUser user);
        AuthenticationProperties GetExternalAuthenticationProperties(string provider, string externalLoginHandler);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<bool> CanSignInAsync(ApplicationUser applicationUser, ExternalLoginInfo info);
        Task<ApplicationUser> FindAsync(ExternalLoginInfo externalLoginInfo);
        Task<Claim> GetTenantClaim(ApplicationUser user);
    }
}
