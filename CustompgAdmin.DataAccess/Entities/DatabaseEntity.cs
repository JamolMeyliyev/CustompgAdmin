

namespace CustompgAdmin.DataAccess.Entities;

public class DatabaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Table> Tables { get; set; } = new List<Table>();
    public ICollection<Function> Functions { get; set; } = new List<Function>();
    public ICollection<Trigger> Triggers { get; set; } = new List<Trigger>();
    public ICollection<View> Views { get; set; } = new List<View>();
}
