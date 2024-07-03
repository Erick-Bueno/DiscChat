using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ServerConfiguration : IEntityTypeConfiguration<ServerModel>
{
    public void Configure(EntityTypeBuilder<ServerModel> builder)
    {
        builder.HasKey(s => s.id);
        builder.HasIndex(u => u.externalId);
        builder.Property(u => u.externalId).HasMaxLength(200);
        builder.HasOne(s => s.user)
        .WithMany(u => u.servers)
        .HasForeignKey(s => s.idUser);
        
    }
}