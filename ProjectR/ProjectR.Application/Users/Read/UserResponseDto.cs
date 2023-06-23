namespace ProjectR.Application.Users.Read
{
    public sealed record UserResponseDto(
        Guid id,
        string username,
        string email
        );
}
