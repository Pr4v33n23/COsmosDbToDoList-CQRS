using System;

namespace ToDoList.API.CQRS.Handlers.Queries.GetAllToDosV2
{
    public class GetAllToDosV2QueryResponse
    {
        public string TaskId { get; set; }
        
        public string Task { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; } 
        
        public DateTime DateCreated { get; set; } 
        
        public bool IsImportant { get; set; }
        
    }
}