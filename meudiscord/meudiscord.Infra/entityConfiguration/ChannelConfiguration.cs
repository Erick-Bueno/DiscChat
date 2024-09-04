using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ChannelConfiguration : IEntityTypeConfiguration<ChannelEntity>
{
    public void Configure(EntityTypeBuilder<ChannelEntity> builder)
    {
        builder.HasKey(c => c.id);
        builder.HasIndex(c => c.externalId);
        builder.Property(c => c.externalId).HasMaxLength(200);
        builder.HasOne(c => c.server)
        .WithMany(s => s.channels)
        .HasForeignKey(c => c.serverId)
        .OnDelete(DeleteBehavior.Cascade);

    }
}