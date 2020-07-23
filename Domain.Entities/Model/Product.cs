using Binit.Framework.AbstractEntities;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities.Model
{
    [Table("Product")]
    public class Product : AuditableEntity
    {
        #region Constructor

        public Product()
        {

        }

        #endregion

        #region Properties

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        [ForeignKey("CategoryId")]
        [JsonIgnore]
        public Category Category { get; set; }

        public Guid? CategoryId { get; set; }

        public List<Feature> Features { get; set; }

        public List<ProductEditor> Editors { get; set; }
        #endregion

        #region Public Methods
        public override bool CanRead(IOperationContext context)
        {
            // TODO.
            return true;
        }

        public override bool CanWrite(IOperationContext context)
        {
            // TODO.
            return true;
        }

        public override string ToString()
        {
            return $"{this.Name}: {this.Description}";
        }

        public override void CopyTo<Product>(Product target)
        {
            // Exclude category navigation property to make sure EF doesn't modify the category entity through Product. 
            EntityCopy<Product>.Copy(this as Product, target, new List<string> {
               "Category"
            });
        }
        #endregion
    }
}