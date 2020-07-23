using Microsoft.Extensions.Configuration;

namespace Domain.Logic.ExternalServices.FileManager
{
    public class FileManagerConfiguration
    {
        public string BaseAddress { get; set; }
        public string UploadEndpoint { get; set; }
        public string UploadLargeEndpoint { get; set; }
        public string DownloadEndpoint { get; set; }
        public string AcceptedMimeTypes { get; set; }

        public FileManagerConfiguration Bind(IConfiguration Configuration)
        {
            Configuration.GetSection("FileManagerConfiguration").Bind(this);
            return this;
        }

    }
}