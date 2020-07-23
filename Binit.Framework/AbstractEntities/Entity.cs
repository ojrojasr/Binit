using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Binit.Framework.AbstractEntities
{
    public abstract class Entity : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id
        {
            get
            {
                if (this.id == default(Guid) || this.id == null || this.id == Guid.Empty)
                    this.id = SequentialGuidGenerator.NewSequentialGuid();
                return id;
            }

            set { this.id = value; }
        }
        private Guid id { get; set; }

        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastEditedDate { get; set; }
        public Guid CreatorId { get; set; }
        public Guid LastEditorId { get; set; }

        public abstract bool CanRead(IOperationContext context);
        public abstract bool CanWrite(IOperationContext context);

        public virtual bool Compare(IEntity entity)
        {
            return this.Id == entity.Id;
        }

        public virtual void CopyTo<TEntity>(TEntity target) where TEntity : class, IEntity
        {
            EntityCopy<TEntity>.Copy(this as TEntity, target);
        }
    }
}