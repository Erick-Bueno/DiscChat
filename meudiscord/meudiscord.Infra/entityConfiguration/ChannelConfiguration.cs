using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ChannelConfiguration : IEntityTypeConfiguration<ChannelModel>
{
    public void Configure(EntityTypeBuilder<ChannelModel> builder)
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