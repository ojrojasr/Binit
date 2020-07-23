using Binit.Framework.Constants.DAL;
using Binit.Framework.Interfaces.DAL;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Log
{
    [Table("Audit")]
    public class AuditLog : Binit.Framework.AbstractEntities.LogEntity
    {
        #region Properties

        public string User { get; set; }
        public string Entity { get; set; }
        public Guid EntityId { get; set; }
        public DALOperations Operation { get; set; }

        #endregion

        #region Constructor

        public AuditLog()
        {

        }

        public AuditLog(IAuditable auditEntity, IOperationContext operationContext, DALOperations operation)
        {
            this.Detail = auditEntity.ToJson();
            this.Entity = auditEntity.GetType().Name;
            this.EntityId = auditEntity.Id;
            this.Message = operation.ToString();
            this.Operation = operation;
            this.User = operationContext.GetUsername();
        }

        #endregion

        #region Public Methods

        public override bool CanRead(IOperationContext context)
        {
            return true;
        }

        public override bool CanWrite(IOperationContext context)
        {
            return true;
        }

        #endregion
    }
}