using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustompgAdmin.DataAccess.Entities;

[Table("connections")]
public partial class ConnectionEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(300)]
    public string Name { get; set; } = null!;

    [Column("host")]
    [StringLength(300)]
    public string Host { get; set; } = null!;

    [Column("port")]
    [StringLength(300)]
    public string Port { get; set; } = null!;

    [Column("username")]
    [StringLength(300)]
    public string Username { get; set; } = null!;

    [Column("password")]
    [StringLength(300)]
    public string Password { get; set; } = null!;

    [Column("created_date", TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }
}

