using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Configuration;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Password).IsRequired();

        builder.Property(x => x.Email).IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasMany(x => x.Feeds)
            .WithMany(x => x.Users)
            .UsingEntity<UserFeed>(
                x => x.HasOne(y => y.Feed)
                    .WithMany()
                    .HasForeignKey(prop => prop.FeedId)
                    .OnDelete(DeleteBehavior.Cascade),
                x => x.HasOne(y => y.User)
                    .WithMany()
                    .HasForeignKey(prop => prop.UserId)
                    .OnDelete(DeleteBehavior.Cascade),
                x =>
                {
                    x.HasKey(prop => new { prop.FeedId, prop.UserId });
                }
            );

        builder.HasMany<News>(x => x.News)
            .WithMany(x => x.Users)
            .UsingEntity<UserNews>(
                x => x.HasOne(y => y.News)
                    .WithMany()
                    .HasForeignKey(prop => prop.NewsId)
                    .OnDelete(DeleteBehavior.Cascade),
                x => x.HasOne(y => y.User)
                    .WithMany()
                    .HasForeignKey(prop => prop.UserId)
                    .OnDelete(DeleteBehavior.Cascade),
                x =>
                {
                    x.HasKey(prop => new { prop.NewsId, prop.UserId });
                }
            );
    }
}