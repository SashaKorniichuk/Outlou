using Domain.Shared;

namespace Domain.Errors;

public static class DomainErrors
{
    public static class News
    {
        public static readonly Func<Guid, Error> NotFound = id => new Error(
            "News.NotFound",
            $"The news with the identifier {id} was not found.");
    }

    public static class User
    {
        public static readonly Error InvalidCredentials = new(
            "User.InvalidCredentials",
            "The provided credentials are invalid");

        public static readonly Error EmailAlreadyInUse= new(
            "User.EmailAlreadyInUse",
            "The specified email is already in use");
    }

    public static class UserFeed
    {
        public static readonly Error FeedAlreadyExist = new(
            "UserFeed.FeedAlreadyExist",
            "The specified feed is already exist");
    }
}