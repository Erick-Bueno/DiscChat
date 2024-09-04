using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IDbTransaction BeginTransaction()
    {
        var transaction = _appDbContext.Database.BeginTransaction();
        return transaction.GetDbTransaction();
    }
}