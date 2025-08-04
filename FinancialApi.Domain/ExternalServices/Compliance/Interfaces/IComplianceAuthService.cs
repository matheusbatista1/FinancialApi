using FinancialApi.Domain.ExternalServices.Compliance.Models;
using Refit;

namespace FinancialApi.Domain.ExternalServices.Compliance.Interfaces;

public interface IComplianceAuthService
{
    [Post("/auth/code")]
    Task<AuthCodeResponse> GetAuthCodeAsync([Body] AuthCodeRequest request);

    [Post("/auth/token")]
    Task<TokenResponse> GetTokenAsync([Body] TokenRequest request);

    [Post("/auth/refresh")]
    Task<TokenResponse> RefreshTokenAsync([Body] RefreshTokenRequest request);
}