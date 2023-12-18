using AutoMapper;
using CustompgAdmin.DataAccess.Entities;
using CustompgAdmin.DataAccess.Repositories;
using CustompgAdmin.DataAccess.Repositories.ColumnRepository;
using CustompgAdmin.DataAccess.Repositories.TableRepository;
using CustompgAdmin.DataAccess.UnitOfWork;
using CustompgAdmin.Services.DTOs.Table;
using CustompgAdmin.Services.Exceptions;
using CustompgAdmin.Services.Services.Query;
using CustompgAdmin.Services.Validator;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Column = CustompgAdmin.DataAccess.Entities.Column;
using Table = CustompgAdmin.DataAccess.Entities.Table;


namespace CustompgAdmin.Services.Services.TableServices;

public class TableService : ITableService
{
    
    private readonly ITableRepository _repos;
    private readonly IMapper _mapper;
    private readonly IDatabaseRepository _databaseRepos;
    private readonly IColumnRepository _columnRepository;
    private readonly IUnitOfWork _unitOfWork;
    public readonly IServiceProvider _serviceProvider;
    private readonly IQueryService _queryService;

    public TableService(
        
        ITableRepository repos,
        IMapper mapper,
        IDatabaseRepository databaseRepository,
        IColumnRepository columnRepos,
        IUnitOfWork unitOfWork,
        IServiceProvider serviceProvider,
        IQueryService queryService

                       )

    {
        
        _repos = repos;
        _mapper = mapper;
        _databaseRepos = databaseRepository;
        _columnRepository = columnRepos;
        _unitOfWork = unitOfWork;
        _serviceProvider = serviceProvider;
        _queryService = queryService;
    }

    public async ValueTask<List<TableDto>> All(int databaseId)
    {
        var tableList = await _repos.SelectAll().Where(s => s.DatabaseId == databaseId).ToListAsync();

        if(tableList is null )
        {
            return new List<TableDto>();
        }

        return _mapper.Map<List<TableDto>>(tableList);
    }
    public async ValueTask CreateTableAsync(CreateTable dto,int databaseId)
    {
        var validator = new CreateTableValidator();
        var resultForValid = validator.Validate(dto);
        if(!resultForValid.IsValid)
        {
            throw new CreateDtoIsNotValidException("Table");
        }

        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                
                var entity = new Table();
                entity.Name = dto.Name;
                entity.DatabaseId = databaseId;

                var storedEntity = await _repos.InsertAsync(entity);

                if(dto.CreateColumns.Count() > 0)
                {
                    foreach(var column in dto.CreateColumns)
                    {
                        var columnEntity = new Column();
                        columnEntity.TableId = storedEntity.Id;
                        columnEntity.Name = column.Name;
                        await _columnRepository.InsertAsync(columnEntity);
                    }
                }

                var database = await _databaseRepos.SelectByIdAsync(databaseId);

                if (database == null)
                {
                    throw new NotFoundException("Database");
                }

                NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString(database.Name));
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(CreateTableForQuery(dto), connection);
                command.ExecuteNonQuery();

