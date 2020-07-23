using Domain.Entities.Model;
using Newtonsoft.Json;

namespace Domain.Logic.ExternalServices.FileManager
{
    public class IgniteFileDTO
    {
        public string OriginalFilename { get; set; }

        public string Extension { get; set; }

        public string MimeType { get; set; }

        public long Size { get; set; }

        // We'll temporarily use Newtonsoft for json serialization until mechanism to handle circular references is fixed for System.Text.Json.Serialization
        // Tracking code: 3.0.0-issue1
        [JsonProperty("Id")]
        public string FileId { get; set; }

        public IgniteFileDTO() { }

        public IgniteFileDTO(IgniteFile file)
        {
            this.OriginalFilename = file.Filename;
            this.Extension = file.Extension;
            this.MimeType = file.MimeType;
            this.Size = file.Size;
            this.FileId = file.FileId.ToString();
        }

        public IgniteFile ToEntity()
        {
            return new IgniteFile()
            {
                Filename = this.OriginalFilename,
                MimeType = this.MimeType,
                Extension = this.Extension,
                Size = this.Size,
                FileId = this.FileId
            };
        }
    }

}