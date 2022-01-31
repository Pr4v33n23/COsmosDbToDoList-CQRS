using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ToDoList.API.CQRS.Handlers.Commands.AddNewToDo;
using ToDoList.API.CQRS.Handlers.Commands.AddNewToDoV2;
using ToDoList.API.CQRS.Handlers.Queries.GetAllToDos;
using ToDoList.API.CQRS.Handlers.Queries.GetAllToDosV2;
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
            SetupApiVersioning(services);
            SetupSwagger(services);

        }

        private static void SetupSwagger(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                });
            });

        }

        private static void SetupApiVersioning(IServiceCollection services)
        {
            services.AddApiVersioning(x =>  
            {  
                x.DefaultApiVersion = new ApiVersion(1, 0);  
                x.AssumeDefaultVersionWhenUnspecified = true;  
                x.ReportApiVersions = true;  
            });  
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
            services.AddTransient<IRequestHandler<AddNewToDoCommand, AddNewToDoCommandResponse>, AddNewToDoCommandHandler>();
            services.AddTransient<IRequestHandler<GetAllToDosQuery, IEnumerable<GetAllToDosQueryResponse>>, GetAllToDosQueryHandler>();
            services.AddTransient<IRequestHandler<AddNewToDoV2Command, AddNewToDoV2CommandResponse>, AddNewToDoV2CommandHandler>();
            services.AddTransient<IRequestHandler<GetAllToDosV2Query, IEnumerable<GetAllToDosV2QueryResponse>>, GetAllToDosV2QueryHandler>();
            
        }

        private static void SetupMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
        }
    }
}