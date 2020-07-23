using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    public interface IHolidayService : IService<Holiday>
    {
        Holiday GetFull(Guid id);
        IQueryable<Holiday> GetFull();
        Task<Holiday> GetFullAsync(Guid id);
    }
}
