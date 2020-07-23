using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    public interface IFrontUserService : IService<FrontUser>
    {
        ///<summary>
        /// Asynchronously create front user.
        ///</summary>
        Task<string> CreateAsync(FrontUser user, IEnumerable<string> roles);

        ///<summary>
        /// Asynchronously update front user.
        ///</summary>
        Task UpdateAsync(FrontUser user, IEnumerable<string> roles);

        ///<summary>
        /// Get user by Id including all its dependencies.
        ///</summary>
        Task<FrontUser> GetFullAsync(Guid id);
    }
}
