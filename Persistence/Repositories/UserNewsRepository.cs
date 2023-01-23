using Domain.Entities;
using Domain.Repositories;
using System;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class UserNewsRepository:IUserNewsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserNewsRepository(ApplicationDbContext dbContext)
    {
        _dbContext= dbContext;
    }

    public void Add(UserNews userNews)
    {
        _dbContext.Set<UserNews>().Add(userNews);
    }

    public async Task<IEnumerable<UserNews?>> GetAllUnreadUserNewsFromSomeDate(Guid userId, DateTime date, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<UserNews>().Where(x => x.PublishedDateTime > date).Include(x=>x.News).ToListAsync(cancellationToken);
    }

    public async Task<UserNews?> GetNewsByIdAsync(Guid id, CancellationToken token)
    {
        return await _dbContext.Set<UserNews>().FirstOrDefaultAsync(x => x.NewsId == id, token);
    }

    public void Update(UserNews userNews)
    {
        _dbContext.Set<UserNews>().Update(userNews);
    }
}