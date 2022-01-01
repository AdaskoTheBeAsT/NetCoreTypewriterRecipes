using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AngularWebApiSample;

public static class Program
{
#pragma warning disable CA1031 // Do not catch general exception types
    public static int Main(string[] args)
    {
        var host = default(IHost);
        try
        {
            host = CreateHostBuilder(args).Build();
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

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging => logging.ClearProviders())
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
}
