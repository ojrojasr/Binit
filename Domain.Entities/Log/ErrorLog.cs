using Binit.Framework.Interfaces.DAL;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Log
{
    [Table("Errors")]
    public class ErrorLog : Binit.Framework.AbstractEntities.LogEntity
    {
        public string ErrorXml { get; set; }
        public string ApplicationName { get; set; }
        public string HostName { get; set; }
        public string Source { get; set; }
        public int StatusCode { get; set; }
        public string Type { get; set; }
        public string User { get; set; }
        public string WebHostHtmlMessage { get; set; }

        [NotMapped]
        public string Exception { get; set; }

        public override bool CanRead(IOperationContext context)
        {
            // TODO
            return true;
        }

        public override bool CanWrite(IOperationContext context)
        {
            // TODO
            return true;
        }
    }
}