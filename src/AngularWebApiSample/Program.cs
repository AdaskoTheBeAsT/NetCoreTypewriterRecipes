using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AngularWebApiSample
{
    public static class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json",
                optional: true)
            .AddEnvironmentVariables()
            .Build();

#pragma warning disable CA1031 // Do not catch general exception types
        public static int Main(string[] args)
        {
            var host = default(IWebHost);
            try
            {
                host = CreateWebHostBuilder(args).Build();
                host.Run();
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
            finally
            {
                host?.Dispose();
            }
        }
#pragma warning restore CA1031 // Do not catch general exception types

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => logging.ClearProviders())
                .UseStartup<Startup>()
                .UseSetting("detailedErrors", "true")
                .CaptureStartupErrors(true)
                .UseConfiguration(Configuration);
    }
}
