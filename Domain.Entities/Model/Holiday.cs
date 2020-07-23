using Binit.Framework.AbstractEntities;
using Binit.Framework.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities.Model
{
    [Table("Holiday")]
    public class Holiday : AuditableEntity
    {
        #region Constructor

        public Holiday()
        {

        }

        #endregion

        #region Properties

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("ReasonId")]
        [JsonIgnore]
        public HolidayType Reason { get; set; }
        public Guid? ReasonId { get; set; }
        public List<HolidayUser> Users { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }

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