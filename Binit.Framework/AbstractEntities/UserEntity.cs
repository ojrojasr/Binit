using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Binit.Framework.AbstractEntities
{
    public abstract class UserEntity : IdentityUser<Guid>, IEntity, IAuditable
    {
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastEditedDate { get; set; }
        public Guid CreatorId { get; set; }
        public Guid LastEditorId { get; set; }

        public abstract bool CanRead(IOperationContext context);
        public abstract bool CanWrite(IOperationContext context);

        public string ToJson()
        {
            // We'll temporarily use Newtonsoft for json serialization until mechanism to handle circular references is fixed for System.Text.Json.Serialization
            // Tracking code: 3.0.0-issue1
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                MaxDepth = 1,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public virtual bool Compare(IEntity entity)
        {
            return this.Id == entity.Id;
        }

        public virtual void CopyTo<TEntity>(TEntity target) where TEntity : class, IEntity
        {
            // Exclude properties that are not allowed to be modified by the user.
            var excludeProperties = new List<string>
            {
                "NormalizedUserName",
                "NormalizedEmail",
                "PasswordHash",
                "SecurityStamp",
                "LockoutEnd",
                "EmailConfirmed",
                "ConcurrencyStamp"
            };
            EntityCopy<TEntity>.Copy(this as TEntity, target, excludeProperties);
        }
    }
}