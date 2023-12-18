

using AutoMapper;
using CustompgAdmin.DataAccess.Context;
using CustompgAdmin.DataAccess.Entities;
using CustompgAdmin.DataAccess.Repositories;
using CustompgAdmin.DataAccess.Repositories.Connection;
using CustompgAdmin.Services.DTOs.Connection;
using CustompgAdmin.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustompgAdmin.Services.Services.Connection;

public class ConnectionService : IConnectionService
{
    
    private readonly IMapper _mapper;
    private readonly IServiceProvider _serviceProvider;
    private readonly AppDbContext _context;
    public ConnectionService(IMapper mapper,IServiceProvider serviceProvider,AppDbContext context)
    {
        _mapper = mapper;
        _serviceProvider = serviceProvider;
        _context= context;
    }

    public string ConnectServer(CreateConnectionDB createConnection)
    {
        var serverEntity = _mapper.Map<ConnectionEntity>(createConnection);

        var scope = _serviceProvider.CreateScope();
        var _connectionRepos = scope.ServiceProvider.GetRequiredService<IConnectionRepository>();

        _context.Connections.Add(serverEntity);
        _context.SaveChanges();

        return _connectionRepos.ConnectServer(serverEntity);

    }
    public string ConnectServerForChaeckPassword(int id, string password)
    {

        var serverEntity = _context.Connections.FirstOrDefault(c => c.Id == id && c.Password == password);
        if(serverEntity == null)
        {
            throw new Exception("Password xato");
        }

        var scope = _serviceProvider.CreateScope();
        var _connectionRepos = scope.ServiceProvider.GetRequiredService<IConnectionRepository>();

        return _connectionRepos.ConnectServer(serverEntity);
    }

    public async ValueTask<List<ConnectionDto>> GetConnectionsAsync()
    {
        return _mapper.Map<List<ConnectionDto>>(await _context.Connections.ToListAsync());
    }
}
