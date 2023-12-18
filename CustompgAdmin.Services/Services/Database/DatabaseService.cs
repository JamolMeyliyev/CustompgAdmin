using AutoMapper;
using CustompgAdmin.Services.DTOs.Database;
using CustompgAdmin.DataAccess.Repositories;
using CustompgAdmin.DataAccess.Entities;
using CustompgAdmin.Services.Exceptions;
using CustompgAdmin.Services.Validator;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using static Dapper.SqlMapper;
using CustompgAdmin.Services.Services.TableServices;
using CustompgAdmin.Services.Services.Query;
using Microsoft.EntityFrameworkCore;

namespace CustompgAdmin.Services.Services.Database;

public class DatabaseService:IDatabaseService
{
    private readonly IDatabaseRepository _repos;
    private readonly IMapper _mapper;
    private readonly IServiceProvider _serviceProvider;
    private readonly ITableService _tableService;
    private readonly IQueryService _queryService;
    
   
    public DatabaseService(IDatabaseRepository repos,
        IMapper mapper,
        IConnectionRepository connection,
        IServiceProvider serviceProvider,
        ITableService tableService,
        IQueryService queryService
        )
    {
        _repos = repos;
        _mapper = mapper;
        _serviceProvider = serviceProvider;
        _tableService = tableService;
        _queryService = queryService;

        
    }

    public async ValueTask<DatabaseDto> CreateDatabaseAsync(CreateDatabase createDatabase)
    {
        var validator = new CreateDatabaseValidator();
        var result = validator.Validate(createDatabase);
        if (!result.IsValid)
        {
            throw new CreateDtoIsNotValidException("Database");
        }
       
        var database = _mapper.Map<DatabaseEntity>(createDatabase);


        if(database == null)
        {
            throw new NotFoundException("Database");
        }
        
        await _repos.InsertAsync(database);

        var scope = _serviceProvider.CreateScope();
        var _connectionRepos = scope.ServiceProvider.GetRequiredService<IConnectionRepository>();

        NpgsqlConnection connection = new NpgsqlConnection(_connectionRepos.Get());
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand($"CREATE DATABASE {database.Name}", connection);
        command.ExecuteNonQuery(); 

        return _mapper.Map<DatabaseDto>(database);

    }

    public async ValueTask DeleteAsync(int id)
    {
        var database = await _repos.SelectByIdAsync(id);

        if (database == null)
        {
            throw new NotFoundException("Database");
        }

        var scope = _serviceProvider.CreateScope();
        var _connectionRepos = scope.ServiceProvider.GetRequiredService<IConnectionRepository>();

        NpgsqlConnection connection = new NpgsqlConnection(_connectionRepos.Get());
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand($"DROP DATABASE {database.Name}", connection);
        command.ExecuteNonQuery();

        await _repos.DeleteAsync(database);
    }

    public async ValueTask<DatabaseDto> GetDatabaseById( int id)
    {
      var database = await _repos.SelectByIdAsync(id);

        if (database == null)
        {
            throw new NotFoundException("Database");
        }

        return _mapper.Map<DatabaseDto>(database);
    }

    public async ValueTask<List<DatabaseDto>> GetDatabasesAsync()
    {

        var databases = await  _repos.SelectAll().ToListAsync();

        if(databases == null)
        {
            return new List<DatabaseDto>();
        }
        
        return _mapper.Map<List<DatabaseDto>>(databases);
    }

    public string ViewSQLCode(CreateDatabase createDatabase)
    {
        return $"CREATE DATABASE {createDatabase.Name}\r\n    WITH\r\n    OWNER = postgres\r\n    ENCODING = 'UTF8'\r\n    LOCALE_PROVIDER = 'libc'\r\n    CONNECTION LIMIT = -1\r\n    IS_TEMPLATE = False;";
    }

    public async ValueTask<dynamic> WriteQuery(int id, string query)
    {
        await _queryService.CreateAsync(query);
        return await _tableService.WriteQuery(id, query);
    }
}
