using Microsoft.EntityFrameworkCore;
using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Errors;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Users.Read;

public sealed class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserResponseDto>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetUserQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result<UserResponseDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _applicationDbContext
            .Users
            .Where(u => u.Id == request.userId)
            .Select(u => new UserResponseDto(u.Id, u.Username, u.Email))
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            return Result.Failure<UserResponseDto>(DomainErrors.User.UserIdNotFound(request.userId));
        }

        return user;
    }
}
