using System;

namespace ToDoList.API.CQRS.Handlers.Queries.GetAllToDos
{
    public class GetAllToDosQueryResponse
    {
        public string TaskId { get; set; }
        
        public string Task { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; } 
        
        public DateTime DateCreated { get; set; } 
    }
}