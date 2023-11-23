using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustompgAdmin.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    IDbContextTransaction CurrencyTransaction { get; }
    IDbContextTransaction BeginTransaction();
    void Commit();
    void Rollback();
}
