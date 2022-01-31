using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using ToDoList.Shared.Services;

namespace TodoList.Infrastructure.Database.DataAccess
{
    public class CosmosDbService : CosmosDbFactory, ICosmosDbService
    {
        private Container Container { get; set; } = null;
        
        private  CosmosClient CosmosClient { get; set; }
        
        private string DatabaseName { get; set; }

        private int DefaultThroughput { get; set; } = 400;
        
        
        public CosmosDbService(IOptions<ApplicationSettings> applicationSettings)
        {
            var cosmosDbSettings = applicationSettings.Value.CosmosDbSettings;

            var account = cosmosDbSettings.Account;
            var key = cosmosDbSettings.Key;
            DatabaseName = cosmosDbSettings.DatabaseName;
            
            CosmosClient  = new CosmosClient(account, key, new CosmosClientOptions
            {
                SerializerOptions =  new CosmosSerializationOptions
                {
                    PropertyNamingPolicy =  CosmosPropertyNamingPolicy.CamelCase
                }
            });
        }

        private void InitContainer<T>()
        {
            var modelName = typeof(T).Name;
            Container = CosmosClient.GetContainer(DatabaseName, modelName);
        }
        
        //call the below function to create database automatically
        public override async Task<HttpStatusCode> CreateDatabase(string databaseName, CancellationToken cancellationToken = default)
        {
            try
            {
                var databaseCreateResponse =
                    await CosmosClient.CreateDatabaseIfNotExistsAsync(databaseName, DefaultThroughput, null,
                        cancellationToken);
                return databaseCreateResponse.StatusCode;
            }
            catch (CosmosException exception) when( exception.StatusCode != HttpStatusCode.Created)
            { 
                return exception.StatusCode;
            }
        }

        //call the below function to create container automatically
        public override async Task<HttpStatusCode> CreateContainer(string databaseName, string containerName, Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var database = CosmosClient.GetDatabase(databaseName);
                var containerCreateResponse = await database.CreateContainerIfNotExistsAsync(id.ToString(),
                    containerName, DefaultThroughput, null ,cancellationToken);
                return containerCreateResponse.StatusCode;
            }
            catch (CosmosException exception) when ( exception.StatusCode != HttpStatusCode.Created)
            {
                return exception.StatusCode;
            }
        }

        public override async Task<HttpStatusCode> IsItemExists<T>(string taskId, CancellationToken cancellationToken = default)
        {
            var modelName = typeof(T).Name;
            var modelSymbol = modelName[..1].ToLower();
            var query = $"select * from {modelName} {modelSymbol} where {modelSymbol}.taskId = \"{taskId}\"";
            var items = await GetItemsAsync<T>(query, cancellationToken);

            return !items.Any() ? HttpStatusCode.NotFound : HttpStatusCode.OK;
        }

        public override async Task<T> GetItemAsync<T>(string taskId, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Container == null) InitContainer<T>();
                var response = await Container.ReadItemAsync<T>(taskId.ToString(), new PartitionKey(taskId.ToString()),
                    null, cancellationToken);
                return response.Resource;
            }

            catch (CosmosException exception) when (exception.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
        }

        public override async Task<IEnumerable<T>> GetItemsAsync<T>(string queryString, CancellationToken cancellationToken = default)
        {
            try
            {
                if(Container == null) InitContainer<T>();
                var query = Container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
                var results = new List<T>();

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync(cancellationToken);
                    results.AddRange(response);
                }

                return results;

            }
            catch (CosmosException exception) when(exception.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
        }

        public override async Task<T> AddItemAsync<T>(string taskId, T item, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Container == null) InitContainer<T>();
                var response = await Container.CreateItemAsync(item, new PartitionKey(taskId.ToString()), null,
                    cancellationToken);
                return response.Resource;
            }

            catch (CosmosException exception) 
            {
                throw exception.GetBaseException();
            }
        }

        public override async Task<HttpStatusCode> UpdateItemAsync<T>(string taskId, T item, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Container == null) InitContainer<T>();
                var response = await Container.UpsertItemAsync(item, new PartitionKey(taskId.ToString()), null,
                    cancellationToken);
                return response.StatusCode;

            }
            catch (CosmosException exception) when (exception.StatusCode != HttpStatusCode.OK)
            {
                return exception.StatusCode;
            }
        }

        public override async Task<HttpStatusCode> DeleteItemAsync<T>(string taskId, T item, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Container == null) InitContainer<T>();
                var response = await Container.DeleteItemAsync<T>(taskId.ToString(), new PartitionKey(taskId.ToString()),
                    null, cancellationToken);
                return response.StatusCode;
            }
            catch (CosmosException exception) when (exception.StatusCode != HttpStatusCode.NoContent)
            {
                return exception.StatusCode;
            }
        }
    }
}