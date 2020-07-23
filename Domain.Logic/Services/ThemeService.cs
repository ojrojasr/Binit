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
    public class ThemeService : Service<Theme>, IThemeService
    {
        private readonly IService<Question> questionService;

        public ThemeService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
            IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer, IService<Question> questionService)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {
            this.questionService = questionService;
        }

        /// <summary>
        /// Gets a product by Id including all of its relationships.
        /// </summary>
        public Theme GetFull(Guid id)
        {
            Theme theme = base.GetAll()
                .Where(p => p.Id == id)
                .Include(p => p.Questions).ThenInclude(a => a.Answers)
                   .FirstOrDefault();

            return theme;
        }
        public IQueryable<Theme> GetAllFull()
        {
            var theme = base.GetAll()
                .Include(p => p.Questions)
                .ThenInclude(q => q.Answers);

            return theme;
        }

        /// <summary>
        /// Asynchronously gets a product by Id including all of its relationships.
        /// </summary>
        public async Task<Theme> GetFullAsync(Guid id, bool asNoTracking = false)
        {

            Theme theme = await base.GetAll(asNoTracking)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if (theme == null)
                throw base.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.GetFullAsyncNotFoundEx]));

            return theme;

        }

        /// <summary>
        /// Product UpdateAsync override.
        /// Updates the product and all its dependant relationships.
        /// </summary>
        public async override Task UpdateAsync(Theme theme)
        {
            // Get current product from db.
            // Only include relationships that need to be auto-updated by EF Core.
            var dbTheme = await base.GetAll()
                .Where(t => t.Id == theme.Id)
                .FirstOrDefaultAsync();


            // Set values from memory product to db tracked product.
            theme.CopyTo(dbTheme);

            // Update product.
            await base.UpdateAsync(dbTheme);
        }

        public async override Task DeleteAsync(Theme entity)
        {
            // Check if the holiday type is related to a product.
            var isRelatedToGame = await questionService.GetAll().Where(h => h.ThemeId == entity.Id).AnyAsync();
            if (isRelatedToGame)
                throw base.exceptionManager.Handle(new UserException(this.localizer[Lang.DeleteAsyncWithRelatedThemeEx]));

            // If it's not related, delete.
            await base.DeleteAsync(entity);
        }

        public IQueryable<Theme> GetCompletedThemes()
        {
            var completedThemes = base.GetAll()
                .Where(t => t.Questions.Count() >= t.QuestionQuantity);
            return completedThemes;
        }


        public async Task<Guid> GetFirstId()
        {
            var firstThemeId = await this.GetAll().Select(t => t.Id).FirstAsync();
            return firstThemeId;
        }
    }
}