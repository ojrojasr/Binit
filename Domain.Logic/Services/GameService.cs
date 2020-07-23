using Binit.Framework;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using DAL;
using Domain.Entities.Model;
using Domain.Entities.Model.Views;
using Domain.Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.Services.ThemeService;

namespace Domain.Logic.Services
{
    /// <summary>
    /// Product specific services.
    /// </summary>
    public class GameService : Service<Game>, IGameService
    {
        

        public GameService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
            IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {

        }

        /// <summary>
        /// Gets a product by Id including all of its relationships.
        /// </summary>
        public Game GetFull(Guid id)
        {
            Game game = base.GetAll()
                .Where(g => g.Id == id)
                .Include(g => g.Answers)
                   .FirstOrDefault();

            return game;
        }
        public IQueryable<Game> GetAllFull()
        {
            var game = base.GetAll()
                .Include(p => p.Theme)
                .Include(p => p.Answers)
                .ThenInclude(q => q.Question);
                   
            return game;
        }

        /// <summary>
        /// Asynchronously gets a product by Id including all of its relationships.
        /// </summary>
        public async Task<Game> GetFullAsync(Guid id, bool asNoTracking = false)
        {

            Game game = await base.GetAll(asNoTracking)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if (game == null)
                throw base.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.GetFullAsyncNotFoundEx]));

            return game;

        }

        /// <summary>
        /// Product UpdateAsync override.
        /// Updates the product and all its dependant relationships.
        /// </summary>
        public async override Task UpdateAsync(Game game)
        {
            // Get current product from db.
            // Only include relationships that need to be auto-updated by EF Core.
            var dbGame = await base.GetAll()
                .Where(t => t.Id == game.Id)
                .FirstOrDefaultAsync();


            // Set values from memory product to db tracked product.
            game.CopyTo(dbGame);

            // Update product.
            await base.UpdateAsync(dbGame);
        }
        public override async Task DeleteAsync(Guid id)
        {
            // Gets pelicula with all its relationships included.
            var game = await this.GetFullAsync(id);

            // Delete pelicula.
            await base.DeleteAsync(game);
        }

        public async Task<Guid> GetFirstId()
        {
            var firstThemeId = await this.GetAll().Select(t => t.Id).FirstAsync();
            return firstThemeId;
        }
    }
}