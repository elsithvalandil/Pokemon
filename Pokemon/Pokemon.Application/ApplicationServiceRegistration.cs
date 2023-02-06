using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Pokemon.Application
{
    /// <summary>
    /// Container class for dependency injection.
    /// </summary>
    public static class ApplicationServiceRegistration
    {
        /// <summary>
        /// Method to register MediatR assemblie for dependency injection.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
