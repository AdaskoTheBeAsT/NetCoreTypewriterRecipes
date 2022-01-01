using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace ReactWebApiSample;

public partial class Startup
{
    public void ConfigureServicesCompression(IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
            options.MimeTypes =
                ResponseCompressionDefaults.MimeTypes.Concat(
                    new []
                    {
                        "image/svg+xml",
                        "application/x-font-ttf",
                        "application/font-woff",
                        "application/font-woff2",
                        "application/vnd.ms-fontobject",
                        "application/x-icon",
                    });
        });
    }
}
