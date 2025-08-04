using FinancialApi.Application.Commands.Auth;
using FinancialApi.Application.Commands.People;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApi.API.Controllers.Auth;

[ApiController]
[Route("")]

[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region POST

    [HttpPost("people")]
    public async Task<IActionResult> Create([FromBody] CreatePersonCommand command)
    {
        var result = await _mediator.Send(command);
        return Created("", result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginPersonCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    #endregion
}