using System;
using MediatR;

namespace ToDoList.API.CQRS.Handlers.Commands.AddNewToDo
{
    public class AddNewToDoCommand : IRequest<AddNewToDoCommandResponse>
    {
        public string TaskId { get; set; }
        
        public string Task { get; set; }

        public string Description { get; set; }
    }
}