using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SimpleInjector;

namespace ReactWebApiSample;

public sealed partial class Startup
    : IDisposable
{
    private readonly Container _container = new Container();

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        ConfigureServicesCompression(services);
        ConfigureServicesCors(services);

        services
            .AddControllers()
            .AddNewtonsoftJson(
                options =>
                {
                    // https://security-code-scan.github.io/#SCS0028
                    // implemented as white list
#pragma warning disable SCS0028
#pragma warning disable SEC0030 // Insecure Deserialization - Newtonsoft JSON
                    options.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
#pragma warning restore SEC0030 // Insecure Deserialization - Newtonsoft JSON
#pragma warning restore SCS0028
                    options.SerializerSettings.SerializationBinder = new LimitedBinder();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                });
        ConfigureServicesSwagger(services);
        services.AddLogging();

        ConfigureServicesIoC(services);
        var assemblies = GetAssemblies();
        ConfigureServicesMapping(assemblies);
        ConfigureServicesValidation(assemblies);
        ConfigureServicesMediatR(assemblies);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        ConfigureIoC(app, env);
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            ConfigureSwagger(app);
        }

        app.UseResponseCompression();
        app.UseDefaultFiles();
        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = ctx =>
            {
                var headers = ctx.Context.Response.GetTypedHeaders();
                if (ctx.File.Name.Equals("index.html", StringComparison.OrdinalIgnoreCase))
                {
                    headers.CacheControl = new CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.Zero,
                        NoCache = true,
                    };
                }
                else
                {
                    headers.CacheControl = new CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromDays(365),
                    };
                }
            },
        });

        app.UseHttpsRedirection();

        app.UseRouting();
        ConfigureCors(app);
        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
        _container.Verify();
    }

    public void Dispose()
    {
        _container.Dispose();
    }
}
