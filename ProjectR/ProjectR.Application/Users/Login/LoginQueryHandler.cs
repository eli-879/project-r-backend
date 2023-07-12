using ProjectR.Application.Abstractions;
using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Errors;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Users.Login;

internal sealed class LoginQueryHandler : IQueryHandler<LoginQuery, JWTResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginQueryHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }
    public async Task<Result<JWTResponseDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        // get user
        var user = await _userRepository.GetUserByUsernameAsync(request.username);

        if (user is null)
        {
            return Result.Failure<JWTResponseDto>(DomainErrors.User.UserUsernameNotFound(request.username));
        }

        // generate JWT token

        var token = _jwtProvider.GenerateToken(user);

        // return JWT token

        return new JWTResponseDto(token);
    }
}
