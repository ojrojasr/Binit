using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly IFileManagerService fileManagerService;
        private readonly IService<IgniteFile> igniteFileService;

        public FileController(IFileManagerService fileManagerService, IService<IgniteFile> igniteFileService)
        {
            this.fileManagerService = fileManagerService;
            this.igniteFileService = igniteFileService;
        }

        public async Task<IActionResult> Display(string id)
        {
            try
            {
                var file = await this.igniteFileService.GetAsync(new Guid(id));
                if (file.IsImage())
                    return await this.fileManagerService.Download(file);

                // Return generic file icon
                return File("~/images/file-icons/file-other.png", "image/png");
            }
            catch (Exception)
            {
                return File("~/images/file-icons/file-not-found.png", "image/png");
            }
        }

        public async Task<IActionResult> Download(string id)
        {
            var file = await this.igniteFileService.GetAsync(new Guid(id));
            return await this.fileManagerService.Download(file);
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            string message = "";
            try
            {
                var file = this.Request.Form.Files.FirstOrDefault();
                var uploadedFile = await this.fileManagerService.Upload(file);

                await this.igniteFileService.CreateAsync(uploadedFile);

                return Json(new FileViewModel(uploadedFile));

            }
            catch (ExternalServiceException ex)
            {
                this.HttpContext.Response.StatusCode = (int)ex.StatusCode;
                message = ex.Message;
            }
            catch (UserException ex)
            {
                this.HttpContext.Response.StatusCode = 500;
                message = ex.Message;
            }
            catch (Exception)
            {
                this.HttpContext.Response.StatusCode = 500;
                message = "Error uploading file";
            }

            return Json(new
            {
                message = message
            });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            string message = "";
            try
            {
                await this.igniteFileService.DeleteAsync(new Guid(id));
            }
            catch (ExternalServiceException ex)
            {
                this.HttpContext.Response.StatusCode = (int)ex.StatusCode;
                message = ex.Message;
            }
            catch (UserException ex)
            {
                this.HttpContext.Response.StatusCode = 500;
                message = ex.Message;
            }
            catch (Exception)
            {
                this.HttpContext.Response.StatusCode = 500;
                message = "Error uploading file";
            }

            return Json(new
            {
                message = message
            });
        }
    }
}