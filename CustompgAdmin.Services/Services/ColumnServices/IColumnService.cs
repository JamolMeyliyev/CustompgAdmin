

using CustompgAdmin.Services.DTOs.Column;

namespace CustompgAdmin.Services.Services.ColumnServices;

public interface IColumnService
{
    ValueTask<List<ColumnReturnDto>> GetAll(int tableId);
    ValueTask Create(AddColumnDto dto,int tableId);
    ValueTask Delete(int tableId,int id);  
    ValueTask<dynamic> WriteQuery(string query,int tableId);
    ValueTask<Dictionary<string, List<object>>> ViewDataByColumnId(int id,int tableId);

}
