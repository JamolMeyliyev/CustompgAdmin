using CustompgAdmin.DataAccess.Entities;
using CustompgAdmin.DataAccess.Repositories;
using CustompgAdmin.Services.DTOs.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustompgAdmin.Services.Services.Query;

public class QueryService : IQueryService
{
    private readonly IQueryRepository _repos;
    public QueryService(IQueryRepository repos)
    {
        _repos = repos;
    }
    public async ValueTask CreateAsync(string query)
    {
        string[] strings = query.Split(' ');

        var queryStory = new QueryEntity();

        if (strings[0].ToLower() == "create")
        {
            queryStory.Status = (int)EnumQueryStatus.CREATE;
        }
        if (strings[0].ToLower() == "select")
        {
            queryStory.Status = (int)EnumQueryStatus.SELECT;
        }
        if (strings[0].ToLower() == "drop" || strings[0].ToLower() == "delete")
        {
            queryStory.Status = (int)EnumQueryStatus.DELETE;
        }
        else
        {
            queryStory.Status = (int)EnumQueryStatus.OTHER;
        }
        queryStory.Query = query;

        await _repos.InsertAsync(queryStory);
    }

    public async ValueTask<List<QueryEntity>> GetAll(QueryFilter filter)
    {
        var data = _repos.SelectAll();
        if(filter.Status is not null)
        {
            data = data.Where(x => x.Status == (int)filter.Status);
        }
        return await data.ToListAsync();
    }

    public async ValueTask<QueryEntity> GetByIdAsync(int id)
    {
        var query = await _repos.SelectByIdAsync(id);
        return query;
    }
}
