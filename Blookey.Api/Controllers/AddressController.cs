using Blookey.Application.Features.Address.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blookey.Api.Controllers;

[ApiController]
public class AddressController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public AddressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost("address")]
    public async Task<ActionResult> CreateAddress([FromBody] CreateAddressCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}
