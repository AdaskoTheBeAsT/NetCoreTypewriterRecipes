using System.Collections.Generic;
using System.Reflection;
using AdaskoTheBeAsT.AutoMapper.SimpleInjector;

namespace AngularWebApiSample;

public partial class Startup
{
    public void ConfigureServicesMapping(IEnumerable<Assembly> assemblies)
    {
        _container.AddAutoMapper(assemblies);
    }
}
