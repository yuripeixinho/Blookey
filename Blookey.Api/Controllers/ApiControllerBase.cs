using Blookey.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace Blookey.Api.Controllers;

[Route("api/")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            var c when c.EndsWith("NotFound") => NotFound(result.Error),
            var c when c.EndsWith("Forbidden") => Forbid(),
            _ => BadRequest(result.Error)
        };
    }
}

