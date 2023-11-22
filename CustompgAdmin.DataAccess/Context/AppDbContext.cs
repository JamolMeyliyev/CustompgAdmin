
using Microsoft.EntityFrameworkCore;

namespace CustompgAdmin.DataAccess.Context;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
}
