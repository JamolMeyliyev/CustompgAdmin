
using CustompgAdmin.DataAccess.Entities;


namespace CustompgAdmin.DataAccess.Repositories.Connection;

public class ConnectionRepository : IConnectionRepository
{
    
    public string _connectionString;
     
    public string Get()
    {
        return _connectionString;
    }
    public string ConnectServer(ConnectionEntity connectionEntity)
    {
        _connectionString = $"Host={connectionEntity.Host};Port={connectionEntity.Port};Username={connectionEntity.Username};Password={connectionEntity.Password};";
        
        return _connectionString;
    }
    

}
