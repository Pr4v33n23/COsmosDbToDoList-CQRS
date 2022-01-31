using System;

namespace ToDoList.API.CQRS.Handlers.Commands.AddNewToDoV2
{
    public class AddNewToDoV2CommandResponse
    {
        public string TaskId { get; set; }
        
        public string Task { get; set; }

        public string Description { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public bool IsImportant { get; set; }
        
        public DateTime DateCreated { get; set; }

    }
}