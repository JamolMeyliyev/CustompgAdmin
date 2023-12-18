
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace CustompgAdmin.DataAccess.Entities;

[Table("databases")]
public partial class DatabaseEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(300)]
    public string Name { get; set; } = null!;

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [InverseProperty("Database")]
    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
}

