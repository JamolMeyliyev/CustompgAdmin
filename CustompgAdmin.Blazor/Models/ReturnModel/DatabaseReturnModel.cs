namespace CustompgAdmin.UI.Models.ReturnModel;

public class DatabaseReturnModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<TableReturnModel>? Tables { get; set; }
}
