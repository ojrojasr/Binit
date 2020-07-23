using Domain.Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Domain.Logic.Interfaces
{
    public interface IFileManagerService
    {
        Task<IgniteFile> Upload(IFormFile formFile);
        Task<FileStreamResult> Download(IgniteFile file);
    }
}
