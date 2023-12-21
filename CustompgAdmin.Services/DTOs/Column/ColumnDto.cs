using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustompgAdmin.Services.DTOs.Column;

public class ColumnDto
{
    public int Id { get; set; }

    [Column("column_name")]
    public string? Name { get; set; }

    

    //[Column("character_maximum_length")]
    //public int MaxLength { get; set; }

    //[Column("is_nullable")]
    //public string? IsNullable { get; set; }

    //[Column("column_default")]
    //public string? Default { get; set; }

    //[Column("constraint_name")]
    //public string? ConstraintName { get; set; }


}
