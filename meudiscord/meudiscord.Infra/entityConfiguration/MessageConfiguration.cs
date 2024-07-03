using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MessageConfiguration : IEntityTypeConfiguration<MessageModel>
{
    public void Configure(EntityTypeBuilder<MessageModel> builder)
    {
        builder.HasKey(m => m.id);
        builder.HasIndex(m => m.externalId);
        builder.Property(m => m.externalId).HasMaxLength(200);
        builder.Property(m => m.message).HasColumnType("TEXT");
        builder.HasOne(m => m.user)
        .WithMany(u => u.messages)
        .HasForeignKey(m => m.idUser);
        builder.HasOne(m => m.channel)
        .WithMany(c => c.messages)
        .HasForeignKey(m => m.idChannel);
    }
}