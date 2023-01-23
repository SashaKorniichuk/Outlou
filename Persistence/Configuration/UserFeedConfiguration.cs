using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

internal sealed class UserFeedConfiguration : IEntityTypeConfiguration<UserFeed>
{
    public void Configure(EntityTypeBuilder<UserFeed> builder)
    {
        builder.ToTable(nameof(UserFeed));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}

