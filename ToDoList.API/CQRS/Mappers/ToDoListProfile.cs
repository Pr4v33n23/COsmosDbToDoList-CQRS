using AutoMapper;
using ToDoList.API.CQRS.Handlers.Commands;
using ToDoList.API.CQRS.Handlers.Commands.AddNewToDo;
using TodoList.Domain.Entities.ToDo;
using ToDoList.Shared.Dto;

namespace ToDoList.API.CQRS.Mappers
{
    public class ToDoListProfile : Profile
    {
        public ToDoListProfile()
        {
            //DTO to Command
            CreateMap<AddNewToDoDto, AddNewToDoCommand>();
            //Command to Entity
            CreateMap<AddNewToDoCommand, ToDo>();
            
        }
    }
}