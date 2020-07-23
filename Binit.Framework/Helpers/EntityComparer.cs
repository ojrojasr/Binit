using Binit.Framework.Interfaces.DAL;
using System.Collections.Generic;

namespace Binit.Framework.Helpers
{
    public class EntityComparer<T> : IEqualityComparer<T> where T : IEntity
    {
        public bool Equals(T x, T y)
        {
            return x.Compare(y);
        }

        public int GetHashCode(T obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}