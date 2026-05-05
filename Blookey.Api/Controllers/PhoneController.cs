using Blookey.Api.Extensions;
using Blookey.Application.Features.Phone.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blookey.Api.Controllers;

public class PhoneController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public PhoneController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost("phone")]
    public async Task<IResult> CreatePhone([FromBody] CreatePhoneCommand command)
    {
        var result = await _mediator.Send(command);

        return result.IsSuccess
            ? Results.Ok(result)
            : result.ToProblemDetails();
    }
}