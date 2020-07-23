using Binit.Framework;
using Binit.Framework.Helpers.Configuration;
using Binit.Framework.Helpers.Excel;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.DomainLogic.BusinessLogic.HolidayBusinessLogic;

namespace Domain.Logic.BusinessLogic
{
    public class HolidayBusinessLogic : IHolidayBusinessLogic
    {
        private readonly IHolidayService holidayService;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IConfiguration configuration;

        public HolidayBusinessLogic(IHolidayService holidayService, IStringLocalizer<SharedResources> localizer,
            IConfiguration configuration)
        {
            this.holidayService = holidayService;
            this.localizer = localizer;
            this.configuration = configuration;
        }

        /// <summary>
        /// Receives an excel file from the requests and inserts it's rows as Holiday objects.
        /// </summary>
        public async Task ImportExcel(IFormFile formFile)
        {
            var excelHelper = new ExcelHelper<Holiday>(this.localizer, this.configuration);

            // Extract holidays from file.
            List<Holiday> holidays = await excelHelper.Import(formFile);

            // Insert holidays in db.
            foreach (var holiday in holidays)
            {
                await holidayService.CreateAsync(holiday);
            }
        }

        /// <summary>
        /// Searches holidays and exports them as an excel file.
        /// If no search term is provided, returns all holidays.
        /// </summary>
        public async Task<ExportResult> ExportExcel(string searchTerm = null)
        {
            var excelHelper = new ExcelHelper<Holiday>(this.localizer, this.configuration);
            var locale = new LocaleConfiguration(this.configuration);

            var query = this.holidayService.GetFull();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(h =>
                            h.Name.Contains(searchTerm) ||
                            h.Description.Contains(searchTerm)
                        );
            }

            string fileDownloadName = string.Format(localizer[Lang.ExcelExportFilename], DateTime.Now.ToString(locale.DateTimeFormat));

            ExportResult exportResult = await excelHelper.Export(query, fileDownloadName);

            return exportResult;
        }
    }
}