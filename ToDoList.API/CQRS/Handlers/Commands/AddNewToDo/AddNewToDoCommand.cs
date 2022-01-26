using System;
using MediatR;

namespace ToDoList.API.CQRS.Handlers.Commands.AddNewToDo
{
    public class AddNewToDoCommand : IRequest<Unit>
    {
        public string TaskId { get; set; }
        
        public string Task { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; } = false;
        
        public DateTime DateCreated { get; set; } = DateTime.Now;
        
    }
}