using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    public interface IActorService : IService<Actor>
    {

        ///<summary>
        /// Search users that match the provided term.
        ///</summary>
        Task<List<Actor>> SearchActorsAsync(string searchTerm);

        ///<summary>
        /// Get many users by id.
        ///</summary>
        Task<List<Actor>> GetMany(List<string> ids);

    }
}
