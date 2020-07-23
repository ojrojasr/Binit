using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Domain.Entities.Model.Views;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    public interface IGameService : IService<Game>
    {
        Game GetFull(Guid id);
        IQueryable<Game> GetAllFull();
        Task<Guid> GetFirstId();
        Task<Game> GetFullAsync(Guid id, bool asNoTracking = false);


    }
}
