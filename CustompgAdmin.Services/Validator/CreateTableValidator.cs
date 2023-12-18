
using CustompgAdmin.Services.DTOs.Table;
using FluentValidation;
using FluentValidation.Validators;

namespace CustompgAdmin.Services.Validator;

public class CreateTableValidator:AbstractValidator<CreateTable>
{
    public CreateTableValidator()
    {
        RuleFor(s => s.CreateColumns.Count >= 1);
        RuleFor(s => s.Name).NotNull();
    }
}
