﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectR.Application.Comments.Create;
using ProjectR.Application.Comments.Read;
using ProjectR.Domain.Shared;
using System.Security.Claims;

namespace ProjectR.Presentation.Controllers;

[Route("comments")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ILogger<ApiController> _logger;
    private readonly ISender _sender;

    public CommentController(ILogger<ApiController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateCommentRequestDto request)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
        {
            return BadRequest();
        }
        Result<CreateCommentResponseDto> result = await _sender.Send(new CreateCommentCommand(request, userId));

        return result.IsSuccess ? StatusCode(201, result.Value) : BadRequest(result.Error);
    }

    [HttpGet("/thread")]
    public async Task<ActionResult<IEnumerable<GetCommentsForThreadResponseDto>>> Get(Guid threadId)
    {
        Result<IEnumerable<GetCommentsForThreadResponseDto>> result = await _sender.Send(new GetCommentsForThreadQuery(threadId));
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

}
