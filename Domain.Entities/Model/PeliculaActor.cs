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
    [Table("PeliculaActor")]
    public class PeliculaActor
    {
        #region Constructor

        public PeliculaActor()
        {

        }

        #endregion

        #region Properties

        [Required]
        public Guid PeliculaId { get; set; }
        [ForeignKey("PeliculaId")]
        public Pelicula Pelicula { get; set; }
        [Required]
        public Guid ActorId { get; set; }
        [ForeignKey("ActorId")]
        public Actor Actor { get; set; }

        #endregion
    }
}