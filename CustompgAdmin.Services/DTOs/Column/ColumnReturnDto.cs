
using System.ComponentModel.DataAnnotations.Schema;

namespace CustompgAdmin.Services.DTOs.Column;

public class ColumnReturnDto
{
    public int Id { get; set; }

    [Column("column_name")]
    public string? Name { get; set; }

    
}
