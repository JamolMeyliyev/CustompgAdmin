using CustompgAdmin.Blazor.Models.ReturnModel;

namespace CustompgAdmin.UI.Models.ReturnModel;

public class TableReturnModel
{
    public int DatabaseId { get; set; }
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<ColumnReturnModel>? Columns { get; set; }
}
