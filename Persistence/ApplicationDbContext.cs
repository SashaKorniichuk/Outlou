using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;
public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<News> News { get; set; }

    public DbSet<RssFeed> RssFeeds { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserFeed> UserFeeds { get; set; }

    public DbSet<UserNews> UserNews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
}
