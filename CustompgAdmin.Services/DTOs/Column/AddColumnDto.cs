using CustompgAdmin.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustompgAdmin.Services.DTOs.Column;

public class AddColumnDto
{
    public required string Name { get; set; }
    public EnumDataType DataType { get; set; }
    public bool Is_Nullable { get; set; }
    public int? MaxLength { get; set; }
    public object? DefaultData { get; set; }

}
