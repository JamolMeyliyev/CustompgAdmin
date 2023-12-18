using CustompgAdmin.DataAccess.Context;
using CustompgAdmin.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;


namespace CustompgAdmin.DataAccess.Repositories;

public class DatabaseRepository : GenericRepository<DatabaseEntity, int>,IDatabaseRepository
{
    
    private readonly AppDbContext _context;
    private readonly IServiceProvider _serviceProvider;

    public DatabaseRepository(AppDbContext appDbContext, IServiceProvider serviceProvider) : base(appDbContext)
    {
        _serviceProvider = serviceProvider;
        _context = appDbContext;
    }
    public override IQueryable<DatabaseEntity> SelectAll()
    {
        var entities = base.SelectAll();

        entities = entities
            .Include(x => x.Tables).ThenInclude(t => t.Columns);

        return entities;
    }
    public override async ValueTask<DatabaseEntity?> SelectByIdAsync(int id)
        => await this.SelectAll().FirstOrDefaultAsync(x => x.Id == id);
}
