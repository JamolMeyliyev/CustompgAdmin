using CustompgAdmin.Services.DTOs.Column;
namespace CustompgAdmin.Services.DTOs.Table;

public class TableDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<ColumnReturnDto> ColumnDtos { get; set; }
}
