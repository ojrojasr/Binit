using Binit.Framework.AbstractEntities;
using Binit.Framework.Interfaces.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Model
{
    [Table("IgniteFile")]
    public class IgniteFile : AuditableEntity
    {
        #region Constructor

        public IgniteFile()
        {

        }

        #endregion

        #region Properties

        [Required]
        public string Filename { get; set; }

        [Required]
        public string Extension { get; set; }

        [Required]
        public string MimeType { get; set; }

        [Required]
        public long Size { get; set; }

        [Required]
        public string FileId { get; set; }

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

        public bool IsImage()
        {
            return this.MimeType.StartsWith("image");
        }

        #endregion
    }
}