using Binit.Framework.AbstractEntities;
using Binit.Framework.Interfaces.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Model
{
    [Table("Category")]
    public class Category : AuditableEntity
    {
        #region Constructor

        public Category()
        {

        }

        #endregion

        #region Properties

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

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