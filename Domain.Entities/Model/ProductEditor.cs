using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Model
{
    /// <summary>
    /// Join entity to represent many-to-many relationship between Product and ApplicationUser
    /// Currently EF Core doesn't support other means to represent many-to-many relationships
    /// More info: https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.0/features#property-bag-entities
    /// Tracking code: preview5-issue2
    /// </summary>
    [Table("ProductEditor")]
    public class ProductEditor
    {
        #region Constructor

        public ProductEditor()
        {

        }

        #endregion

        #region Properties

        [Required]
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [Required]
        public Guid EditorId { get; set; }
        [ForeignKey("EditorId")]
        public ApplicationUser Editor { get; set; }

        #endregion
    }
}