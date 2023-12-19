using System.ComponentModel.DataAnnotations.Schema;

namespace CustompgAdmin.UI.Models.ReturnModel;

public class ColumnReturnModel
{
    public int Id { get; set; }

    [Column("column_name")]
    public string? Name { get; set; }

    [Column("data_type")]
    public string? DataType { get; set; }

    


}
