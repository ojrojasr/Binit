using Binit.Framework.Helpers.Excel;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    public interface IHolidayBusinessLogic
    {
        /// <summary>
        /// Receives an excel file from the requests and inserts it's rows in the database as Holiday objects.
        /// </summary>
        Task ImportExcel(IFormFile formFile);

        /// <summary>
        /// Searches holidays by an (optional) searchTerm and exports them as an excel file.
        /// </summary>
        Task<ExportResult> ExportExcel(string searchTerm = null);
    }
}