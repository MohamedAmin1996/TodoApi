using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApi.API.DTOs;
using TodoApi.Application.Auth.Commands.Login;
using TodoApi.Application.Auth.Commands.Register;

namespace TodoApi.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;
    public AuthController(ISender mediator) => _mediator = mediator;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req)
    {
        var cmd = new RegisterCommand(req.Username, req.Email, req.Password);
        var token = await _mediator.Send(cmd);
        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var cmd = new LoginCommand(req.Email, req.Password);
        var token = await _mediator.Send(cmd);
        return Ok(new { token });
    }
}