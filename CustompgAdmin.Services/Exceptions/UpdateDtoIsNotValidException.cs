

namespace CustompgAdmin.Services.Exceptions;

public class UpdateDtoIsNotValidException:Exception
{
  public  UpdateDtoIsNotValidException(string message) :base($"{message} update dto is not valid") { }  
}
