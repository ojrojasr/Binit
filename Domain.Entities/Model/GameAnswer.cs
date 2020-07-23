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
    [Table("GameAnswer")]
    public class GameAnswer
    {
        #region Constructor

        public GameAnswer()
        {

        }

        #endregion

        #region Properties

        public Guid QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        public Guid AnswerId { get; set; }
        [ForeignKey("AnswerId")]
        public Answer AnswerSelected { get; set; }
        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; }



        #endregion
    }
}