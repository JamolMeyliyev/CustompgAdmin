using CustompgAdmin.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Table = CustompgAdmin.DataAccess.Entities.Table;

namespace CustompgAdmin.DataAccess.Repositories.TableRepository;

public class TableRepository : GenericRepository<Table, int>, ITableRepository
{
    public TableRepository(AppDbContext context) : base(context)
    {
    }
    public override IQueryable<Table> SelectAll()
    {
        var entities = base.SelectAll();

        entities = entities
            .Include(x => x.Columns);

        return entities;
    }
    public override async ValueTask<Table?> SelectByIdAsync(int id)
        => await this.SelectAll().FirstOrDefaultAsync(x => x.Id == id);
}
