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

        public static Error UserIdIsNotValid(string id)
        {
            return new Error("User.UseIdNotValid", $"UserId: {id} was not a valid Id");
        }


    }

    public static class Epic
    {
        public static Error EpicIdNotFound(Guid id)
        {
            return new Error("Epic.EpicIdNotFound", $"Epic with the Id: {id} was not found");
        }

        public static Error EpicNameNotFound(string epicName)
        {
            return new Error("Epic.EpicNameNotFound", $"Epic with the name: {epicName} was not found");
        }
    }

    public static class Thread
    {
        public static Error ThreadIdNotFound(Guid id)
        {
            return new Error("Thread.ThreadIdNotFound", $"Thread with the Id: {id} was not found");
        }
    }

    public static class Comment
    {
        public static Error CommentIdNotFound(Guid id)
        {
            return new Error("Comment.CommentIdNotFound", $"Comment with the Id: {id} was not found");
        }

        public static Error CommentNotInSameThread()
        {
            return new Error("Comment.CommentNotInSameThread", $"Comment parent not in same thread as current thread");
        }
    }
}
