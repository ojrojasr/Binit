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
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.Services.QuestionService;

namespace Domain.Logic.Services
{
    /// <summary>
    /// Pelicula specific services.
    /// </summary>
    public class QuestionService : Service<Question>, IQuestionService
    {

        private readonly IService<Answer> answerService;
        public QuestionService(IExceptionManager exceptionManager, ILogger logger, IOperationContext operationContext,
            IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer, IService<Answer> answerService)
            : base(exceptionManager, logger, operationContext, unitOfWork, localizer)
        {
            this.answerService = answerService;
        }

        public Question GetFull(Guid id)
        {
            var question = base.GetAll()
                .Where(p => p.Id == id)
                    .Include(p => p.Theme)
                   .Include(p => p.Answers)
                   .FirstOrDefault();

            if (question == null || question.Deleted)
                throw base.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.GetFullNotFoundEx]));

            return question;
        }

        public Question GetQuestion(Guid id)
        {
            var question = base.GetAll()
                .Where(p => p.Answers.Select(a => a.Id).Contains(id))
                    .Include(p => p.Answers)
                    .Include(p => p.Theme)
                   .FirstOrDefault();

            if (question == null || question.Deleted)
                throw base.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.GetFullNotFoundEx]));

            return question;
        }
        public IQueryable<Question> GetAllFull()
        {
            var questions = base.GetAll()
                   .Include(q => q.Answers);

            return questions;
        }

        public async Task<Question> GetFullAsync(Guid id)
        {
            var question = await base.GetAll()
                .Where(p => p.Id == id)
                    .Include(p => p.Theme)
                   .Include(p => p.Answers)
                   .FirstOrDefaultAsync();

            if (question == null || question.Deleted)
                throw base.exceptionManager.Handle(new NotFoundException(this.localizer[Lang.GetFullAsyncNotFoundEx]));

            return question;
        }

        /// <summary>
        /// Pelicula DeleteAsync override.
        /// Deletes the pelicula and all its dependant relationships.
        /// </summary>
        public override async Task DeleteAsync(Guid id)
        {
            // Gets pelicula with all its relationships included.
            var question = await this.GetFullAsync(id);

            // Delete pelicula.
            await base.DeleteAsync(question);
        }

        /// <summary>
        /// Pelicula UpdateAsync override.
        /// Updates the pelicula and all its dependant relationships.
        /// </summary>
        public override async Task UpdateAsync(Question question)
        {
            await this.UpdateQuestion(question.Id, question.Answers);
            // Update pelicula.
            await base.UpdateAsync(question);
        }

        private async Task UpdateQuestion(Guid questiontId, List<Answer> newAnswer)
        {
            var dbQuestion = this.answerService.GetAll().Where(f => f.Question.Id == questiontId).AsNoTracking().ToList();

            // Create any features that exist in newFeatures but not in dbFeatures.
            foreach (var answer in newAnswer.Except(dbQuestion, new EntityComparer<Answer>()))
                await answerService.CreateAsync(answer);

            // Update any features that exist both in newFeatures and in dbFeatures
            foreach (var answer in newAnswer.Intersect(dbQuestion, new EntityComparer<Answer>()))
                await answerService.UpdateAsync(answer);

            // Remove any features that exist in dbFeatures but not in newFeatures.
            foreach (var answer in dbQuestion.Except(newAnswer, new EntityComparer<Answer>()))
                await answerService.DeleteAsync(answer);
        }
    }
}