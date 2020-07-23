using Binit.Framework.AbstractEntities;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Model
{
    [Table("Game")]
    public class Game : AuditableEntity
    {
        #region Constructor

        public Game()
        {
           Answers = new List<GameAnswer>();
        }

        #endregion

        #region Properties
        public Guid UserId { get; set; }
        [ForeignKey("ThemeId")]
        public Theme Theme { get; set; }
        public Guid ThemeId { get; set; }
        public List<GameAnswer> Answers {get; set;}
        public bool Ended { get; set; }
        public int QuestionQuantity { get; set; }
        public int CorrectQuantity { get; set; }
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

        public override void CopyTo<Game>(Game target)
        {
            // Exclude category navigation property to make sure EF doesn't modify the category entity through Product. 
            EntityCopy<Game>.Copy(this as Game, target, new List<string> {
               "Theme"
            });
        }
        #endregion
    }
}