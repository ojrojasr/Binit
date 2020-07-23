using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Seeds
{
    public abstract class SeedBase
    {
        public ModelBuilder modelBuilder { get; set; }
        public Dictionary<Type, IList<object>> AddedElements { get; set; }

        public void Process()
        {
            Execute();
        }

        protected abstract void Execute();
    }
}
