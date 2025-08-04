using FinancialApi.Application.Commands.Transaction;
using FluentValidation;
namespace FinancialApi.Application.Validators.Transaction;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionValidator()
    {
        RuleFor(x => x.Value)
            .NotEqual(0).WithMessage("O valor da transação não pode ser zero.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("A descrição é obrigatória.");
    }
}