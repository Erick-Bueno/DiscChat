using Microsoft.EntityFrameworkCore;

public class DbFixture:IDisposable
{
    private readonly AppDbContext _dbContext;
    public readonly string databaseName = $"context-{Guid.NewGuid()}";
    public readonly string connectionString;
    private bool _disposed;

    public DbFixture(){
        connectionString = $"Server=localhost;Database={databaseName};Uid=root;Pwd=sirlei231;";
        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));
        _dbContext = new AppDbContext(builder.Options);
        _dbContext.Database.Migrate();
    }
    public void Dispose(){
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing){
        if(!_disposed){
            if(disposing){
                _dbContext.Database.EnsureDeleted();
            }
            _disposed = true;
        }
    }
}