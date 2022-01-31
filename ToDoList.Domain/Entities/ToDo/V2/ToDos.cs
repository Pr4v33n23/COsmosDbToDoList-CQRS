using System;
using ToDoList.Domain.Entities.Base;

namespace TodoList.Domain.Entities.ToDo.V2
{
    public class ToDos : BaseEntity
    {
        public string TaskId { get; set; }
        
        public string Task { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; } 
        
        public bool IsImportant { get; set; }
        
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}