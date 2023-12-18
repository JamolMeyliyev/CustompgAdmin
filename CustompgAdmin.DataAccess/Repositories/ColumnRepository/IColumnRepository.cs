using CustompgAdmin.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustompgAdmin.DataAccess.Repositories.ColumnRepository;

public interface IColumnRepository:IGenericRepository<Column,int>
{
}
