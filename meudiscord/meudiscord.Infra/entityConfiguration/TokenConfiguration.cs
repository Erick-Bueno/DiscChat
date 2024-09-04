using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TokenConfiguration : IEntityTypeConfiguration<TokenEntity>
{
    public void Configure(EntityTypeBuilder<TokenEntity> builder)
    {
        builder.HasKey(t => t.id);
        builder.HasIndex(t => t.externalId);
        builder.HasIndex(t => t.email);
        builder.Property(t => t.externalId).HasMaxLength(200);
        builder.Property(t => t.email).IsRequired().HasMaxLength(100);
    }
}