

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustompgAdmin.DataAccess.Entities;

[Table("query_stories")]
public partial class QueryEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("status")]
    public int? Status { get; set; }

    [Column("query")]
    public string? Query { get; set; }
}
