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

        public abstract Task<T> GetItem<T>(string taskId, CancellationToken cancellationToken = default) where T : class;

        public abstract Task<IEnumerable<T>> GetItems<T>(string queryString,
            CancellationToken cancellationToken = default) where T : class;

        public abstract Task<HttpStatusCode> AddItem<T>(string taskId, T item, CancellationToken cancellationToken = default);
        
        public abstract Task<HttpStatusCode> UpdateItem<T>(string taskId, T item, CancellationToken cancellationToken = default);
        
        public abstract Task<HttpStatusCode> DeleteItem<T>(string taskId, T item, CancellationToken cancellationToken = default);
        
    }
}