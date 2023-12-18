namespace CustompgAdmin.Services.Exceptions;

public class CreateDtoIsNotValidException:Exception
{
  public CreateDtoIsNotValidException(string message) : base($"{message} create dto is not valid") { }
}
