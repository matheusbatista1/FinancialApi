using FinancialApi.Application.Commands.People;
using FinancialApi.Domain.ExternalServices.Compliance.Interfaces;
using FinancialApi.Domain.ExternalServices.Compliance.Models;
using FluentValidation;

namespace FinancialApi.Application.Validators.People;

public class CreatePersonValidator : AbstractValidator<CreatePersonCommand>
{
    private readonly IComplianceValidationService _complianceApi;
    private readonly IComplianceTokenProvider _tokenProvider;

    public CreatePersonValidator(IComplianceValidationService complianceApi, IComplianceTokenProvider tokenProvider)
    {
        _complianceApi = complianceApi;
                _tokenProvider = tokenProvider;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.");

        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("O documento é obrigatório.")
            .MustAsync(IsValidCpfOrCnpjAsync)
            .WithMessage("O documento deve ser um CPF ou CNPJ válido na base do Compliance.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");
    }

    private async Task<bool> IsValidCpfOrCnpjAsync(string document, CancellationToken cancellationToken)
    {
        var clean = new string(document.Where(char.IsDigit).ToArray());

        var token = await _tokenProvider.GetAccessTokenAsync();
        var bearer = $"Bearer {token}";

        if (clean.Length == 11)
        {
            var result = await _complianceApi.ValidateDocumentCpfAsync(new DocumentRequest(clean), bearer);
            return result.Data.Status == 1;
        }

        if (clean.Length == 14)
        {
            var result = await _complianceApi.ValidateDocumentCnpjAsync(new DocumentRequest(clean), bearer);
            return result.Data.Status == 1;
        }

        return false;
    }
}