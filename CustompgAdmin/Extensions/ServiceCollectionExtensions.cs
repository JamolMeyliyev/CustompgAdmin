using CustompgAdmin.DataAccess.Repositories.ColumnRepository;
using CustompgAdmin.DataAccess.Repositories.Connection;
using CustompgAdmin.DataAccess.Repositories.TableRepository;
using CustompgAdmin.DataAccess.Repositories;
using CustompgAdmin.DataAccess.UnitOfWork;
using CustompgAdmin.Services.Mapper;
using CustompgAdmin.Services.Services.ColumnServices;
using CustompgAdmin.Services.Services.Connection;
using CustompgAdmin.Services.Services.Database;
using CustompgAdmin.Services.Services.TableServices;
using CustompgAdmin.Services.Services.Query;

namespace CustompgAdmin.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IConnectionRepository, ConnectionRepository>();
        services.AddScoped<IDatabaseRepository, DatabaseRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
        services.AddScoped<IColumnRepository, ColumnRepository>();
        services.AddScoped<IQueryRepository, QueryRepository>();



        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDatabaseService, DatabaseService>();
        services.AddScoped<IConnectionService, ConnectionService>();
        services.AddScoped<ITableService, TableService>();
        services.AddScoped<IColumnService, ColumnService>();
        services.AddScoped<IQueryService,QueryService>();

        services.AddAutoMapper(typeof(MapperProfile));

        return services;
    }
}



