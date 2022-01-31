using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TodoList.Infrastructure.Database
{
    public abstract class CosmosDbFactory
    {
        public abstract Task<HttpStatusCode> CreateDatabase(string databaseName,
            CancellationToken cancellationToken = default);

        public abstract Task<HttpStatusCode> CreateContainer(string databaseName, string containerName, Guid id,
            CancellationToken cancellationToken = default);

        public abstract Task<HttpStatusCode> IsItemExists<T>(string taskId, CancellationToken cancellationToken = default)
            where T : class;

        public abstract Task<T> GetItemAsync<T>(string taskId, CancellationToken cancellationToken = default) where T : class;

        public abstract Task<IEnumerable<T>> GetItemsAsync<T>(string queryString,
            CancellationToken cancellationToken = default) where T : class;

        public abstract Task<T> AddItemAsync<T>(string taskId, T item, CancellationToken cancellationToken = default);
        
        public abstract Task<HttpStatusCode> UpdateItemAsync<T>(string taskId, T item, CancellationToken cancellationToken = default);
        
        public abstract Task<HttpStatusCode> DeleteItemAsync<T>(string taskId, T item, CancellationToken cancellationToken = default);
        
    }
}