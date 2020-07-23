using Binit.Framework;
using Binit.Framework.Constants.DAL;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using DAL;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.Services.ActorService;

namespace Domain.Logic.Services
{
    /// <summary>
    /// Service for actor administration.
    /// </summary>
    public class ActorService : Service<Actor>, IActorService
    {
        private readonly IPeliculaService peliculaService;

        public ActorService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
        IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer, IPeliculaService peliculaService)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {
            this.peliculaService = peliculaService;
        }

        

        #region Methods

  
        public async Task<List<Actor>> SearchActorsAsync(string searchTerm)
        {
            var caseInsensitiveSearchTerm = $"%{searchTerm}%";

            return await base.GetAll()
            .Where(u => EF.Functions.Like(u.Name, caseInsensitiveSearchTerm))
            .ToListAsync();
        }

        public async Task<List<Actor>> GetMany(List<string> ids)
        {
            return await base.GetAll()
            .Where(u => ids.Contains(u.Id.ToString()))
            .ToListAsync();
        }

        public async override Task DeleteAsync(Actor entity)
        {
            // Check if the holiday type is related to a product.
            var isRelatedToPelicula = await peliculaService.GetAll().Where(h => h.Actores.Any(a => a.ActorId == entity.Id)).AnyAsync();
            if (isRelatedToPelicula)
                throw base.exceptionManager.Handle(new UserException(this.localizer[Lang.DeleteAsyncWithRelatedPeliculaEx]));

            // If it's not related, delete.
            await base.DeleteAsync(entity);
        }

        #endregion
    }
}