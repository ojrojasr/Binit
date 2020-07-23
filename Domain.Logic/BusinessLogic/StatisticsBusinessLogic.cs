using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.Helpers.Configuration;
using Binit.Framework.Helpers.Excel;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Domain.Logic.DTOs;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.BusinessLogic.StatisticsBusinessLogic;

namespace Domain.Logic.BusinessLogic
{
    public class StatisticsBusinessLogic : IStatisticsBusinessLogic
    {
        private readonly IGameService gameService;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IConfiguration configuration;
        private readonly IOperationContext operationContext;

        public StatisticsBusinessLogic(IGameService gameService, IOperationContext operationContext, IStringLocalizer<SharedResources> localizer,
            IConfiguration configuration)
        {
            this.gameService = gameService;
            this.localizer = localizer;
            this.configuration = configuration;
            this.operationContext = operationContext;
        }

        public List<CardDTO> GetCardDTO(Guid userId)
        {
            var cards = new List<CardDTO>();

            IQueryable<Game> games = gameService.GetAllFull();

            if (!this.operationContext.UserIsInRole(Roles.BackofficeSuperAdministrator))
                games = games.Where(g => g.UserId == userId);

            var totalGames = games.Where(g => g.UserId == userId).Count().ToString();
            var finishedGames = games.Where(g => g.UserId == userId && g.Ended).Count().ToString();
            int correctTotal = games.Where(g => g.UserId == userId).Sum(g => g.CorrectQuantity);
            int totalAnswers = games.SelectMany(g => g.Answers).Distinct().Count();

            double porcentage = 0;
            if (totalAnswers != 0){
                porcentage = (correctTotal * 100) / totalAnswers;
            }
            cards.Add(new CardDTO
            {
                Title = localizer[Lang.TotalGames],
                Data = totalGames,
            });

            cards.Add(new CardDTO
            {
                Title = localizer[Lang.Finished],
                Data = finishedGames,
            });

            cards.Add(new CardDTO
            {
                Title = localizer[Lang.CorrectAnswers],
                Data = porcentage.ToString() + '%',
            });

            return cards;
        }

    }
}