using Binit.Framework.AbstractEntities;
using Binit.Framework.Constants;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.Interfaces.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Model
{
    [Table("Tenant")]
    public class Tenant : Entity, ITenant
    {
        #region Constructor

        public Tenant()
        {

        }

        #endregion

        #region Properties

        [Required]
        [RegularExpression(RegularExpressions.NoSpecialCharacters)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        #endregion

        #region Public Methods

        public override bool CanRead(IOperationContext context)
        {
            return true;
        }

        public override bool CanWrite(IOperationContext context)
        {
            return context.UserIsInRole(Roles.BackofficeSuperAdministrator);
        }
        #endregion
    }
}