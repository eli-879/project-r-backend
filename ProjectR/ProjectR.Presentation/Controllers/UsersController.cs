using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectR.Application.Users.Create;
using ProjectR.Application.Users.Delete;
using ProjectR.Application.Users.Login;
using ProjectR.Application.Users.Read;
using ProjectR.Domain.Shared;

namespace ProjectR.Presentation.Controllers;

[Route("users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly ISender _sender;
    public UsersController(ILogger<UsersController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserResponseDto>> Get(Guid userId)
    {
        Result<UserResponseDto> userResult = await _sender.Send(new GetUserQuery(userId));

        return userResult.IsSuccess ? Ok(userResult.Value) : NotFound(userResult.Error);
    }

    [HttpPost]
    public async Task<ActionResult<CreateUserResponseDto>> Post([FromBody] CreateUserRequestDto request)
    {
        Result<CreateUserResponseDto> userResult = await _sender.Send(new CreateUserCommand(request.username, request.password, request.email));

        return userResult.IsSuccess ? StatusCode(201, userResult.Value) : BadRequest(userResult.Error);
    }

    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult> Delete(Guid userId)
    {
        /*try
        {
            await _sender.Send(new DeleteUserCommand(userId));

            return StatusCode(204);

        }
        catch (UserNotFoundException e)
        {
            return StatusCode(404, e.Message);
        */

        var result = await _sender.Send(new DeleteUserCommand(userId));

        return result.IsSuccess ? StatusCode(204) : NotFound(result.Error);
    }

    [HttpPost("login")]
    public async Task<ActionResult<JWTResponseDto>> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
    {
        /* try
         {
             var token = await _sender.Send(new LoginQuery(request.username, request.password));

             return StatusCode(200, token);


         }
         catch (UserNotFoundException e)
         {
             return StatusCode(401, e.Message);
         }*/

        var tokenResult = await _sender.Send(new LoginQuery(request.username, request.password));

        return tokenResult.IsSuccess ? StatusCode(200, tokenResult.Value) : StatusCode(401, tokenResult.Error);
    }


}
