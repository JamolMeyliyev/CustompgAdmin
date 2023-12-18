using CustompgAdmin.DataAccess.Context;
using CustompgAdmin.DataAccess.Entities;

namespace CustompgAdmin.DataAccess.Repositories;

public class QueryRepository : GenericRepository<QueryEntity, int>,IQueryRepository

{
    public QueryRepository(AppDbContext context) : base(context)
    {
    }
}
