using System.Linq;

namespace Binit.Framework.Extensions
{
    public static class Reflection
    {
        public static bool ClassImplementsInterface<TClass, TInterface>()
        {
            return typeof(TClass).GetInterfaces().Contains(typeof(TInterface));
        }
    }
}