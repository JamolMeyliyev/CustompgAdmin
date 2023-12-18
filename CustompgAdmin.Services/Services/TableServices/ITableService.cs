

using CustompgAdmin.Services.DTOs.Table;

namespace CustompgAdmin.Services.Services.TableServices;

public interface ITableService
{
    ValueTask<List<TableDto>> All(int databaseId);
    ValueTask CreateTableAsync(CreateTable dto,int databaseId);
    ValueTask<TableDto> GetByTableIdAsync(int id);
    ValueTask DeleteTableAsync(int id);
    ValueTask<dynamic> WriteQuery(int databaseId,string query);
    ValueTask WriteScripts(int id, string scriptId);
    ValueTask<string> GetScripts(int id, int databaseId, int scriptId);
    ValueTask<Dictionary<string,List<object>>> ViewData(int id,int databasId);

     
}
