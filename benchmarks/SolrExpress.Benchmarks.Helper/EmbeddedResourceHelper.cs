using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace SolrExpress.Benchmarks.Helper
{
    /// <summary>
    /// Helper class to manipule embedded resources
    /// </summary>
    public static class EmbeddedResourceHelper
    {
        public static string GetByName(Assembly assembly, string resourceName)
        {
            var resourceStream = assembly.GetManifestResourceStream(resourceName);

            if (resourceStream == null)
            {
                throw new Exception($"Resource {resourceName} not found");
            }

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
