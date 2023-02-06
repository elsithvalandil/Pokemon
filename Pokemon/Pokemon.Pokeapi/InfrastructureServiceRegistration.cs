using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokemon.Application.Contracts.Infraestructure;
using Pokemon.Pokeapi.Models;
using Pokemon.Pokeapi.Services;

namespace Pokemon.Pokeapi
{
    /// <summary>
    /// Container class for dependency injection.
    /// </summary>
    public static class InfrastructureServiceRegistration
    {
        /// <summary>
        /// Method to add dependency injection of service interface and read configuration from PokeapiSettings section in appsettings.json.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetSection("PokeapiSettings").Exists())
            {
                services.Configure<PokeapiSettings>(c => configuration.GetSection("PokeapiSettings").Bind(c));
            }
            services.AddTransient<IPokeapiService, PokeapiService>();

            return services;
        }
    }
}
