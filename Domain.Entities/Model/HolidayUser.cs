using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Model
{
    /// <summary>
    /// Join entity to represent many-to-many relationship between Holiday and ApplicationUser
    /// Currently EF Core doesn't support other means to represent many-to-many relationships
    /// More info: https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.0/features#property-bag-entities
    /// Tracking code: preview5-issue2
    /// </summary>
    [Table("HolidayUser")]
    public class HolidayUser
    {
        #region Constructor

        public HolidayUser()
        {

        }

        #endregion

        #region Properties

        [Required]
        public Guid HolidayId { get; set; }
        [ForeignKey("HolidayId")]
        public Holiday Holiday { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        #endregion
    }
}