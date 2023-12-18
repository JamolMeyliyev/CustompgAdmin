using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Table = CustompgAdmin.DataAccess.Entities;
namespace CustompgAdmin.DataAccess.Entities;

[Table("columns")]
public partial class Column
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(30)]
    public string Name { get; set; } = null!;

    [Column("table_id")]
    public int TableId { get; set; }

    [ForeignKey("TableId")]
    [InverseProperty("Columns")]
    public virtual Table Table { get; set; } = null!;
}

