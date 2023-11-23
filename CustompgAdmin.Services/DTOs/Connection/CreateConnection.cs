
namespace CustompgAdmin.Services.DTOs.Connection;

public class CreateConnectionDB
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string Database { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
//Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=WbProjectmonitoring