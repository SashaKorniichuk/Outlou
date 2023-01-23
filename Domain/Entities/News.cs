using Domain.Enums;
using Domain.Primitives;
using static Domain.Errors.DomainErrors;

namespace Domain.Entities;

public sealed class News : Entity
{
    private readonly List<User> _users = new();
    public News(
        Guid id,
        Guid rssFeedId,
        string name,
        string description) : base(id)
    {
        RssFeedId = rssFeedId;
        Name = name;
        Description = description;
    }

    public Guid RssFeedId { get; private set; }
    public RssFeed RssFeed { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public IReadOnlyCollection<User> Users => _users;
}