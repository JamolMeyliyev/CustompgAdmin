


using CustompgAdmin.Services.DTOs.Database;

namespace CustompgAdmin.Services.Services.Database;

public interface IDatabaseService
{
    ValueTask<DatabaseDto> CreateDatabaseAsync(CreateDatabase createDatabase);
    string ViewSQLCode(CreateDatabase createDatabase);
    ValueTask<List<DatabaseDto>> GetDatabasesAsync();
    ValueTask<DatabaseDto> GetDatabaseById(int id);
    ValueTask DeleteAsync(int id);
    ValueTask<dynamic> WriteQuery(int id, string query);

}
