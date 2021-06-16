using CQRSMediatRDemo.Commands;
using CQRSMediatRDemo.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSMediatRDemo.Controllers
{
    public class TodoController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<TodoController> logger;

        public TodoController(IMediator mediator, ILogger<TodoController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            var response = await mediator.Send(new GetTodoByID.Query(id));
            return response == null ? NotFound() : Ok(response);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddTodo(AddTodo.Command command) => Ok(await mediator.Send(command));
        
    }
}
