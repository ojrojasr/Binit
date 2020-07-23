using Binit.Framework.AbstractEntities;
using Binit.Framework.Interfaces.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Model
{
    [Table("Actor")]
    public class Actor : AuditableEntity
    {
        #region Constructor

        public Actor()
        {

        }

        #endregion

        #region Properties

        [Required]
        public string Name { get; set; }


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
            return $"{this.Name}:";
        }
        #endregion
    }
}