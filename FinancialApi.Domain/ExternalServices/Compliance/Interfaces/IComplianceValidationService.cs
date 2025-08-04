using FinancialApi.Domain.ExternalServices.Compliance.Models;
using Refit;

namespace FinancialApi.Domain.ExternalServices.Compliance.Interfaces;

public interface IComplianceValidationService
{
    [Post("/cpf/validate")]
    Task<DocumentResponse> ValidateDocumentCpfAsync([Body] DocumentRequest request, [Header("Authorization")] string authorization);

    [Post("/cnpj/validate")]
    Task<DocumentResponse> ValidateDocumentCnpjAsync([Body] DocumentRequest request, [Header("Authorization")] string authorization);
}