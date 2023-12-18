namespace CustompgAdmin.UI.Models.ReturnModel;

public class ConnectionDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Host { get; set; }
    public required string Port { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}

