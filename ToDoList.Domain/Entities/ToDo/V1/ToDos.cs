using System;
using ToDoList.Domain.Entities.Base;

namespace ToDoList.Domain.Entities.ToDo.V1
{
    public class ToDos : BaseEntity
    {
        public string TaskId { get; set; }
        
        public string Task { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; } 
        
        public DateTime DateCreated { get; set; } = DateTime.Now;

    }
}