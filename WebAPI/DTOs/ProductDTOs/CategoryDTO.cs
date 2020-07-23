using Domain.Entities.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.DTOs.CategoryDTOs.CategoryDTO;

namespace WebAPI.DTOs.CategoryDTOs
{
    public class CategoryDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = Lang.NameRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.DescriptionRequired)]
        [StringLength(120, MinimumLength = 20, ErrorMessage = Lang.DescriptionStringLenth)]
        public string Description { get; set; }

        public CategoryDTO()
        {

        }

        public CategoryDTO(Category category)
        {
            this.Id = category.Id.ToString();
            this.Name = category.Name;
            this.Description = category.Description;
        }

        public Category ToEntity()
        {
            var category = new Category()
            {
                Id = this.Id != null ? new Guid(this.Id) : new Guid(),
                Name = this.Name,
                Description = this.Description
            };

            return category;
        }
    }
}