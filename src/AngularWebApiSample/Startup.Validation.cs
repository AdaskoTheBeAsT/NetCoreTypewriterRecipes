using System.Collections.Generic;
using System.Reflection;
using AdaskoTheBeAsT.FluentValidation.SimpleInjector;

namespace AngularWebApiSample;

public partial class Startup
{
    public void ConfigureServicesValidation(IEnumerable<Assembly> assemblies)
    {
        _container.AddFluentValidation(assemblies);
    }
}
