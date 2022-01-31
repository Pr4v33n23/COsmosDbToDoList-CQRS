using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TodoList.Domain.Entities.ToDo.V2;
using TodoList.Infrastructure.Database.DataAccess;

namespace ToDoList.API.CQRS.Handlers.Commands.AddNewToDoV2
{
    public class AddNewToDoV2CommandHandler : IRequestHandler<AddNewToDoV2Command, AddNewToDoV2CommandResponse>
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly IMapper _mapper;

        public AddNewToDoV2CommandHandler(ICosmosDbService cosmosDbService, IMapper mapper)
        {
            _cosmosDbService = cosmosDbService;
            _mapper = mapper;
        }
        public async Task<AddNewToDoV2CommandResponse> Handle(AddNewToDoV2Command request, CancellationToken cancellationToken)
        {
            var toDoV2 = _mapper.Map<ToDos>(request);
            var response = await _cosmosDbService.AddItemAsync(toDoV2.TaskId, toDoV2, cancellationToken);
            return _mapper.Map<AddNewToDoV2CommandResponse>(response);
        }
    }
}