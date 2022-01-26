using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.CQRS.Handlers.Commands.AddNewToDo;
using ToDoList.Shared.Dto;

namespace ToDoList.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class ToDoListController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ToDoListController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<Unit> AddNewToDoList(AddNewToDoDto payLoad) =>
            await _mediator.Send(_mapper.Map<AddNewToDoCommand>(payLoad));
        
    }
}