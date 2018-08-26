using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class WebApiServiceCollectionExtensions
    {
#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
          /// <summary>
          /// Adds MVC services to the specified <see cref="IServiceCollection" /> for Web API.
          /// This is a slimmed down version of <see cref="MvcServiceCollectionExtensions.AddMvc"/>.
          /// </summary>
          /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
          /// <returns>An <see cref="IMvcBuilder"/> that can be used to further configure the MVC services.</returns>
        public static IMvcBuilder AddWebApi(this IServiceCollection services)
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var builder = services.AddMvcCore();

            builder.AddAuthorization();
            builder.AddFormatterMappings();
            builder.AddJsonFormatters();
            builder.AddCors();
            return new MvcBuilder(builder.Services, builder.PartManager);
        }

#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
          /// <summary>
          /// Adds MVC services to the specified <see cref="IServiceCollection" /> for Web API.
          /// This is a slimmed down version of <see cref="MvcServiceCollectionExtensions.AddMvc"/>.
          /// </summary>
          /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
          /// <param name="setupAction">An <see cref="Action{MvcOptions}"/> to configure the provided <see cref="MvcOptions"/>.</param>
          /// <returns>An <see cref="IMvcBuilder"/> that can be used to further configure the MVC services.</returns>
        public static IMvcBuilder AddWebApi(this IServiceCollection services, Action<MvcOptions> setupAction)
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            var builder = services.AddWebApi();
            builder.Services.Configure(setupAction);

            return builder;
        }
    }
}
