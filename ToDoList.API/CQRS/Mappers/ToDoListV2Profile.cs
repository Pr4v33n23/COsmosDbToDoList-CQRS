using AutoMapper;
using ToDoList.API.CQRS.Handlers.Commands.AddNewToDoV2;
using ToDoList.API.CQRS.Handlers.Queries.GetAllToDosV2;
using TodoList.Domain.Entities.ToDo.V2;

namespace ToDoList.API.CQRS.Mappers
{
    public class ToDoListV2Profile : Profile
    {
        public ToDoListV2Profile()
        {
            //Command
            CreateMap<AddNewToDoV2Command, ToDos>();
            CreateMap<ToDos, AddNewToDoV2CommandResponse>();
            
            //Query
            CreateMap<ToDos, GetAllToDosV2QueryResponse>();

        }
    }
}