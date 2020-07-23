using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    public interface IBackOfficeUserService : IService<BackOfficeUser>
    {
        ///<summary>
        /// Asynchronously create backOffice user.
        ///</summary>
        Task<string> CreateAsync(BackOfficeUser user, IEnumerable<string> roles);

        ///<summary>
        /// Asynchronously update backOffice user.
        ///</summary>
        Task UpdateAsync(BackOfficeUser user, IEnumerable<string> roles);

        ///<summary>
        /// Get user by Id including all its dependencies.
        ///</summary>
        Task<BackOfficeUser> GetFullAsync(Guid id);
    }
}
