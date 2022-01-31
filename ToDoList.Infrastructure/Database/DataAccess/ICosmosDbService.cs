using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TodoList.Infrastructure.Database.DataAccess
{
    public interface ICosmosDbService
    { 
        Task<HttpStatusCode> CreateDatabase(string databaseName, CancellationToken cancellationToken = default);
        
        Task<HttpStatusCode> CreateContainer(string databaseName, string containerName, Guid id, CancellationToken cancellationToken = default);
        
        Task<HttpStatusCode> IsItemExists<T>(string taskId, CancellationToken cancellationToken = default) where T : class;

        Task<T> GetItemAsync<T>(string taskId, CancellationToken cancellationToken = default) where T : class;
        
        Task<IEnumerable<T>> GetItemsAsync<T>(string queryString, CancellationToken cancellationToken = default) where T : class;
        
        Task<T> AddItemAsync<T>(string taskId, T item, CancellationToken cancellationToken = default);
        
        Task<HttpStatusCode> UpdateItemAsync<T>(string taskId, T item, CancellationToken cancellationToken = default);
        
        Task<HttpStatusCode> DeleteItemAsync<T>(string taskId, T item, CancellationToken cancellationToken = default);
    }
}