namespace Outlou.Application.News.Queries.GetAllUnreadNewsFromSomeDate;

public sealed record NewsResponse(Guid RssFeedId, string Name, string Description);