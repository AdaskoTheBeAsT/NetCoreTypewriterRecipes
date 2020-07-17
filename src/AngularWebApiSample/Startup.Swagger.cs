using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

namespace AngularWebApiSample
{
    public partial class Startup
    {
        private const string SwaggerEndpoint = "v1/swagger.json";

        public void ConfigureServicesSwagger(IServiceCollection services)
        {
            // both services for swagger
            services.TryAddSingleton<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupCollectionProvider>();
            services.TryAddEnumerable(
                ServiceDescriptor.Transient<IApiDescriptionProvider, DefaultApiDescriptionProvider>());

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AngularWebApiSample API", Version = "v1" });

                // Set the comments path for the swagger json and ui.
                // based on http://michaco.net/blog/TipsForUsingSwaggerAndAutorestInAspNetCoreMvcServices
                var fileName =
                    GetType().GetTypeInfo().Module.Name
                    .Replace(".dll", ".xml", StringComparison.OrdinalIgnoreCase)
                    .Replace(".exe", ".xml", StringComparison.OrdinalIgnoreCase);
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, fileName));
            });
        }

        public void ConfigureSwagger(IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => c.SwaggerEndpoint(SwaggerEndpoint, "AngularWebApiSample API"));
        }
    }
}
