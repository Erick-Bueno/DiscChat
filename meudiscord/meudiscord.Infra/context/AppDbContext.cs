using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<UserModel> users { get; set; }
    public DbSet<ChannelModel> channels { get; set; }
    public DbSet<MessageModel> messages { get; set; }
    public DbSet<ServerModel> servers { get; set; }
    public DbSet<TokenModel> tokens { get; set; }

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