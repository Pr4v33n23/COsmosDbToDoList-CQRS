using System.Collections.Generic;
using MediatR;

namespace ToDoList.API.CQRS.Handlers.Queries.GetAllToDosV2
{
    public class GetAllToDosV2Query : IRequest<IEnumerable<GetAllToDosV2QueryResponse>>
    {
        
    }
}