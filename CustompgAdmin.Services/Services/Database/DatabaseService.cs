

using CustompgAdmin.DataAccess.Repositories;
using CustompgAdmin.DataAccess.UnitOfWork;
using CustompgAdmin.Services.DTOs.Database;

namespace CustompgAdmin.Services.Services.Database;

public class DatabaseService:IDatabaseService
{
    private readonly IDatabaseRepository _repos;
    private readonly IUnitOfWork _unitOfWork;
    public DatabaseService(IDatabaseRepository repos, IUnitOfWork unitOfWork)
    {
        _repos = repos;
        _unitOfWork = unitOfWork;
    }

    public ValueTask<DatabaseDto> CreateDatabaseAsync(CreateDatabase createDatabase)
    {
        
    }

    public ValueTask<string> ViewSQLCode(CreateDatabase createDatabase)
    {
        
    }
}
