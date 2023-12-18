using CustompgAdmin.DataAccess.Entities;
using CustompgAdmin.Services.DTOs.Column;
using Npgsql.Internal.TypeHandlers.NetworkHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustompgAdmin.Services.DTOs.Table;

public class CreateTable
{
    public required string Name { get; set; }
    public List<CreateColumn> CreateColumns { get; set; } = new List<CreateColumn>();
}

