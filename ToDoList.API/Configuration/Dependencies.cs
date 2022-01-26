using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.API.CQRS.Handlers.Commands;
using ToDoList.API.CQRS.Handlers.Commands.AddNewToDo;
using TodoList.Infrastructure.Database.DataAccess;
using ToDoList.Shared.Services;

namespace ToDoList.API.Configuration
{
    public static class Dependencies
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.CommonServices(configuration);
        }

        private static void CommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            SetupMediatR(services);
            SetupMapper(services);
            SetupApplicationSettings(services, configuration);
            SetupServices(services);

        }

        private static void SetupServices(IServiceCollection services)
        {
            services.AddScoped<ICosmosDbService, CosmosDbService>();
        }

        private static void SetupApplicationSettings(IServiceCollection services,  IConfiguration configuration)
        {
            services.AddOptions<ApplicationSettings>()
                .Bind(configuration.GetSection(nameof(ApplicationSettings)));
        }

        private static void SetupMediatR(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IRequestHandler<AddNewToDoCommand, Unit>, AddNewToDoCommandHandler>();
        }

        private static void SetupMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
        }
    }
}