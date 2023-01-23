using Domain.Entities;

namespace Domain.Repositories;

public interface IUserNewsRepository
{
    Task<IEnumerable<UserNews?>> GetAllUnreadUserNewsFromSomeDate(Guid userId,DateTime date, CancellationToken cancellationToken = default);

    Task<UserNews?> GetNewsByIdAsync(Guid id, CancellationToken token);
    void Update(UserNews userNews);

    void Add(UserNews userNews);
}

