using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using System;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    public interface IEventService : IServiceTenantDependent<Event>
    {
        Event GetFull(Guid id);
        Task<Event> GetFullAsync(Guid id);
    }
}
