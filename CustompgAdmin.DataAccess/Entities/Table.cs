

namespace CustompgAdmin.DataAccess.Entities;

public class Table:BaseEntity
{
    public int DatabaseId { get; set; }

    public ICollection<Column> Columns { get; set;}
}
