using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoApi.API.DTOs;
using TodoApi.Application.Todos.Commands.CreateTodo;
using TodoApi.Application.Todos.Queries.GetTodos;


namespace TodoApi.API.Controllers;


[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class TodosController : ControllerBase
{
    private readonly ISender _mediator;
    public TodosController(ISender mediator) => _mediator = mediator;


    private Guid CurrentUserId =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);


    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _mediator.Send(new GetTodosQuery(CurrentUserId, page, pageSize));
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoRequest req)
    {
        // API DTO → Command (Application never sees CreateTodoRequest)
        var cmd = new CreateTodoCommand(req.Title, req.Description, req.DueDate, CurrentUserId);
        var result = await _mediator.Send(cmd);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }
}
