using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TodoList.Domain.Entities.ToDo;
using TodoList.Infrastructure.Database.DataAccess;

namespace ToDoList.API.CQRS.Handlers.Commands.AddNewToDo
{
    public class AddNewToDoCommandHandler : IRequestHandler<AddNewToDoCommand, Unit>
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly IMapper _mapper;

        public AddNewToDoCommandHandler(ICosmosDbService cosmosDbService, IMapper mapper )
        {
            _cosmosDbService = cosmosDbService;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(AddNewToDoCommand request, CancellationToken cancellationToken)
        {
            var toDo = _mapper.Map<ToDo>(request);
            await _cosmosDbService.AddItem(request.TaskId, toDo, cancellationToken);
            return await Task.FromResult(Unit.Value);
        }
        
    }
}