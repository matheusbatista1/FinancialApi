using FinancialApi.Application.Commands.Auth;
using FluentValidation;

namespace FinancialApi.Application.Validators.Auth;

public class LoginPersonValidator : AbstractValidator<LoginPersonCommand>
{
    public LoginPersonValidator()
    {
        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("O documento é obrigatório.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.");
    }
}