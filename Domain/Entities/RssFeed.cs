using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

public sealed class RssFeed:Entity
{
    private readonly List<News> _feedNews = new();
    private readonly List<User> _users = new();
    public RssFeed(
        Guid id,
        Uri uri
       ) :base(id)
    {
        Uri = uri;
    }

    public Uri Uri { get; private set; }

    public IReadOnlyCollection<News> FeedNews => _feedNews;
    public IReadOnlyCollection<User> Users => _users;
}

