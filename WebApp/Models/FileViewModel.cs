using Domain.Entities.Model;

namespace WebApp.Models
{
    public class FileViewModel
    {

        public FileViewModel() { }

        public FileViewModel(IgniteFile file)
        {
            this.Id = file.Id.ToString();
            this.Extension = file.Extension;
            this.Filename = file.Filename;
        }
        public string Id { get; set; }

        public string Extension { get; set; }

        public string Filename { get; set; }
    }
}