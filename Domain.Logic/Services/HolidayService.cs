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
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.Services.HolidayService;

namespace Domain.Logic.Services
{
    /// <summary>
    /// Holiday specific services.
    /// </summary>
    public class HolidayService : Service<Holiday>, IHolidayService
    {
        private readonly IService<HolidayType> holidayTypeService;

        public HolidayService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
            IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer, IService<HolidayType> holidayTypeService)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {
            this.holidayTypeService = holidayTypeService;
        }

        public Holiday GetFull(Guid id)
        {
            var holiday = base.GetAll()
                   .Where(p => p.Id == id)
                   .Include(p => p.Reason)
                   .Include(p => p.Users).ThenInclude(e => e.User)
                   .FirstOrDefault();

            if (holiday == null || holiday.Deleted)
                throw base.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.GetFullNotFoundEx]));

            return holiday;
        }

        public IQueryable<Holiday> GetFull()
        {
            var holidays = base.GetAll()
                   .Include(p => p.Reason)
                   .Include(p => p.Users).ThenInclude(e => e.User);

            return holidays;
        }

        public async Task<Holiday> GetFullAsync(Guid id)
        {
            var holiday = await base.GetAll()
                   .Where(p => p.Id == id)
                   .Include(p => p.Reason)
                   .Include(p => p.Users).ThenInclude(e => e.User)
                   .FirstOrDefaultAsync();

            if (holiday == null || holiday.Deleted)
                throw base.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.GetFullAsyncNotFoundEx]));

            return holiday;
        }

        /// <summary>
        /// Holiday DeleteAsync override.
        /// Deletes the holiday and all its dependant relationships.
        /// </summary>
        public override async Task DeleteAsync(Guid id)
        {
            // Gets holiday with all its relationships included.
            var holiday = await this.GetFullAsync(id);

            // Delete all users.
            holiday.Users.Clear();

            // Delete holiday.
            await base.DeleteAsync(holiday);
        }

        /// <summary>
        /// Holiday UpdateAsync override.
        /// Updates the holiday and all its dependant relationships.
        /// </summary>
        public override async Task UpdateAsync(Holiday holiday)
        {
            // Get current holiday from db.
            // Only include relationships that need to be auto-updated by EF Core.
            var dbHoliday = await base.GetAll()
                .Where(p => p.Id == holiday.Id)
                .Include(p => p.Users)
                .FirstOrDefaultAsync();

            // Set values from memory holiday to db tracked holiday.
            // This makes sure the changes to many-to-many relationships are applied and tracked by EF Core.
            holiday.CopyTo(dbHoliday);

            // Update holiday.
            await base.UpdateAsync(dbHoliday);
        }
    }
}