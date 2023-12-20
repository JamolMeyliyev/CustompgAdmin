using System.Text.Json.Serialization;

namespace CustompgAdmin.Blazor.Models.CreateModel;

public class CreateTableModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("createColumns")]
    public List<CreateColumnModel>? Columns { get; set; }
}
