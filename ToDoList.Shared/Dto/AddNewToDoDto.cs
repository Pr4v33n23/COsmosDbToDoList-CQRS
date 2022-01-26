using System;

namespace ToDoList.Shared.Dto
{
    public class AddNewToDoDto
    {
        
        public string TaskId { get; set; }
        
        public string Task { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; } = false;
        
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}