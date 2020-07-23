using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Domain.Logic.ExternalServices.FileManager
{
    public class FileManagerService : IFileManagerService
    {
        private readonly IHttpClientFactory clientFactory;

        private readonly FileManagerConfiguration fileManagerConfiguration;

        public FileManagerService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.fileManagerConfiguration = new FileManagerConfiguration().Bind(configuration);
        }

        public async Task<IgniteFile> Upload(IFormFile formFile)
        {
            using (var client = this.clientFactory.CreateClient())
            {
                string endpoint = UriHelper.Combine(this.fileManagerConfiguration.BaseAddress, this.fileManagerConfiguration.UploadEndpoint);

                HttpResponseMessage result;
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(formFile.OpenReadStream())
                    {
                        Headers =
                        {
                            ContentLength = formFile.Length,
                            ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(formFile.ContentType)
                        }
                    }, "formFile", formFile.FileName);

                    result = await client.PostAsync(endpoint, content);
                }

                if (!result.IsSuccessStatusCode)
                {
                    if (result.StatusCode == HttpStatusCode.BadRequest)
                    {
                        throw new ValidationException(result.Content.ToString());
                    }

                    throw new ExternalServiceException("There was an error uploading the file", result.Content.ToString(), result.StatusCode);
                }

                // We'll temporarily use Newtonsoft for json serialization until mechanism to handle circular references is fixed for System.Text.Json.Serialization
                // Tracking code: 3.0.0-issue1
                var fileDTO = JsonConvert.DeserializeObject<IgniteFileDTO>(await result.Content.ReadAsStringAsync());
                return fileDTO.ToEntity();
            }
        }

        public async Task<FileStreamResult> Download(IgniteFile file)
        {
            using (var client = this.clientFactory.CreateClient())
            {
                string endpoint = UriHelper.Combine(this.fileManagerConfiguration.BaseAddress,
                    string.Format(this.fileManagerConfiguration.DownloadEndpoint, file.FileId));

                var stream = await client.GetStreamAsync(endpoint);

                return new FileStreamResult(stream, new MediaTypeHeaderValue(file.MimeType));
            }
        }
    }
}