using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectR.Application.Epics.Create;
using ProjectR.Application.Epics.Read;
using ProjectR.Application.Threads.Create;
using ProjectR.Domain.Shared;
using System.Security.Claims;

namespace ProjectR.Presentation.Controllers;

[Route("epics")]
[ApiController]
public class EpicsController : ControllerBase
{
    private readonly ILogger<EpicsController> _logger;
    private readonly ISender _sender;

    public EpicsController(ILogger<EpicsController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet("{epicId}")]
    public async Task<ActionResult<EpicResponseDto>> Get(Guid epicId)
    {
        Result<EpicResponseDto> epicResult = await _sender.Send(new GetEpicQuery(epicId));

        return epicResult.IsSuccess ? Ok(epicResult.Value) : NotFound(epicResult.Error);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EpicResponseDto>>> Get()
    {
        Result<IEnumerable<EpicResponseDto>> epicsResult = await _sender.Send(new GetAllEpicsQuery());

        return epicsResult.IsSuccess ? Ok(epicsResult.Value) : NotFound(epicsResult.Error);
    }

    [HttpPost]
    public async Task<ActionResult<CreateEpicResponseDto>> Post([FromBody] CreateEpicRequestDto request)
    {
        Result<CreateEpicResponseDto> epicResult = await _sender.Send(new CreateEpicCommand(request.name));

        return epicResult.IsSuccess ? StatusCode(201, epicResult.Value) : BadRequest(epicResult.Error);
    }


}
