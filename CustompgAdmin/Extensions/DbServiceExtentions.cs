using CustompgAdmin.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CustompgAdmin.Extensions;

public static class DbServiceExtentions
{
    public static IServiceCollection AddDbContexts(
    this IServiceCollection service,
    IConfiguration configuration)
    {
        
        service.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql("Host =localhost;Port=5432;Username=postgres;Password=postgres;Database = postgres"));
       
        return service;
    }
}
