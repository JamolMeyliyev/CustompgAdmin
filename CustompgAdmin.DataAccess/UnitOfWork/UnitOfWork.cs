
using CustompgAdmin.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace CustompgAdmin.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AppDbContext context;
    public UnitOfWork(IServiceProvider serviceProvider,
        AppDbContext context)
    {
        _serviceProvider = serviceProvider;
        this.context = context;
    }

    public void Commit()
    {
        if (context.Database.CurrentTransaction != null)
        {
            context.Database.CurrentTransaction.Commit();
        }
    }

    public void Rollback()
    {
        if (context.Database.CurrentTransaction != null)
        {
            context.Database.CurrentTransaction.Rollback();
        }
    }

    public IDbContextTransaction BeginTransaction()
    {
        return context.Database.BeginTransaction();
    }

    public IDbContextTransaction CurrencyTransaction => context.Database.CurrentTransaction;
}
