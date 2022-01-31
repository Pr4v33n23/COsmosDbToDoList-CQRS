using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.CQRS.Handlers.Commands.AddNewToDo;
using ToDoList.API.CQRS.Handlers.Commands.AddNewToDoV2;
using ToDoList.API.CQRS.Handlers.Queries.GetAllToDos;
using ToDoList.API.CQRS.Handlers.Queries.GetAllToDosV2;

namespace ToDoList.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ToDoListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToDoListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "taskId": 1,
        ///        "task": "Item1",
        ///        "description": "to complete Item1"
        ///     }
        ///
        /// </remarks>
        [HttpPost("addnewtodo")]
        public async Task<IActionResult> AddNewToDoListV1(AddNewToDoCommand newToDoCommand)
        {
            var response  =  await _mediator.Send(newToDoCommand);
            return CreatedAtAction("GetToDo", new {taskId = response.TaskId}, response);
        }

        [HttpGet("getalltodos")]
        public async Task<IActionResult> GetAllToDosV1()
        {
            var response = await _mediator.Send(new GetAllToDosQuery());
            return Ok(response);
        }
        
        [HttpGet("getalltodos")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetAllToDosV2()
        {
            var response = await _mediator.Send(new GetAllToDosV2Query());
            return Ok(response);
        }
        
        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "taskId": 1,
        ///        "task": "Item1",
        ///        "description": "to complete Item1",
        ///        "isImportant": true
        ///     }
        ///
        /// </remarks>
        
        [HttpPost("addnewtodo")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> AddNewToDoListV2(AddNewToDoV2Command newToDoCommand)
        {
            var response  =  await _mediator.Send(newToDoCommand);
            return CreatedAtAction("GetToDo", new {taskId = response.TaskId}, response);
        }

        [HttpGet]

        public async Task<IActionResult> GetToDo()
        {
            return Ok();
        }

    }
}