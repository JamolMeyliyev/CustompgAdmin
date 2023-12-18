using CustompgAdmin.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustompgAdmin.Services.DTOs.Query;

public class QueryFilter
{
    public EnumQueryStatus? Status { get; set; }
}
