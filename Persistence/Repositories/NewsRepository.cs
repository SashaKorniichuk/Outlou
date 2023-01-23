using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class NewsRepository : INewsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public NewsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(News news)
    {
        _dbContext.Set<News>().Add(news);
    }

    public async Task<News?> GetNewsByIdAsync(Guid id, CancellationToken token)
    {
        return await _dbContext.Set<News>().FirstOrDefaultAsync(x => x.Id == id,token);
    }

    public async Task<bool> IsAlreadyExist(string title, CancellationToken cancellationToken = default)
    {
        return !await _dbContext
        .Set<News>()
            .AnyAsync(news => news.Name == title, cancellationToken);
    }

    public void Update(News news)
    {
        _dbContext.Set<News>().Update(news);
    }
}
