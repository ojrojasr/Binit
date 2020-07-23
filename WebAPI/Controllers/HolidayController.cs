using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.Helpers.Excel;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Attributes;
using WebAPI.DTOs.HolidayDTOs;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {

        #region Properties

        private readonly IHolidayService holidayService;
        private readonly IHolidayBusinessLogic holidayBusinessLogic;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IConfiguration configuration;

        #endregion

        #region Constructor

        public HolidayController(IHolidayService holidayService, IHolidayBusinessLogic holidayBusinessLogic,
            IStringLocalizer<SharedResources> localizer, IConfiguration configuration)
        {
            this.holidayService = holidayService;
            this.holidayBusinessLogic = holidayBusinessLogic;
            this.localizer = localizer;
            this.configuration = configuration;
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Get a list with all the Holidays
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HolidayDTO>>> Get()
        {
            var holidays = await this.holidayService.GetAll().ToListAsync();

            return holidays.Select(p => new HolidayDTO(p)).ToList();
        }

        /// <summary>
        /// Get a specific Holiday by id
        /// </summary>
        /// <param name="id">Id of the Holiday you want to get</param>        
        [HttpGet("{id}")]
        public async Task<ActionResult<HolidayDTO>> Get(string id)
        {
            var holiday = await holidayService.GetAsync(new Guid(id));
            return Ok(new HolidayDTO(holiday));
        }

        /// <summary>
        /// Creates a new Holiday.
        /// Only available for Backoffice Administrators
        /// </summary>
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HolidayDTO holidayDTO)
        {
            Holiday holiday = holidayDTO.ToEntity();
            await this.holidayService.CreateAsync(holiday);

            return Ok();
        }

        /// <summary>
        /// Updates an existing Holiday.
        /// Only available for Backoffice Administrators
        /// </summary>
        /// <param name="id">Id of the Holiday you want to update</param>
        /// <param name="holidayDTO">Holiday you want to update</param>
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayAdministrator)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] HolidayDTO holidayDTO)
        {
            holidayDTO.Id = id;

            await holidayService.UpdateAsync(holidayDTO.ToEntity());

            return Ok();
        }

        /// <summary>
        /// Deletes an existing Holiday.
        /// Only available for Backoffice Administrators
        /// </summary>
        /// <param name="id">Id of the Holiday you want to delete</param>
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayAdministrator)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await holidayService.DeleteAsync(new Guid(id));

            return Ok();
        }

        /// <summary>
        /// Imports a list of holidays from an excel file.
        /// </summary>
        [HttpPost("excel-import")]
        public async Task<IActionResult> Import(IFormFile formFile)
        {
            await this.holidayBusinessLogic.ImportExcel(formFile);

            return Ok();
        }

        /// <summary>
        /// Exports a list of holidays into an excel file.
        /// </summary>
        [HttpGet("excel-export")]
        public async Task<IActionResult> Export()
        {
            ExportResult exportResult = await this.holidayBusinessLogic.ExportExcel();

            return File(exportResult.Stream, exportResult.ExportMimeType, exportResult.Filename);
        }

        #endregion
    }
}
