using Domain.Primitives;

namespace Domain.Entities;

public sealed class User : Entity
{
    private readonly List<RssFeed> _feeds = new();
    private readonly List<News> _news = new();
    public User(Guid id, string email,string password):base(id)
    {
        Email = email;
        Password = password;
    }
    public string Email { get; private set; }

    public string Password { get; private set; }

    public ICollection<RssFeed> Feeds => _feeds;
    public ICollection<News> News => _news;
}