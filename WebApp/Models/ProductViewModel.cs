using Domain.Entities.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.ProductViewModel;

namespace WebApp.Models
{
    public class ProductViewModel : EntityViewModel
    {

        [Required(ErrorMessage = Lang.NameRequired)]
        [Display(Name = Lang.NameLabel)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.DescriptionRequired)]
        [StringLength(120, ErrorMessage = Lang.DescriptionStringLength, MinimumLength = 20)]
        [Display(Name = Lang.DescriptionLabel)]
        public string Description { get; set; }

        [Required(ErrorMessage = Lang.PriceRequired)]
        [Display(Name = Lang.PriceLabel)]
        public double Price { get; set; }

        [Display(Name = Lang.ReleaseDateLabel)]
        public string ReleaseDate { get; set; }

        [Display(Name = Lang.CategoryLabel)]
        public string CategoryId { get; set; }

        [Display(Name = Lang.CategoryLabel)]
        public string Category { get; set; }

        [Display(Name = Lang.FeaturesLabel)]
        public List<FeatureViewModel> Features { get; set; }

        [Display(Name = Lang.EditorsIdsLabel)]
        public List<string> EditorsIds { get; set; }

        [Display(Name = Lang.EditorsLabel)]
        public List<SelectListItem> Editors { get; set; }

        public ProductViewModel()
        {
            this.Id = new Guid().ToString();
            this.ReleaseDate = DateTime.Now.ToString("dd/MM/yyyy");
            this.Features = new List<FeatureViewModel>();
            this.EditorsIds = new List<string>();
            this.Editors = new List<SelectListItem>();
        }

        public ProductViewModel(Product product)
        {
            this.Id = product.Id.ToString();
            this.Name = product.Name;
            this.Description = product.Description;
            this.Price = product.Price;
            this.ReleaseDate = product.ReleaseDate.ToString("dd/MM/yyyy");

            if (product.CategoryId != null)
            {
                this.CategoryId = product.CategoryId.ToString();
                this.Category = product.Category.Name;
            }

            this.Features = new List<FeatureViewModel>();
            if (product.Features != null && product.Features.Count > 0)
            {
                foreach (var feature in product.Features)
                {
                    this.Features.Add(new FeatureViewModel(feature));
                }
            }

            this.Editors = new List<SelectListItem>();
            if (product.Editors != null && product.Editors.Count > 0)
            {
                foreach (var item in product.Editors)
                {
                    this.Editors.Add(new SelectListItem(item.Editor.Email, item.EditorId.ToString(), true));
                }
            }
        }

        public Product ToEntity()
        {
            var product = new Product()
            {
                Id = this.Id != null ? new Guid(this.Id) : Guid.Empty,
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                ReleaseDate = DateTime.ParseExact(this.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Features = new List<Feature>(),
                Editors = new List<ProductEditor>()
            };

            // Set product category
            if (!string.IsNullOrEmpty(this.CategoryId))
                product.CategoryId = new Guid(this.CategoryId);

            // Set product features
            if (this.Features != null && this.Features.Count > 0)
            {
                foreach (var feature in this.Features)
                {
                    product.Features.Add(new Feature()
                    {
                        Id = new Guid(feature.Id),
                        Name = feature.Name,
                        Description = feature.Description,
                        ProductId = product.Id
                    });
                }
            }

            // Set editors
            if (this.EditorsIds != null && this.EditorsIds.Count > 0)
            {
                foreach (var editorId in this.EditorsIds)
                {
                    product.Editors.Add(new ProductEditor()
                    {
                        ProductId = product.Id,
                        EditorId = new Guid(editorId)
                    }
                    );
                }
            }

            return product;
        }
    }
}