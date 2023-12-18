namespace CustompgAdmin.UI.Models.ReturnModel;

public class TableReturnModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<ColumnReturnModel> Columns { get; set; }
}
