


using CustompgAdmin.Services.DTOs.Database;

namespace CustompgAdmin.Services.Services.Database;

public interface IDatabaseService
{
    ValueTask<DatabaseDto> CreateDatabaseAsync(CreateDatabase createDatabase);
    ValueTask<string> ViewSQLCode(CreateDatabase createDatabase);

}
