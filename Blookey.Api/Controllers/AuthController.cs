using Blookey.Application.Features.Identity.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blookey.Api.Controllers;

public class AuthController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;   
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterCommand command)
    {
        var response = await _mediator.Send(command);

        return Ok(new { id = response });
    }

    //[Authorize]
    //[HttpPost("register/profile")]
    //public async Task<ActionResult> CompleteProfile([FromBody] CompleteProfileCommand command)
    //{
    //    // cadastro de CPF, data de nascimento, Endereço, Telefone, etc

    //    return Ok();
    //}
}
