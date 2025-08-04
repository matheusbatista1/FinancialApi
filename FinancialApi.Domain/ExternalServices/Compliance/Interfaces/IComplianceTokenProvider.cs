namespace FinancialApi.Domain.ExternalServices.Compliance.Interfaces;

public interface IComplianceTokenProvider
{
    Task<string> GetAccessTokenAsync();
}