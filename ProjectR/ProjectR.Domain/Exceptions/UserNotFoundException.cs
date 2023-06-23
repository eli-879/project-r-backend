namespace ProjectR.Domain.Exceptions
{
    public sealed class UserNotFoundException : Exception
    {
        public UserNotFoundException(Guid userId)
            : base($"The user with id of {userId} was not found.")
        { }

        public UserNotFoundException(string username)
           : base($"The user with username of {username} was not found.")
        { }
    }
}
