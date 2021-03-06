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
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.Services.GeneroService;

namespace Domain.Logic.Services
{
    public class GeneroService : Service<Genero>, IService<Genero>
    {
        private readonly IPeliculaService peliculaService;
        public GeneroService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
        IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer, IPeliculaService peliculaService)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {
            this.peliculaService = peliculaService;
        }

        /// <summary>
        /// Removes holiday type.
        /// May throw UserException if the holiday type is related to an existent holiday.
        /// </summary>
        public override void Delete(Genero entity)
        {
            // Check if the holiday type is related to a product.
            var isRelatedToHoliday = peliculaService.GetAll().Where(h => h.generoId == entity.Id).Any();
            if (isRelatedToHoliday)
                throw base.exceptionManager.Handle(new UserException(this.localizer[Lang.DeleteWithRelatedPeliculaEx]));

            // If it's not related, delete.
            base.Delete(entity);
        }

        /// <summary>
        /// Asynchronously removes holiday type.
        /// May throw UserException if the holiday type is related to an existent product.
        /// </summary>
        public async override Task DeleteAsync(Genero entity)
        {
            // Check if the holiday type is related to a product.
            var isRelatedToPelicula = await peliculaService.GetAll().Where(h => h.generoId == entity.Id).AnyAsync();
            if (isRelatedToPelicula)
                throw base.exceptionManager.Handle(new UserException(this.localizer[Lang.DeleteAsyncWithRelatedPeliculaEx]));

            // If it's not related, delete.
            await base.DeleteAsync(entity);
        }
    }
}