using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TodoList.Domain.Entities.ToDo.V2;
using TodoList.Infrastructure.Database.DataAccess;

namespace ToDoList.API.CQRS.Handlers.Queries.GetAllToDosV2
{
    public class GetAllToDosV2QueryHandler : IRequestHandler<GetAllToDosV2Query, IEnumerable<GetAllToDosV2QueryResponse>>
    {
        private ICosmosDbService _cosmosDbService;
        private IMapper _mapper;

        public GetAllToDosV2QueryHandler(ICosmosDbService cosmosDbService, IMapper mapper)
        {
            _cosmosDbService = cosmosDbService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetAllToDosV2QueryResponse>> Handle(GetAllToDosV2Query request, CancellationToken cancellationToken)
        {
            var toDosV2 =
                await _cosmosDbService.GetItemsAsync<ToDos>($"select * from {nameof(ToDos)}", cancellationToken);
            return _mapper.Map<IEnumerable<GetAllToDosV2QueryResponse>>(toDosV2);
        }
    }
}