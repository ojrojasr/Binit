using Domain.Entities.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.DTOs.ProductDTOs.ProductEditorDTO;

namespace WebAPI.DTOs.ProductDTOs
{
    public class ProductEditorDTO
    {

        [Required(ErrorMessage = Lang.EditorIdRequired)]
        public string EditorId { get; set; }

        public ProductEditorDTO()
        {

        }

        public ProductEditorDTO(ProductEditor pe)
        {
            this.EditorId = pe.EditorId.ToString();
        }

        public ProductEditor ToEntity()
        {
            return new ProductEditor()
            {
                EditorId = new Guid(this.EditorId)
            };
        }
    }
}