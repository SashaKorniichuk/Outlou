using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Configuration;
internal sealed class UserNewsConfiguration : IEntityTypeConfiguration<UserNews>
{
    public void Configure(EntityTypeBuilder<UserNews> builder)
    {
        builder.ToTable(nameof(UserNews));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}