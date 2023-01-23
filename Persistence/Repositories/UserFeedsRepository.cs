using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class UserFeedsRepository : IUserFeedsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserFeedsRepository(ApplicationDbContext dbContext)
    {
        _dbContext=dbContext;
    }
    public void Add(UserFeed userFeed)
    {
        _dbContext.Set<UserFeed>().Add(userFeed);
    }

    public async Task<IEnumerable<RssFeed?>> GetAllActiveFeeds(Guid userId, CancellationToken cancellationToken=default)
    {
        return await _dbContext.Set<UserFeed>().Where(x => x.UserId==userId && x.Status==RssFeedStatus.Active).Select(x => x.Feed).ToListAsync(cancellationToken);
    }

    public async Task<bool> IsAlreadyExist(Guid feedId, CancellationToken cancellationToken = default)
    {
        return !await _dbContext
            .Set<UserFeed>()
            .AnyAsync(userFeed => userFeed.FeedId == feedId, cancellationToken);
    }
}