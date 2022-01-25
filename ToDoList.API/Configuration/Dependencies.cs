using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.API.Configuration
{
    public static class Dependencies
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.CommonServices(configuration);
        }

        public static void CommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            SetupMediatR(services, configuration);
        }

        private static void SetupMediatR(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}