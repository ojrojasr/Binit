using Binit.Framework.Interfaces.DAL;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Model.SPs
{
    public class SPCreateCategory : ISPCommand
    {
        public string Name { get => "SP_CreateCategories"; }
        public Dictionary<string, object> Params { get; set; }
    }

    public class SPReturnCategory : IDbView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
