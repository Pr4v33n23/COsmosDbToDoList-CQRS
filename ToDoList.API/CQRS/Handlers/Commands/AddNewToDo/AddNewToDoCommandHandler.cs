using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ToDoList.Domain.Entities.ToDo.V1;
using TodoList.Infrastructure.Database.DataAccess;

namespace ToDoList.API.CQRS.Handlers.Commands.AddNewToDo
{
    public class AddNewToDoCommandHandler : IRequestHandler<AddNewToDoCommand, AddNewToDoCommandResponse>
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly IMapper _mapper;

        public AddNewToDoCommandHandler(ICosmosDbService cosmosDbService, IMapper mapper )
        {
            _cosmosDbService = cosmosDbService;
            _mapper = mapper;
        }
        public async Task<AddNewToDoCommandResponse> Handle(AddNewToDoCommand request, CancellationToken cancellationToken)
        {
            var toDos = _mapper.Map<ToDos>(request);
            var response = await _cosmosDbService.AddItemAsync(toDos.TaskId,toDos, cancellationToken);

            return _mapper.Map<AddNewToDoCommandResponse>(response);
        }
        
    }
}