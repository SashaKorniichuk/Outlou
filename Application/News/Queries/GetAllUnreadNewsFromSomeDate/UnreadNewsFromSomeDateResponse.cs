namespace Outlou.Application.News.Queries.GetAllUnreadNewsFromSomeDate;

public sealed record UnreadNewsFromSomeDateResponse(Guid Id,NewsResponse News,
    DateTime PublishedDateTime);