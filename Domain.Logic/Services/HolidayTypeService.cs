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
using System.Linq;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.Services.HolidayTypeService;

namespace Domain.Logic.Services
{
    public class HolidayTypeService : Service<HolidayType>, IService<HolidayType>
    {
        private readonly IHolidayService holidayService;
        public HolidayTypeService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
        IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer, IHolidayService holidayService)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {
            this.holidayService = holidayService;
        }

        /// <summary>
        /// Removes holiday type.
        /// May throw UserException if the holiday type is related to an existent holiday.
        /// </summary>
        public override void Delete(HolidayType entity)
        {
            // Check if the holiday type is related to a product.
            var isRelatedToHoliday = holidayService.GetAll().Where(h => h.ReasonId == entity.Id).Any();
            if (isRelatedToHoliday)
                throw base.exceptionManager.Handle(new UserException(this.localizer[Lang.DeleteWithRelatedProductEx]));

            // If it's not related, delete.
            base.Delete(entity);
        }

        /// <summary>
        /// Asynchronously removes holiday type.
        /// May throw UserException if the holiday type is related to an existent product.
        /// </summary>
        public async override Task DeleteAsync(HolidayType entity)
        {
            // Check if the holiday type is related to a product.
            var isRelatedToHoliday = await holidayService.GetAll().Where(h => h.ReasonId == entity.Id).AnyAsync();
            if (isRelatedToHoliday)
                throw base.exceptionManager.Handle(new UserException(this.localizer[Lang.DeleteAsyncWithRelatedProductEx]));

            // If it's not related, delete.
            await base.DeleteAsync(entity);
        }
    }
}