using Binit.Framework.AbstractEntities;
using Binit.Framework.Interfaces.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities.Model
{
    public class ApplicationUser : UserEntity
    {
        #region Properties

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [ForeignKey("PhotoId")]
        [JsonIgnore]
        public IgniteFile Photo { get; set; }
        public Guid? PhotoId { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }

        [Required]
        public Guid TenantId { get; set; }


        public string UserType { get; set; }


        #endregion

        #region Constructor

        public ApplicationUser()
        {

        }

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

        public string GetFullName() => $"{Name} {LastName}";

        #endregion
    }
}