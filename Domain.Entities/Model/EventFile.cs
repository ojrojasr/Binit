using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Model
{
    /// <summary>
    /// Join entity to represent many-to-many relationship between Event and IgniteFile
    /// Currently EF Core doesn't support other means to represent many-to-many relationships
    /// More info: https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.0/features#property-bag-entities
    /// Tracking code: preview5-issue2
    /// </summary>
    [Table("EventFile")]
    public class EventFile
    {
        #region Constructor

        public EventFile()
        {

        }

        #endregion

        #region Properties

        [Required]
        public Guid EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }
        [Required]
        public Guid FileId { get; set; }
        [ForeignKey("FileId")]
        public IgniteFile File { get; set; }

        #endregion
    }
}