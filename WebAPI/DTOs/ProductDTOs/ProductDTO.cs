using Domain.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.DTOs.ProductDTOs.ProductDTO;

namespace WebAPI.DTOs.ProductDTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }

        public string CreatedAt { get; set; }

        [Required(ErrorMessage = Lang.NameRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.DescriptionRequired)]
        [StringLength(120, MinimumLength = 20, ErrorMessage = Lang.DescriptionStringLenth)]
        public string Description { get; set; }

        [Required(ErrorMessage = Lang.PriceRequired)]
        public string Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string CategoryId { get; set; }

        public ICollection<FeatureDTO> Features { get; set; }

        public ICollection<ProductEditorDTO> Editors { get; set; }

        public ProductDTO()
        {

        }

        public ProductDTO(Product product)
        {
            this.Id = product.Id.ToString();
            this.CreatedAt = product.CreatedDate.ToString();
            this.Name = product.Name;
            this.Description = product.Description;
            this.Price = product.Price.ToString();
            this.ReleaseDate = product.ReleaseDate;

            // Set category.
            if (product.Category != null)
                this.CategoryId = product.Category.Name;

            // Set features.
            if (product.Features != null && product.Features.Any())
                this.Features = product.Features.Select(f => new FeatureDTO(f)).ToList();

            // Set editors.
            if (product.Editors != null && product.Editors.Any())
                this.Editors = product.Editors.Select(pe => new ProductEditorDTO(pe)).ToList();
        }

        public Product ToEntity()
        {
            var product = new Product()
            {
                Id = this.Id != null ? new Guid(this.Id) : new Guid(),
                CreatedDate = this.CreatedAt != null ? Convert.ToDateTime(this.CreatedAt) : DateTime.Now,
                Name = this.Name,
                Description = this.Description,
                Price = Convert.ToDouble(this.Price),
                ReleaseDate = this.ReleaseDate
            };

            // Set product category.
            if (!string.IsNullOrEmpty(this.CategoryId))
                product.CategoryId = new Guid(this.CategoryId);

            // Set Features.
            if (this.Features != null && this.Features.Any())
                product.Features = this.Features.Select(f => f.ToEntity()).ToList();

            // Set Editors.
            if (this.Editors != null && this.Editors.Any())
            {
                // We setup the ProductId server-side to avoid the creation of editors associated to a different product.
                product.Editors = new List<ProductEditor>();
                foreach (var editor in this.Editors.Select(pe => pe.ToEntity()))
                {
                    editor.ProductId = product.Id;
                    product.Editors.Add(editor);
                }
            }

            return product;
        }
    }
}