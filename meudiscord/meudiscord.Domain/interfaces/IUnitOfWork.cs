using System.Data;

public interface IUnitOfWork
{
    IDbTransaction BeginTransaction();
}