                _unitOfWork.Commit();
                
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
        }
    }
    public async ValueTask DeleteTableAsync(int id)
    {
        var table = await _repos.SelectByIdAsync(id);

        if (table is null)
        {
            throw new DirectoryNotFoundException("Table");
        }

        await _repos.DeleteAsync(table);

        var query = $"DROP TABLE {table.Name.ToLower()}";

        var database = await _databaseRepos.SelectByIdAsync(table.DatabaseId);

        if (database == null)
        {
            throw new NotFoundException("Database");
        }

        NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString(database.Name));
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.ExecuteNonQuery();


    }
    public async ValueTask<TableDto> GetByTableIdAsync(int id)
    {
        var table =await _repos.SelectByIdAsync(id);

        if(table is null)
        {
            throw new NotFoundException("Table");
        }
        
        
        return _mapper.Map<TableDto>(table);
       
    }
    public async ValueTask<Dictionary<string, List<object>>> ViewData(int id,int databasId)
    {
      
        
        var table = await _repos.SelectByIdAsync(id);

        if (table == null)
        {
            throw new NotFoundException("Table");
        }

        var database = await _databaseRepos.SelectByIdAsync(databasId);

        if(database == null)
        {
            throw new NotFoundException("Database");
        }

        NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString(database.Name));

        connection.Open();
        var columns = table.Columns;
        Dictionary<string, List<object>?> dataDic = new Dictionary<string, List<object>?>();
        

        foreach (var column in columns)
        {

          
            var query = $"SELECT {column.Name} FROM {table.Name}";

            dataDic.Add(column.Name, connection.Query<object>(query).ToList());
        }
        
        return dataDic;
        
 
    }
    public async ValueTask<dynamic> WriteQuery(int databaseId, string query)
    {
        var database = await _databaseRepos.SelectByIdAsync(databaseId);

        if (database is null)
        {
            throw new DirectoryNotFoundException("Database");
        }

        NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString(database.Name));
        connection.Open();

        string[] strings= query.Split(' ');
        if (strings[0].ToLower() == "select")
        {
            
            string columnsPart = query.ToLower().Split(new[] { "select", "from" }, StringSplitOptions.None)[1].Trim();
            string[] columns = columnsPart.Split(',');

            Dictionary<string, List<object>?> dataDic = new Dictionary<string, List<object>?>();
            foreach (var column in columns)
            {


                var queryString = $"SELECT {column} FROM {strings[strings.Length -1].ToLower()}";

                dataDic.Add(column, connection.Query<object>(queryString).ToList());
            }

            await _queryService.CreateAsync(query);

            return dataDic;

        }
        if(strings[0].ToLower() == "create")
        {
            if(strings[1].ToLower() == "database")
            {
                

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.ExecuteNonQuery();
                DatabaseEntity databaseEntity = new DatabaseEntity();

                databaseEntity.Name = strings[strings.Length -1];
                databaseEntity.Tables = new List<Table>();
                databaseEntity.CreatedDate = DateTime.Now;
                
                await _databaseRepos.InsertAsync(databaseEntity);

                return "CREATED DATABASE";

            }
            if (strings[1].ToLower() == "table")
            {
                
                string columnsPart = query.Split(new[] { "(", ")" }, StringSplitOptions.None)[1].Trim();

                string tableName = query.ToLower().Split(new[] { "table", "(" }, StringSplitOptions.None)[1].Trim();

                string[] columnParameters = columnsPart.Split(',');

                List<string> columnNames = new List<string>();
                foreach (var i in columnParameters)
                {
                   
                    string[] parameters = i.Split(' ');

                    columnNames.Add(parameters[0]);

                }
                using (var transaction = _unitOfWork.BeginTransaction())
                { 
                    try
                    {

                        var table = new Table();
                        table.Name = tableName;
                        table.DatabaseId = databaseId;
                        var storedEntity = await _repos.InsertAsync(table);


                        if (columnNames.Count > 0)
                        {
                            foreach (var columnName in columnNames)
                            {
                                var columnEntity = new Column();
                                columnEntity.TableId = storedEntity.Id;
                                columnEntity.Name = columnName;
                                await _columnRepository.InsertAsync(columnEntity);
                            }
                        }
                        _unitOfWork.Commit();

                        NpgsqlCommand command = new NpgsqlCommand(query, connection);
                        command.ExecuteNonQuery();

                        return "CREATED TABLE";

                    }
                    catch (Exception ex)
                    {
                        _unitOfWork.Rollback();
                        throw new Exception(ex.Message);
                    }
                }


            }
        }
        

        return "SUCCESS";
    }
    public string GetConnectionString(string databaseName = null)
    {
        var scope = _serviceProvider.CreateScope();
        var _connectionRepos = scope.ServiceProvider.GetRequiredService<IConnectionRepository>();
        if(databaseName is null)
        {
            return _connectionRepos.Get();
        }
        return $"{_connectionRepos.Get()}Database = {databaseName}";

    }
    public string CreateTableForQuery(CreateTable dto)
    {
        string query = $"CREATE TABLE IF NOT EXISTS {dto.Name}(";

        foreach (var c in dto.CreateColumns)
        {
            string column = "";
           
            
            if((int)c.DataType >8 && c.MaxLength is not null)
            { 
                column += $" {c.Name} {c.DataType}({c.MaxLength})  ";
            }
            else
            {
                column += $"{c.Name} {c.DataType} ";
            }
            if (!c.Is_Nullable)
            {
                column += " NOT NULL ";
            }
            if (c.IsPrimaryKey)
            {
                column += " PRIMARY KEY ";
            }
            if(c.IsUnique)
            {
                column += " UNIQUE ";
            }
            if(c.DefaultData!= null )
            {
                if((int)c.DataType > 4)
                {
                    column += $"  DEFAULT '{c.DefaultData}'";
                }
                else
                {
                    column += $"  DEFAULT {c.DefaultData}";
                }
               
            }
            
            query += (column + ",");

        }
        query = query.Substring(0, query.Length - 1);

        query += ");";

        return query;
    }
    public string GetTableForQuery(Table dto)
    {
        //return  $"SELECT\r\n  " +
        //   $"  col.column_name,\r\n " +
        //   $"   col.data_type,\r\n " +
        //   $"   col.character_maximum_length,\r\n" +
        //   $"   col.is_nullable,\r\n " +
        //   $"   col.column_default,\r\n" +
        //   $"   cons.constraint_name,\r\n" +
        //   $"   cons.constraint_type\r\nFROM\r\n " +
        //   $"   information_schema.columns col\r\n" +
        //   $"LEFT JOIN\r\n    information_schema.constraint_column_usage cons_col ON col.column_name = cons_col.column_name AND col.table_name = cons_col.table_name\r\n" +
        //   $"LEFT JOIN\r\n    information_schema.table_constraints cons ON cons_col.constraint_name = cons.constraint_name\r\nWHERE\r\n    col.table_name = '{dto.Name.ToLower()}';";
        return $"SELECT *  FROM {dto.Name.ToLower()}";
    }
    public async ValueTask WriteScripts(int id,string scriptQuery)
    {
        var table = await _repos.SelectByIdAsync(id);

        if(table is null)
        {
            throw new NotFoundException("Table");
        }


        var database = await _databaseRepos.SelectByIdAsync(table.DatabaseId);

        if(database is null)
        {
            throw new NotFoundException("Database");
        }

        NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString(database.Name));
        connection.Open();
        NpgsqlCommand command = new NpgsqlCommand(scriptQuery, connection);
        command.ExecuteNonQuery();

        await _queryService.CreateAsync(scriptQuery);

    }
    public async ValueTask<string> GetScripts(int id, int databaseId, int scriptId)
    {
        string result = "";
        string columnsString = "";

        var table = await _repos.SelectByIdAsync(id);
        
        foreach(var c in table.Columns)
        {
            columnsString += $"{c.Name.ToLower()}";
            if(scriptId == 3)
            {
                columnsString += "=?";
            }
            columnsString += ",";
        }
        columnsString.Substring(0, columnsString.Length - 1);

        if(table is null)
        {
            throw new NotFoundException("Table");
        }

        switch (scriptId)
        {
            case 0: result = "";break;
            case 1: result = $"SELECT {columnsString}\r\n\tFROM public.{table.Name.ToLower()};"; break;
            case 2: result = $"INSERT INTO public.{table.Name.ToLower()}(\r\n\t{columnsString})\r\n\tVALUES ();" ; break;
            case 3: result = $"UPDATE public.{table.Name.ToLower()}\r\n\tSET {columnsString}\r\n\tWHERE <condition>;"; break;
            case 4: result = $"DELETE FROM public.{table.Name.ToLower()}\r\n\tWHERE <condition>;"; break;
        }
        return result;
    }

   
}
