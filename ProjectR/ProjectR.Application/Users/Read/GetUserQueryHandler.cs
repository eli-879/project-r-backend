using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Exceptions;

namespace ProjectR.Application.Users.Read
{
    public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResponseDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetUserQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<UserResponseDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _applicationDbContext
                .Users
                .Where(u => u.Id == request.userId)
                .Select(u => new UserResponseDto(u.Id, u.Username, u.Email))
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new UserNotFoundException(request.userId);
            }

            return user;
        }
    }
}
