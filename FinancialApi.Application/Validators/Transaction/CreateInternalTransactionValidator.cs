using FinancialApi.Application.Commands.Transaction;
using FluentValidation;

namespace FinancialApi.Application.Validators.Transaction;

public class CreateInternalTransactionValidator : AbstractValidator<CreateInternalTransactionCommand>
{
    public CreateInternalTransactionValidator()
    {
        RuleFor(x => x.Value)
            .GreaterThan(0).WithMessage("O valor da transferência deve ser maior que zero.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("A descrição é obrigatória.");

        RuleFor(x => x.ReceiverAccountId)
            .NotEqual(Guid.Empty).WithMessage("O ID da conta de destino é obrigatório.");
    }
}