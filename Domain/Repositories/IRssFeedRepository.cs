using Domain.Entities;

namespace Domain.Repositories;

public interface IRssFeedRepository
{
    Task<IEnumerable<RssFeed>> GetAllRssFeedsAsync(CancellationToken token);
    Task<RssFeed?> GetFeedByUrl(Uri uri,CancellationToken token);
    void Add(RssFeed rssFeed);
}