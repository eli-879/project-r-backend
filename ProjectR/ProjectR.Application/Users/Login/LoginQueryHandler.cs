using MediatR;
using ProjectR.Application.Abstractions;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Exceptions;

namespace ProjectR.Application.Users.Login
{
    public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, JWTResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public LoginQueryHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }
        public async Task<JWTResponseDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            // get user
            var user = await _userRepository.GetUserByUsernameAsync(request.username);

            if (user == null)
            {
                throw new UserNotFoundException(request.username);
            }

            // generate JWT token

            var token = _jwtProvider.GenerateToken(user);

            // return JWT token

            return new JWTResponseDto(token);


        }
    }
}
