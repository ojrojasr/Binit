using Binit.Framework.AbstractEntities;
using Binit.Framework.Interfaces.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Model
{
    [Table("Answer")]
    public class Answer : AuditableEntity
    {
        #region Constructor

        public Answer()
        {

        }

        #endregion

        #region Properties

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public bool IsCorrect { get; set; }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        public Guid QuestionId { get; set; }

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
            return $"{this.Name}";
        }
        #endregion
    }
}