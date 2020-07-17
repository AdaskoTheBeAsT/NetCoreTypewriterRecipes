using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AngularWebApiSample
{
    public partial class Startup
    {
        public static readonly string AllowDev = "AllowDev";

        public void ConfigureServicesCors(IServiceCollection services)
        {
            services.AddCors(
                options =>
                    options.AddPolicy(
                        AllowDev,
                        p =>
                            p.AllowAnyMethod()
                                .AllowAnyHeader()
                                .SetIsOriginAllowed(_ => true)
                                .AllowCredentials()));
        }

        public void ConfigureCors(IApplicationBuilder app)
        {
            app.UseCors(AllowDev);
        }
    }
}
