using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using SimpleInjector;

namespace AngularWebApiSample;

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
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(
        IServiceCollection services)
    {
        ConfigureServicesCompression(services);
        ConfigureServicesCors(services);

        services
            .AddControllers()
            .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
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
    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env)
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
        app.UseRouting();
        ConfigureCors(app);
        app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        _container.Verify();
    }

    public void Dispose()
    {
        _container.Dispose();
    }
}
