using Domain.Entities;

namespace Domain.Repositories;

public interface IUserFeedsRepository
{
    Task<bool> IsAlreadyExist(Guid feedId,CancellationToken cancellationToken = default);

    Task<IEnumerable<RssFeed?>> GetAllActiveFeeds(Guid userId,CancellationToken cancellationToken = default);

    void Add(UserFeed userFeed);
}

