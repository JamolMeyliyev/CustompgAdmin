using CustompgAdmin.Data.Context;
using CustompgAdmin.DataAccess.Entities;

namespace CustompgAdmin.DataAccess.Repositories;

public class DatabaseRepository : GenericRepository<DatabaseEntity, int>
{
    public DatabaseRepository(AppDbContext appDbContext) : base(appDbContext)
    {

    }
}
