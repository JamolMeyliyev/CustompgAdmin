using CustompgAdmin.Services.DTOs.Table;

namespace CustompgAdmin.Services.DTOs.Database;

public class DatabaseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<TableDto> TableDtos { get; set; }
}
