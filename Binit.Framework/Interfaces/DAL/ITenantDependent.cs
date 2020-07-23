using System;

namespace Binit.Framework.Interfaces.DAL
{
    public interface ITenantDependent
    {
        ITenant Tenant { get; set; }
        Guid TenantId { get; set; }
    }
}