using System.Collections.Generic;
using System.Reflection;
using AdaskoTheBeAsT.FluentValidation.MediatR;
using AdaskoTheBeAsT.MediatR.SimpleInjector.AspNetCore;

namespace AngularWebApiSample
{
    public partial class Startup
    {
        public void ConfigureServicesMediatR(IEnumerable<Assembly> assemblies)
        {
            _container.AddMediatRAspNetCore(
                options =>
                {
                    options.AsScoped();
                    options.WithAssembliesToScan(assemblies);
                    options.UsingBuiltinPipelineProcessorBehaviors(true);
                    options.UsingPipelineProcessorBehaviors(typeof(FluentValidationPipelineBehavior<,>));
                });
        }
    }
}
