using Outlou.Application.Abstractions.Messaging;

namespace Outlou.Application.RssFeeds.Queries.GetAllActiveRssFeeds;

public sealed record GetAllActiveRssFeedsQuery(Guid UserId) : IQuery<List<ActiveRssFeedResponse>>;