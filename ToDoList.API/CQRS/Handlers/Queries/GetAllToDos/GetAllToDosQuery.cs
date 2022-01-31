using System;
using System.Collections.Generic;
using MediatR;

namespace ToDoList.API.CQRS.Handlers.Queries.GetAllToDos
{
    public class GetAllToDosQuery : IRequest<IEnumerable<GetAllToDosQueryResponse>>
    {
    }
}