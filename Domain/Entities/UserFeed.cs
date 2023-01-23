using Domain.Enums;

namespace Domain.Entities;

public sealed class UserFeed
{
    public UserFeed(Guid id,Guid userId,Guid feedId,RssFeedStatus status)
    {
        Id=id;
        UserId=userId;
        FeedId=feedId;
        Status = status;
    }

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public User? User { get; private set; }
    public Guid FeedId { get; private set; }
    public RssFeed? Feed { get; private set; }
    public RssFeedStatus Status { get; private set; }
}