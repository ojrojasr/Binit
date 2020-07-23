using System;

namespace Binit.Framework.Interfaces.DAL
{
    public interface IEntity
    {
        Guid Id { get; set; }
        bool Deleted { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime LastEditedDate { get; set; }
        Guid CreatorId { get; set; }
        Guid LastEditorId { get; set; }

        bool CanWrite(IOperationContext context);
        bool CanRead(IOperationContext context);
        bool Compare(IEntity entity);
        void CopyTo<TEntity>(TEntity target) where TEntity : class, IEntity;
    }
}