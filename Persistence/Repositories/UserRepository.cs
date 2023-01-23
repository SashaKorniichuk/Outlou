using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext=dbContext;
    }
    public void Add(User user)
    {
        _dbContext
            .Set<User>()
            .Add(user);
    }

    public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default)
    {
       return await _dbContext.Set<User>().ToListAsync(cancellationToken);
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken token=default)
    {
        return await _dbContext
            .Set<User>()
            .FirstOrDefaultAsync(x => x.Email==email,token);
    }

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
    {
        return !await _dbContext
            .Set<User>()
            .AnyAsync(member => member.Email == email, cancellationToken);
    }
}