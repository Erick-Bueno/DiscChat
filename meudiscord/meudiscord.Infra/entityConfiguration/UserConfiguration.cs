using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.id);
        builder.HasIndex(u => u.externalId);
        builder.Property(u => u.externalId).HasMaxLength(200);
        builder.Property(u => u.name).IsRequired().HasMaxLength(200);
        builder.Property(u => u.email).IsRequired().HasMaxLength(200);
        builder.Property(u => u.password).IsRequired().HasMaxLength(500);
    }
}