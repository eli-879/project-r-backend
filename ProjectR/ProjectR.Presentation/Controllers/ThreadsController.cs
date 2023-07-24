using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectR.Application.Threads.Create;
using ProjectR.Application.Threads.Read.GetAllThreads;
using ProjectR.Application.Threads.Read.GetAllThreadsPreview;
using ProjectR.Application.Threads.Read.GetCommentsForThread;
using ProjectR.Domain.Shared;
using System.Security.Claims;

namespace ProjectR.Presentation.Controllers;

[Route("threads")]
[ApiController]
public class ThreadsController : ControllerBase
{
    private readonly ILogger<EpicsController> _logger;
    private readonly ISender _sender;

    public ThreadsController(ILogger<EpicsController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet("/thread/{threadId}")]
    public async Task<ActionResult<IEnumerable<GetCommentsForThreadResponseDto>>> Get(Guid threadId)
    {
        Result<IEnumerable<GetCommentsForThreadResponseDto>> result = await _sender.Send(new GetCommentsForThreadQuery(threadId));
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("/thread")]
    public async Task<ActionResult<CreateThreadResponseDto>> AddThread([FromBody] CreateThreadRequestDto request)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
        {
            return BadRequest();
        }
        Result<CreateThreadResponseDto> addThreadResult = await _sender.Send(new CreateThreadCommand(request, userId));

        return addThreadResult.IsSuccess ? StatusCode(201, addThreadResult.Value) : BadRequest(addThreadResult.Error);
    }

    [HttpGet("/threads")]
    public async Task<ActionResult<ICollection<ThreadPreviewResponseDto>>> GetThreads()
    {
        Result<ICollection<ThreadPreviewResponseDto>> getThreadsResult = await _sender.Send(new GetAllThreadsPreviewQuery());

        return getThreadsResult.IsSuccess ? Ok(getThreadsResult.Value) : NotFound(getThreadsResult.Error);
    }
}
