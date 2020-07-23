using System;

namespace Binit.Framework.Helpers
{
    public static class UriHelper
    {
        public static string Combine(string baseUri, string endpoint)
        {
            var endpointUri = new Uri(baseUri);
            endpointUri = new Uri(endpointUri, endpoint);

            return endpointUri.ToString();
        }
    }
}