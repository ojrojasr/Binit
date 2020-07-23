using Binit.Framework;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using DAL;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.Services.PeliculaService;

namespace Domain.Logic.Services
{
    /// <summary>
    /// Pelicula specific services.
    /// </summary>
    public class PeliculaService : Service<Pelicula>, IPeliculaService
    {

        private readonly IService<Curiosidad> curiosidadService;
        public PeliculaService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
            IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer, IService<Curiosidad> curiosidadService)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {
            this.curiosidadService = curiosidadService;
        }

        public Pelicula GetFull(Guid id)
        {
            var pelicula = base.GetAll()
                .Where(p => p.Id == id)
                   .Include(p => p.genero)
                   .Include(p => p.Curiosidades)
                   .Include(p => p.Actores).ThenInclude(a => a.Actor)
                   .FirstOrDefault();

            if (pelicula == null || pelicula.Deleted)
                throw base.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.GetFullNotFoundEx]));

            return pelicula;
        }

        public IQueryable<Pelicula> GetFull()
        {
            var peliculas = base.GetAll();

            return peliculas;
        }

        public async Task<Pelicula> GetFullAsync(Guid id)
        {
            var pelicula = await base.GetAll()
                .Where(p => p.Id == id)
                   .Include(p => p.genero)
                   .Include(p => p.Curiosidades)
                   .Include(p => p.Actores).ThenInclude(a => a.Actor)
                   .FirstOrDefaultAsync();

            if (pelicula == null || pelicula.Deleted)
                throw base.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.GetFullAsyncNotFoundEx]));

            return pelicula;
        }

        /// <summary>
        /// Pelicula DeleteAsync override.
        /// Deletes the pelicula and all its dependant relationships.
        /// </summary>
        public override async Task DeleteAsync(Guid id)
        {
            // Gets pelicula with all its relationships included.
            var pelicula = await this.GetFullAsync(id);

            // Delete pelicula.
            await base.DeleteAsync(pelicula);
        }

        /// <summary>
        /// Pelicula UpdateAsync override.
        /// Updates the pelicula and all its dependant relationships.
        /// </summary>
        public override async Task UpdateAsync(Pelicula pelicula)
        {
            // Get current pelicula from db.
            // Only include relationships that need to be auto-updated by EF Core.
            var dbPelicula = await base.GetAll()
                .Where(p => p.Id == pelicula.Id)
                .Include(p => p.genero)
                .Include(p => p.Actores)
                .FirstOrDefaultAsync();

            await this.UpdateCuriosidades(pelicula.Id, pelicula.Curiosidades);
            // Set values from memory pelicula to db tracked pelicula.
            // This makes sure the changes to many-to-many relationships are applied and tracked by EF Core.
            pelicula.CopyTo(dbPelicula);

            // Update pelicula.
            await base.UpdateAsync(dbPelicula);
        }

        private async Task UpdateCuriosidades(Guid peliculatId, List<Curiosidad> newCuriosidad)
        {
            var dbCuriosidad = this.curiosidadService.GetAll().Where(f => f.Pelicula.Id == peliculatId).AsNoTracking().ToList();

            // Create any features that exist in newFeatures but not in dbFeatures.
            foreach (var curiosidad in newCuriosidad.Except(dbCuriosidad, new EntityComparer<Curiosidad>()))
                await curiosidadService.CreateAsync(curiosidad);

            // Update any features that exist both in newFeatures and in dbFeatures
            foreach (var curiosidad in newCuriosidad.Intersect(dbCuriosidad, new EntityComparer<Curiosidad>()))
                await curiosidadService.UpdateAsync(curiosidad);

            // Remove any features that exist in dbFeatures but not in newFeatures.
            foreach (var curiosidad in dbCuriosidad.Except(dbCuriosidad, new EntityComparer<Curiosidad>()))
                await curiosidadService.DeleteAsync(curiosidad);
        }
    }
}