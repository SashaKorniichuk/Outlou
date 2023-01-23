using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken token=default);

    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);

    Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default);

    void Add(User user);
}