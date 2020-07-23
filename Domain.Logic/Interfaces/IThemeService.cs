using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Domain.Entities.Model.Views;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    public interface IThemeService : IService<Theme>
    {
        Theme GetFull(Guid id);
        IQueryable<Theme> GetAllFull();
        Task<Guid> GetFirstId();
        Task<Theme> GetFullAsync(Guid id, bool asNoTracking = false);
        IQueryable<Theme> GetCompletedThemes();

    }
}
