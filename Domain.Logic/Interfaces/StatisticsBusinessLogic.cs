using Binit.Framework.Helpers.Excel;
using Domain.Logic.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    public interface IStatisticsBusinessLogic
    {
        public List<CardDTO> GetCardDTO(Guid userId);
    }
}