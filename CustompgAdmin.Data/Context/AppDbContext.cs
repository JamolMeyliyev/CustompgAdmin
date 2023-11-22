using CustompgAdmin.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustompgAdmin.Data.Context;

public class AppDbContext:DbContext
{
   public AppDbContext(ConnectionDB db)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(CreateConnectString());


    }
    public string CreateConnectString(ConnectionDB connectionDB)
    {
        return  connectionDB.Host + ";" + connectionDB.DatabaseName + ";" + connectionDB.Username + ";" + connectionDB.Password;
    }
    
}
