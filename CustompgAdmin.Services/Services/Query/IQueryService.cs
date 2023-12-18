using CustompgAdmin.DataAccess.Entities;
using CustompgAdmin.Services.DTOs.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustompgAdmin.Services.Services.Query;

public interface IQueryService
{
    ValueTask<List<QueryEntity>> GetAll(QueryFilter filter);
    ValueTask CreateAsync(string query);
    ValueTask<QueryEntity> GetByIdAsync(int id);
    
}
