using AutoMapper;
using ToDoList.API.CQRS.Handlers.Commands.AddNewToDo;
using ToDoList.API.CQRS.Handlers.Queries.GetAllToDos;
using ToDoList.Domain.Entities.ToDo.V1;

namespace ToDoList.API.CQRS.Mappers
{
    public class ToDoListProfile : Profile
    {
        public ToDoListProfile()
        {
            //CommandRequest to Entity
            CreateMap<AddNewToDoCommand, ToDos>();
            //Entity to CommandResponse
            CreateMap<ToDos, AddNewToDoCommandResponse>();
            
            //Entity to QueryResponse
            CreateMap<ToDos, GetAllToDosQueryResponse>();

        }
    }
}