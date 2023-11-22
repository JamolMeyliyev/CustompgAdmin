



using CustompgAdmin.DataAccess.Entities;

namespace CustompgAdmin.DataAccess.Repositories.Connection;

public interface IConnectionRepository
{
    void CreateMigrateUpdateDatabase(ConnectionDB connectionDB);
}
