using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    public interface IUserService : IService<ApplicationUser>
    {
        ///<summary>
        /// Asynchronously create user.
        ///</summary>
        Task<string> CreateAsync(ApplicationUser user, IEnumerable<string> roles);

        ///<summary>
        /// Asynchronously update user.
        ///</summary>
        Task UpdateAsync(ApplicationUser user, IEnumerable<string> roles);

        ///<summary>
        /// Search users that match the provided term.
        ///</summary>
        Task<List<ApplicationUser>> SearchUsersAsync(string searchTerm);

        ///<summary>
        /// Get many users by id.
        ///</summary>
        Task<List<ApplicationUser>> GetMany(List<string> ids);

        ///<summary>
        /// Get user by Id including all its dependencies.
        ///</summary>
        Task<ApplicationUser> GetFullAsync(Guid id);
    }
}
