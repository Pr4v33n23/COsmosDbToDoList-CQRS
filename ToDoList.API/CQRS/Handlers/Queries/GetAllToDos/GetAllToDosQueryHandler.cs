using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ToDoList.Domain.Entities.ToDo;
using ToDoList.Domain.Entities.ToDo.V1;
using TodoList.Infrastructure.Database.DataAccess;

namespace ToDoList.API.CQRS.Handlers.Queries.GetAllToDos
{
    public class GetAllToDosQueryHandler : IRequestHandler<GetAllToDosQuery, IEnumerable<GetAllToDosQueryResponse>>
    {
        private readonly ICosmosDbService _cosmosDbService;
        private IMapper _mapper;

        public GetAllToDosQueryHandler(ICosmosDbService cosmosDbService, IMapper mapper)
        {
            _cosmosDbService = cosmosDbService;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<GetAllToDosQueryResponse>> Handle(GetAllToDosQuery request, CancellationToken cancellationToken)
        {
            var toDos = await _cosmosDbService.GetItemsAsync<ToDos>($"select * from {nameof(ToDos)}", cancellationToken);
            return _mapper.Map<IEnumerable<GetAllToDosQueryResponse>>(toDos);
        }
        
    }
}