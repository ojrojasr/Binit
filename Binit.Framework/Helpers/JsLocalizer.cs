using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Binit.Framework.Helpers
{
    public static class JsLocalizer
    {
        public static Dictionary<string, string> GetLocalizedResources(List<string> keys, IStringLocalizer<SharedResources> localizer)
        {
            var dictionary = new Dictionary<string, string>();

            foreach (string key in keys)
            {
                dictionary.Add(key, localizer[key]);
            }

            return dictionary;
        }
    }
}