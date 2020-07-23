using Domain.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.EventViewModel;

namespace WebApp.Models
{
    public class EventViewModel : EntityViewModel
    {
        [Required(ErrorMessage = Lang.NameRequired)]
        [Display(Name = Lang.NameLabel)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.DescriptionRequired)]
        [Display(Name = Lang.DescriptionLabel)]
        [StringLength(120, ErrorMessage = Lang.DescriptionStringLength, MinimumLength = 20)]
        public string Description { get; set; }

        [Display(Name = Lang.DurationLabel)]
        public double Duration { get; set; }

        [Required(ErrorMessage = Lang.DateRequired)]
        [Display(Name = Lang.DateLabel)]
        public string Date { get; set; }

        [Display(Name = Lang.FilesLabel)]
        public List<FileViewModel> Files { get; set; }

        [Display(Name = Lang.FilesLabel)]
        public List<string> FilesIds { get; set; }

        [Required]
        [Display(Name = Lang.LocationLabel)]
        public AddressViewModel Location { get; set; }

        public string LocationId { get; set; }

        public EventViewModel()
        {
            this.Files = new List<FileViewModel>();
            this.FilesIds = new List<string>();
            this.Date = DateTime.Now.ToString("dd/MM/yyyy");
        }

        public EventViewModel(Event eventEntity)
        {
            this.Id = eventEntity.Id.ToString();
            this.Name = eventEntity.Name;
            this.Description = eventEntity.Description;
            this.Duration = eventEntity.Duration;
            this.Date = eventEntity.Date.ToString("dd/MM/yyyy");

            if (eventEntity.LocationId != null)
            {
                this.LocationId = eventEntity.LocationId.ToString();
                this.Location = new AddressViewModel(eventEntity.Location);
            }

            this.Files = new List<FileViewModel>();
            if (eventEntity.Files != null && eventEntity.Files.Count > 0)
            {
                foreach (var file in eventEntity.Files)
                {
                    this.Files.Add(new FileViewModel(file.File));
                }
            }
        }

        public Event ToEntity()
        {
            var item = new Event()
            {
                Id = this.Id != null ? new Guid(this.Id) : Guid.Empty,
                Name = this.Name,
                Description = this.Description,
                Duration = this.Duration,
                Date = DateTime.ParseExact(this.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };

            item.Files = new List<EventFile>();

            // Set event files
            if (this.FilesIds != null && this.FilesIds.Count > 0)
            {
                foreach (var eventFileId in this.FilesIds)
                {
                    item.Files.Add(new EventFile()
                    {
                        EventId = item.Id,
                        FileId = new Guid(eventFileId)
                    });
                }
            }

            // Set Localition Address
            if (this.Location != null && this.Location.Latitude != 0 && this.Location.Longitude != 0)
            {
                item.Location = this.Location.ToEntity();
            }

            return item;
        }
    }
}