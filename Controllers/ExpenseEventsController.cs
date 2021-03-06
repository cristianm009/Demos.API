﻿using Demos.API.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseEventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpenseEventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExpenseEventCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
