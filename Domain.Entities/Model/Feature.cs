using Binit.Framework.AbstractEntities;
using Binit.Framework.Interfaces.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Model
{
    [Table("Feature")]
    public class Feature : AuditableEntity
    {
        #region Constructor

        public Feature()
        {

        }

        #endregion

        #region Properties

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public Guid ProductId { get; set; }

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
        #endregion
    }
}