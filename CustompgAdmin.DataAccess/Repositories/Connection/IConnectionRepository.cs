



using CustompgAdmin.DataAccess.Entities;
using Npgsql;

namespace CustompgAdmin.DataAccess.Repositories;

public interface IConnectionRepository
{
    string Get();
    string ConnectServer(ConnectionEntity connection);

}

