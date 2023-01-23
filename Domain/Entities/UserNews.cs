using Domain.Enums;

namespace Domain.Entities;

public sealed class UserNews
{
    public UserNews(Guid id, Guid userId, Guid newsId,DateTime publishedDateTime,NewsStatus newsStatus)
    {
        Id=id;
        UserId=userId;
        NewsStatus=newsStatus;
        NewsId=newsId;
        PublishedDateTime=publishedDateTime;
    }
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public Guid NewsId { get; private set; }
    public News News { get; private set; }

    public DateTime PublishedDateTime { get; private set; }
    public NewsStatus NewsStatus { get; private set; }

    public void MarkAsRead()
    {
        NewsStatus = NewsStatus.Read;
    }
}

