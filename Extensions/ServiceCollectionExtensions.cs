using Microsoft.Extensions.DependencyInjection;
using Tibaks_Backend.Services;

namespace Tibaks_Backend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Central place to register all application services.
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register your services here
            services.AddScoped<IChildService, ChildService>();

            return services;
        }
    }
}
