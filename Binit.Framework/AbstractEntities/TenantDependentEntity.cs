using Binit.Framework.Interfaces.DAL;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Binit.Framework.AbstractEntities
{
    public abstract class TenantDependentEntity : Entity, ITenantDependent
    {
        [ForeignKey("TenantId")]
        public ITenant Tenant { get; set; }
        public Guid TenantId { get; set; }
    }
}