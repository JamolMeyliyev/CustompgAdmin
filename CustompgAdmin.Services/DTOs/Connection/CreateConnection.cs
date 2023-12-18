
namespace CustompgAdmin.Services.DTOs.Connection;

public class CreateConnectionDB
{
    public required string Name { get; set; }
    public required string Host { get; set; }
    public required string Port { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
//Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=WbProjectmonitoring
