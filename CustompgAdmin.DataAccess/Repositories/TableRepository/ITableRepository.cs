using Table = CustompgAdmin.DataAccess.Entities.Table;

namespace CustompgAdmin.DataAccess.Repositories.TableRepository;

public interface ITableRepository:IGenericRepository<Table,int>
{

}
