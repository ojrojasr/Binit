using Domain.Entities.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.DTOs.ProductDTOs.FeatureDTO;

namespace WebAPI.DTOs.ProductDTOs
{
    public class FeatureDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = Lang.NameRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.DescriptionRequired)]
        [StringLength(120, MinimumLength = 20, ErrorMessage = Lang.DescriptionStringLenth)]
        public string Description { get; set; }

        public FeatureDTO()
        {

        }

        public FeatureDTO(Feature feature)
        {
            this.Id = feature.Id.ToString();
            this.Name = feature.Name;
            this.Description = feature.Description;
        }

        public Feature ToEntity()
        {
            return new Feature()
            {
                Id = this.Id != null ? new Guid(this.Id) : new Guid(),
                Name = this.Name,
                Description = this.Description
            };
        }
    }
}