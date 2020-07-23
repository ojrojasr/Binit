using Binit.Framework.AbstractEntities;
using Binit.Framework.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Model
{
    [Table("Question")]
    public class Question : AuditableEntity
    {
        #region Constructor

        public Question()
        {

        }

        #endregion

        #region Properties

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public List<Answer> Answers { get; set; }
        
        public Guid ThemeId { get; set; }

        [ForeignKey("ThemeId")]
        public Theme Theme { get; set; }
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