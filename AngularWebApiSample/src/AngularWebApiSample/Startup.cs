using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace AngularWebApiSample
{
    public partial class Startup
    {
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
            services
                .AddControllers()
                .AddNewtonsoftJson(
                    options =>
                    {
                        // https://security-code-scan.github.io/#SCS0028
                        // implemented as white list
#pragma warning disable SCS0028
                        options.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
#pragma warning restore SCS0028
                        options.SerializerSettings.SerializationBinder = new LimitedBinder();
                        options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    });
            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
