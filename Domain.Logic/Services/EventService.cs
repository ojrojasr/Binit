using Binit.Framework;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using DAL;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.Services.EventService;

namespace Domain.Logic.Services
{
    /// <summary>
    /// Event specific service.
    /// </summary>
    public class EventService : ServiceTenantDependents<Event>, IEventService
    {
        private readonly IService<IgniteAddress> igniteAddressService;
        public EventService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
            IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer, IService<IgniteAddress> igniteAddressService)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {
            this.igniteAddressService = igniteAddressService;
        }

        public Event GetFull(Guid id)
        {
            var item = base.GetAll()
                   .Where(e => e.Id == id)
                   .Include(e => e.Files).ThenInclude(ef => ef.File)
                   .Include(e => e.Location)
                   .FirstOrDefault();

            if (item == null || item.Deleted)
                throw base.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.GetFullNotFoundEx]));

            return item;
        }

        public async Task<Event> GetFullAsync(Guid id)
        {
            var item = await base.GetAll()
                   .Where(e => e.Id == id)
                   .Include(e => e.Files).ThenInclude(ef => ef.File)
                   .Include(e => e.Location)
                   .FirstOrDefaultAsync();

            if (item == null || item.Deleted)
                throw base.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.GetFullAsyncNotFoundEx]));

            return item;
        }

        /// <summary>
        /// Event CreateAsync override.
        /// Creates the event and all its dependant relationships.
        /// </summary>
        public async override Task CreateAsync(Event item)
        {
            if (item.Location != null)
            {
                var addressDb = this.igniteAddressService.GetAll().Where(a => a.Latitude == item.Location.Latitude && a.Longitude == item.Location.Longitude).FirstOrDefault();
                if (addressDb == null)
                {
                    // Create new address if it doesn't exists
                    await this.igniteAddressService.CreateAsync(item.Location);
                    item.LocationId = item.Location.Id;
                }
                else
                {
                    // Reuse the existing address if already exists
                    item.LocationId = addressDb.Id;
                }
            }

            // Create event.
            await base.CreateAsync(item);

        }

        /// <summary>
        /// Event DeleteAsync override.
        /// Deletes the event and all its dependant relationships.
        /// </summary>
        public override async Task DeleteAsync(Guid id)
        {
            // Gets event with all its relationships included.
            var item = await this.GetFullAsync(id);

            // Delete all files.
            item.Files.Clear();

            // Delete event.
            await base.DeleteAsync(item);
        }

        /// <summary>
        /// Event UpdateAsync override.
        /// Updates the event and all its dependant relationships.
        /// </summary>
        public override async Task UpdateAsync(Event item)
        {
            // Gets event from db.
            var dbEvent = await base.GetAll()
                .Where(p => p.Id == item.Id)
                .Include(p => p.Files)
                .FirstOrDefaultAsync();

            // Update address using the Ignite Address service.
            await this.UpdateAddress(item);

            // Set values from memory event to db tracked event.
            // This makes sure the changes to many-to-many relationships are applied and tracked by EF Core.
            item.CopyTo(dbEvent);

            // Update event.
            await base.UpdateAsync(dbEvent);
        }

        private async Task UpdateAddress(Event item)
        {
            if (item.Location != null)
            {
                var addressDb = this.igniteAddressService.GetAll()
                    .Where(a => a.Latitude == item.Location.Latitude &&
                                a.Longitude == item.Location.Longitude)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (addressDb == null)
                {
                    await this.igniteAddressService.CreateAsync(item.Location);
                    item.LocationId = item.Location.Id;
                }
                else
                {
                    item.LocationId = addressDb.Id;
                }
            }
            else
            {
                item.LocationId = null;
            }
        }
    }
}