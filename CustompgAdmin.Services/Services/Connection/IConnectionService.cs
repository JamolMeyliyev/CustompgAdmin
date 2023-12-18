

using CustompgAdmin.DataAccess.Entities;
using CustompgAdmin.Services.DTOs.Connection;

namespace CustompgAdmin.Services.Services.Connection;

public interface IConnectionService
{
    ValueTask<List<ConnectionDto>> GetConnectionsAsync();
    string ConnectServer(CreateConnectionDB createConnection);
    string ConnectServerForChaeckPassword(int id, string password);
}
