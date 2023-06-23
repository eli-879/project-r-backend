using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectR.Application.Users.Create;
using ProjectR.Application.Users.Delete;
using ProjectR.Application.Users.Login;
using ProjectR.Application.Users.Read;
using ProjectR.Domain.Exceptions;

namespace ProjectR.Presentation.Controllers
{

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
        [HttpGet]
        public async Task<ActionResult> Get(Guid userId)
        {
            var user = await _sender.Send(new GetUserQuery(userId));

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Post(string username, string password, string email)
        {
            await _sender.Send(new CreateUserCommand(username, password, email));

            return StatusCode(201);
        }

        [HttpDelete("{userId:guid}")]
        public async Task<ActionResult> Delete(Guid userId)
        {
            try
            {
                await _sender.Send(new DeleteUserCommand(userId));

                return StatusCode(204);

            }
            catch (UserNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<JWTResponseDto>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _sender.Send(new LoginQuery(request.username, request.password));

                return StatusCode(200, token);


            }
            catch (UserNotFoundException e)
            {
                return StatusCode(401, e.Message);
            }
        }


    }



}
