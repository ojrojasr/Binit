using Binit.Framework.AbstractEntities;
using Binit.Framework.Interfaces.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Model
{
    [Table("IgniteAddress")]
    public class IgniteAddress : AuditableEntity
    {
        #region Constructor

        public IgniteAddress()
        {

        }

        #endregion

        #region Properties

        public string Code { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public string Locality { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

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

        #endregion
    }
}