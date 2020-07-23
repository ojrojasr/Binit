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
    [Table("Event")]
    public class Event : TenantDependentAuditableEntity
    {
        #region Constructor

        public Event()
        {

        }

        #endregion

        #region Properties

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public double Duration { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public List<EventFile> Files { get; set; }

        [ForeignKey("LocationId")]
        [JsonIgnore]
        public IgniteAddress Location { get; set; }
        public Guid? LocationId { get; set; }

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

        public override void CopyTo<Event>(Event target)
        {
            // Exclude location navigation property to make sure EF doesn't modify the location entity through Event. 
            EntityCopy<Event>.Copy(this as Event, target, new List<string> {
               "Location"
            });
        }

        #endregion
    }
}
