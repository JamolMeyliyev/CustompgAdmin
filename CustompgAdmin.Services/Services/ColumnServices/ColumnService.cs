

using AutoMapper;
using CustompgAdmin.DataAccess.Entities;
using CustompgAdmin.DataAccess.Repositories;
using CustompgAdmin.DataAccess.Repositories.ColumnRepository;
using CustompgAdmin.DataAccess.Repositories.TableRepository;
using CustompgAdmin.Services.DTOs.Column;
using CustompgAdmin.Services.Services.Query;
using CustompgAdmin.Services.Services.TableServices;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace CustompgAdmin.Services.Services.ColumnServices;

public class ColumnService : IColumnService
{
    private readonly IColumnRepository _repos;
    private readonly ITableRepository _tableRepos;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDatabaseRepository _databaseRepos;
    private readonly ITableService _tableService;
    private readonly IMapper _mapper;
    private readonly IQueryService _queryService;

    public ColumnService(
        IColumnRepository repos,
        IMapper mapper,
        ITableRepository tableRepository,
        ITableService tableService,
        IDatabaseRepository databaseRepository,
        IServiceProvider serviceProvider,
        IQueryService queryService
        
        )
    {
        _repos = repos;
        _mapper = mapper;
        _tableRepos = tableRepository;
        _databaseRepos = databaseRepository;
        _serviceProvider = serviceProvider;
        _tableService = tableService;
        _queryService = queryService;      
    }
    public async ValueTask Create(AddColumnDto dto, int tableId)
    {
        var table = await _tableRepos.SelectByIdAsync(tableId);
        var database =await _databaseRepos.SelectByIdAsync(table.DatabaseId);

        NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString(database.Name));

        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand(QueryForAddColumn(dto, table),connection);
        command.ExecuteNonQuery();

        var column = _mapper.Map<Column>(dto);
        column.TableId = tableId;
        
        await _repos.InsertAsync(column);

    }

    public async ValueTask Delete(int tableId, int id)
    {
        var column = await _repos.SelectByIdAsync(id);
        var table = await _tableRepos.SelectByIdAsync(tableId);
        var database = await _databaseRepos.SelectByIdAsync(table.DatabaseId);

        NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString(database.Name));

        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand($"ALTER TABLE {table.Name.ToLower()}\r\nDROP COLUMN {column.Name};", connection);
        command.ExecuteNonQuery();

        await _repos.DeleteAsync(column);
    }

    public async ValueTask<List<ColumnReturnDto>> GetAll(int tableId)
    {
        var columns = await _repos.SelectAll().Where(x => x.TableId == tableId).ToListAsync();
        if(columns is null)
        {
            return new List<ColumnReturnDto>();
        }
        return _mapper.Map<List<ColumnReturnDto>>(columns);
        
    }

    public async ValueTask<Dictionary<string, List<object>?>> ViewDataByColumnId(int id,int tableId)
    {
        var column =await _repos.SelectByIdAsync(id);
        var table = await _tableRepos.SelectByIdAsync(tableId);
        var database = await _databaseRepos.SelectByIdAsync(table.DatabaseId);
        
        NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString(database.Name));

        connection.Open();

        
        Dictionary<string, List<object>?> dataDic = new Dictionary<string, List<object>?>();

        dataDic.Add(column.Name, connection.Query<object>(QueryForViewDataByColumnId(column, table)).ToList());
        

        return dataDic;

    }

    public async ValueTask<dynamic> WriteQuery(string query, int tableId)
    {
        var table = await _tableRepos.SelectByIdAsync(tableId);

        await _queryService.CreateAsync(query);

        return await _tableService.WriteQuery(table.DatabaseId, query);
    }

    public string QueryForAddColumn(AddColumnDto dto, Table table)
    {
        
        string query = $"ALTER TABLE IF EXISTS public.{table.Name.ToLower()}\r\n   ADD COLUMN ";

        if ((int)dto.DataType > 8 && dto.MaxLength is not null)
        {
            query += $" {dto.Name} {dto.DataType}({dto.MaxLength})  ";
        }
        else
        {
            query += $"{dto.Name} {dto.DataType} ";
        }
        if (!dto.Is_Nullable)
        {
            query += " NOT NULL ";
        }
        if (dto.DefaultData != null)
        {
            if ((int)dto.DataType > 4)
            {
                query += $"  DEFAULT '{dto.DefaultData}'";
            }
            else
            {
                query += $"  DEFAULT {dto.DefaultData}";
            }

        }
        query += ";";
        return query;
    }

    public string QueryForViewDataByColumnId(Column column, Table table)
    {
        return $"SELECT {column.Name} FROM {table.Name}";
    }

    public string GetConnectionString(string databaseName = null)
    {
        var scope = _serviceProvider.CreateScope();
        var _connectionRepos = scope.ServiceProvider.GetRequiredService<IConnectionRepository>();
        if (databaseName is null)
        {
            return _connectionRepos.Get();
        }
        return $"{_connectionRepos.Get()}Database = {databaseName}";

    }
}
