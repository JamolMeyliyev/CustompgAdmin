using CustompgAdmin.DataAccess.Context;
using CustompgAdmin.DataAccess.Entities;


namespace CustompgAdmin.DataAccess.Repositories.ColumnRepository;

public class ColumnRepository : GenericRepository<Column, int>, IColumnRepository
{
    public ColumnRepository(AppDbContext context) : base(context)
    {
    }
}
