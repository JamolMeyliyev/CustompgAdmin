using System.Text.Json.Serialization;

namespace CustompgAdmin.Blazor.Models.CreateModel;

public class CreateColumnModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("dataType")]
    public int DataType { get; set; }
    [JsonPropertyName("is_Nullable")]
    public bool? Is_Nullable { get; set; } = true;
    [JsonPropertyName("isPrimaryKey")]
    public bool? IsPrimaryKey { get; set; } = false;
    [JsonPropertyName("isUnique")]
    public bool? IsUnique { get; set; } = false;
    [JsonPropertyName("maxLength")]
    public int? MaxLength { get; set; }
    [JsonPropertyName("defaultData")]
    public object? DefaultData { get; set; }

}


