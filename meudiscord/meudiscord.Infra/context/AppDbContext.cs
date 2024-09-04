using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<UserEntity> users { get; set; }
    public DbSet<ChannelEntity> channels { get; set; }
    public DbSet<MessageEntity> messages { get; set; }
    public DbSet<ServerEntity> servers { get; set; }
    public DbSet<TokenEntity> tokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ServerConfiguration());
        modelBuilder.ApplyConfiguration(new ChannelConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfiguration(new TokenConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}