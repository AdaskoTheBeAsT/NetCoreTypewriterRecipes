using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AngularWebApiSample
{
    public partial class Startup
    {
        private const string NamespacePrefix = "AngularWebApiSample";

        public List<Assembly> GetAssemblies()
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var mainAssembly = typeof(Startup).Assembly;
            var assemblies = new List<Assembly> { mainAssembly };
            var refAssemblies = mainAssembly.GetReferencedAssemblies();
            foreach (var assemblyName in refAssemblies
                .Where(a => a.FullName.StartsWith(NamespacePrefix, StringComparison.OrdinalIgnoreCase)))
            {
                var assembly = loadedAssemblies.Find(l => l.FullName == assemblyName.FullName)
                               ?? AppDomain.CurrentDomain.Load(assemblyName);
                assemblies.Add(assembly);
            }

            return assemblies;
        }
    }
}
