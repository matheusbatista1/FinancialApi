using FinancialApi.Application.Commands.Card;
using FluentValidation;

namespace FinancialApi.Application.Validators.Card;
public class CreateCardValidator : AbstractValidator<CreateCardCommand>
{
    public CreateCardValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("O tipo do cartão é obrigatório.")
            .Must(t => t is "physical" or "virtual").WithMessage("O tipo do cartão deve ser 'physical' ou 'virtual'.");

        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("O número do cartão é obrigatório.")
            .Matches(@"^[0-9]{4}\s[0-9]{4}\s[0-9]{4}\s[0-9]{4}$").WithMessage("O número do cartão deve ter 16 dígitos no formato 'XXXX XXXX XXXX XXXX'.");

        RuleFor(x => x.Cvv)
            .NotEmpty().WithMessage("O CVV é obrigatório.")
            .Length(3).WithMessage("O CVV deve ter exatamente 3 dígitos.")
            .Matches("^[0-9]{3}$").WithMessage("O CVV deve conter apenas dígitos.");
    }
}