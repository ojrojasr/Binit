using Binit.Framework.AbstractEntities;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Model
{
    [Table("Theme")]
    public class Theme : AuditableEntity
    {
        #region Constructor

        public Theme()
        {

        }

        #endregion

        #region Properties

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(7)]
        public string Color { get; set; }
        [Required]
        public int  QuestionQuantity {get; set;}
        public List<Question> Questions { get; set; }
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

        public override void CopyTo<Theme>(Theme target)
        {
            // Exclude question navigation property to make sure EF doesn't modify the question entity through Themes. 
            EntityCopy<Theme>.Copy(this as Theme, target, new List<string> {
               "Question"
            });
        }

       
        #endregion
    }
}