using ProjectR.Domain.Shared;

namespace ProjectR.Domain.Errors;
public static class DomainErrors
{
    public static class User
    {
        public static Error UserIdNotFound(Guid id)
        {
            return new Error("User.UserIdNotFound", $"User with the Id: {id} was not found");
        }

        public static Error UserUsernameNotFound(string username)
        {
            return new Error("User.UsernameNotFound", $"User with the username: {username} was not found");
        }


    }

    public static class Epic
    {
        public static Error EpicIdNotFound(Guid id)
        {
            return new Error("Epic.EpicIdNotFound", $"Epic with the Id: {id} was not found");
        }
    }
}
