using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AngularWebApiSample
{
    public partial class Startup
    {
        private const string NamespacePrefix = "AngularWebApiSample";

#pragma warning disable MA0016 // Prefer return collection abstraction instead of implementation
        public List<Assembly> GetAssemblies()
#pragma warning restore MA0016 // Prefer return collection abstraction instead of implementation
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var mainAssembly = typeof(Startup).Assembly;
            var assemblies = new List<Assembly> { mainAssembly };
            var refAssemblies = mainAssembly.GetReferencedAssemblies();
            foreach (var assemblyName in refAssemblies
                .Where(a => a.FullName.StartsWith(NamespacePrefix, StringComparison.OrdinalIgnoreCase)))
            {
                var assembly = loadedAssemblies.Find(l => l.FullName!.Equals(assemblyName.FullName, StringComparison.OrdinalIgnoreCase))
                               ?? AppDomain.CurrentDomain.Load(assemblyName);
                assemblies.Add(assembly);
            }

            return assemblies;
        }
    }
}
