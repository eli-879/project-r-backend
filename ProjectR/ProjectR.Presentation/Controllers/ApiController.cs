using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectR.Application.Api.Subscribe;
using ProjectR.Domain.Shared;
using System.Security.Claims;

namespace ProjectR.Presentation.Controllers;

[Route("api")]
[ApiController]
public class ApiController : ControllerBase
{
    private readonly ILogger<ApiController> _logger;
    private readonly ISender _sender;

    public ApiController(ILogger<ApiController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [Authorize]
    [HttpPost("/subscribe")]
    public async Task<ActionResult> Subscribe([FromBody] SubscribeRequestDto subscribeRequestDto)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
        {
            return BadRequest();
        }

        Result epicUserResult = await _sender.Send(new SubscribeCommand(subscribeRequestDto, userId));

        return epicUserResult.IsSuccess ? StatusCode(201) : BadRequest(epicUserResult.Error);

    }
}
