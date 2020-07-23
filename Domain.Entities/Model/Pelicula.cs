using Binit.Framework.AbstractEntities;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Model
{
    [Table("Pelicula")]
    public class Pelicula : AuditableEntity
    {
        #region Constructor

        public Pelicula()
        {

        }

        #endregion

        #region Properties

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("generoId")]
        public Genero genero { get; set; }

        public Guid generoId { get; set; }

        public List<PeliculaActor> Actores {get; set;}

        public List<Curiosidad> Curiosidades { get; set; }
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

        public override void CopyTo<Pelicula>(Pelicula target)
        {
            // Exclude category navigation property to make sure EF doesn't modify the category entity through Product. 
            EntityCopy<Pelicula>.Copy(this as Pelicula, target, new List<string> {
               "genero"
            });
        }
        #endregion
    }
}