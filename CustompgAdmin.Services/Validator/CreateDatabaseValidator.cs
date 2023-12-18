

using CustompgAdmin.Services.DTOs.Database;
using FluentValidation;

namespace CustompgAdmin.Services.Validator;

public class CreateDatabaseValidator:AbstractValidator<CreateDatabase>
{
    public CreateDatabaseValidator()
    {
        RuleFor(s => s.Name).NotNull();
    }
}
