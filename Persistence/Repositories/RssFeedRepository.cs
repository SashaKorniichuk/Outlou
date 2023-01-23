using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Persistence.Repositories;

internal sealed class RssFeedRepository : IRssFeedRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RssFeedRepository(ApplicationDbContext dbContext)
    {
        _dbContext=dbContext;
    }

    public void Add(RssFeed rssFeed)
    {
        _dbContext.Set<RssFeed>().Add(rssFeed);
    }

    public async Task<IEnumerable<RssFeed>> GetAllRssFeedsAsync(CancellationToken token)
    {
        return await _dbContext.Set<RssFeed>().ToListAsync(token);
    }

    public async Task<RssFeed?> GetFeedByUrl(Uri uri, CancellationToken token)
    {
        return await _dbContext.Set<RssFeed>().FirstOrDefaultAsync(x => x.Uri==uri,token);
    }
}