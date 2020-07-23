using System;

namespace Binit.Framework.Interfaces.DAL
{
    public interface IAuditable
    {
        string ToJson();
        Guid Id { get; set; }
    }
}