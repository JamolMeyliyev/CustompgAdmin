using CustompgAdmin.DataAccess.Entities;
namespace CustompgAdmin.Services.DTOs.Column;

public class CreateColumn
{
    public required string Name { get; set; }
    public EnumDataType DataType { get; set; }
    public bool Is_Nullable { get; set; }
    public bool IsPrimaryKey { get; set; }
    public bool IsUnique { get; set; }
    public int? MaxLength { get; set; }
    public object? DefaultData { get; set; }
}
