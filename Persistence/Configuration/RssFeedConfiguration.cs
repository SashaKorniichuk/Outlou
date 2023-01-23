using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

internal sealed class RssFeedConfiguration : IEntityTypeConfiguration<RssFeed>
{
    public void Configure(EntityTypeBuilder<RssFeed> builder)
    {
       builder.ToTable(nameof(RssFeed));

       builder.HasKey(x => x.Id);

       builder.Property(x => x.Id).ValueGeneratedOnAdd();

       builder.Property(x => x.Uri).IsRequired();

       builder.HasMany(x => x.FeedNews)
           .WithOne(c=>c.RssFeed)
           .HasForeignKey(x => x.RssFeedId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}