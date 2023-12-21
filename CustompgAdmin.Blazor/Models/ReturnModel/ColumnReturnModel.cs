using System.ComponentModel.DataAnnotations.Schema;

namespace CustompgAdmin.Blazor.Models.ReturnModel;

public class ColumnReturnModel
{
    
    public int Id { get; set; }

    [Column("column_name")]
    public string? Name { get; set; }

   

    


}
