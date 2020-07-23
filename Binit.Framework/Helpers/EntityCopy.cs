using Binit.Framework.Interfaces.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Binit.Framework.Helpers
{
    public static class EntityCopy<TEntity> where TEntity : class, IEntity
    {
        private static readonly List<PropertyInfo> sourceProperties = new List<PropertyInfo>();
        private static readonly List<PropertyInfo> targetProperties = new List<PropertyInfo>();

        public static void Copy(TEntity source, TEntity target, List<string> excludeFields)
        {
            Initialize(excludeFields);

            for (int i = 0; i < sourceProperties.Count; i++)
            {
                targetProperties[i].SetValue(target, sourceProperties[i].GetValue(source, null), null);
            }
        }

        public static void Copy(TEntity source, TEntity target)
        {
            Initialize(new List<string>());

            for (int i = 0; i < sourceProperties.Count; i++)
            {
                targetProperties[i].SetValue(target, sourceProperties[i].GetValue(source, null), null);
            }
        }

        private static void Initialize(List<string> excludeFields)
        {
            var excludeEntityProperties = typeof(IEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name);

            excludeFields.AddRange(excludeEntityProperties);

            var properties = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo sourceProperty in properties)
            {
                // If source property cannot be read or must be excluded, skip the current iteration.
                if (!sourceProperty.CanRead || excludeFields.Contains(sourceProperty.Name))
                {
                    continue;
                }

                PropertyInfo targetProperty = typeof(TEntity).GetProperty(sourceProperty.Name);

                // If target property cannot be setted because is readonly, skip the current iteration. 
                if (!targetProperty.CanWrite)
                {
                    continue;
                }

                // If target property cannot be setted because it doesn't have a set method, skip the current iteration. 
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                {
                    continue;
                }

                //bindings.Add(Expression.Bind(targetProperty, Expression.Property(sourceParameter, sourceProperty)));
                sourceProperties.Add(sourceProperty);
                targetProperties.Add(targetProperty);
            }
        }
    }
}