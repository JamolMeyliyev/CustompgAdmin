

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustompgAdmin.DataAccess.Entities;

[Table("tables")]
public partial class Table
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(300)]
    public string Name { get; set; } = null!;

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("database_id")]
    public int DatabaseId { get; set; }

    [InverseProperty("Table")]
    public virtual ICollection<Column> Columns { get; set; } = new List<Column>();

    [ForeignKey("DatabaseId")]
    [InverseProperty("Tables")]
    public virtual DatabaseEntity Database { get; set; } = null!;
}
