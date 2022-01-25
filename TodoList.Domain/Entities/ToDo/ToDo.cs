using System;
using TodoList.Domain.Entities.Base;

namespace TodoList.Domain.Entities.ToDo
{
    public class ToDo : BaseEntity
    {
        public string TaskId { get; set; }
        
        public string Task { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; } = false;
        
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}