using FinancialApi.Application.Commands.Account;
using FluentValidation;

namespace FinancialApi.Application.Validators.Account;

public class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountValidator()
    {
        RuleFor(x => x.Branch)
            .NotEmpty().WithMessage("A agência é obrigatória.")
            .Length(3).WithMessage("A agência deve ter exatamente 3 dígitos.")
            .Matches("^[0-9]{3}$").WithMessage("A agência deve conter apenas dígitos.");

        RuleFor(x => x.Account)
            .NotEmpty().WithMessage("O número da conta é obrigatório.")
            .Matches(@"^[0-9]{7}-[0-9]$").WithMessage("O número da conta deve seguir o formato XXXXXXX-X.");
    }
}