using Domain.Entities;

namespace Domain.Repositories;

public interface INewsRepository
{
    Task<News?> GetNewsByIdAsync(Guid id, CancellationToken token);

    Task<bool> IsAlreadyExist(string title, CancellationToken cancellationToken = default);
    void Update(News news);
    void Add(News news);
}