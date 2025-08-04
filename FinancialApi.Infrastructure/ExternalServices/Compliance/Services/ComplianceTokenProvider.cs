using FinancialApi.Domain.ExternalServices.Compliance.Interfaces;
using FinancialApi.Domain.ExternalServices.Compliance.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FinancialApi.Infrastructure.ExternalServices.Compliance.Services;

public class ComplianceTokenProvider : IComplianceTokenProvider
{
    private readonly IComplianceAuthService _authApi;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ComplianceTokenProvider> _logger;

    private string? _accessToken;
    private DateTime _expiresAt;

    public ComplianceTokenProvider(
        IComplianceAuthService authApi,
        IConfiguration configuration,
        ILogger<ComplianceTokenProvider> logger)
    {
        _authApi = authApi;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<string> GetAccessTokenAsync()
    {

        try
        {
            if (!string.IsNullOrWhiteSpace(_accessToken) && DateTime.UtcNow < _expiresAt)
                return _accessToken!;

            var email = _configuration["ComplianceAuth:Email"]!;
            var password = _configuration["ComplianceAuth:Password"]!;

            var authCodeResponse = await _authApi.GetAuthCodeAsync(new AuthCodeRequest
            {
                Email = email,
                Password = password
            });
            _logger.LogInformation("Auth code recebido: {AuthCode}", authCodeResponse.AuthCode);

            var tokenResponse = await _authApi.GetTokenAsync(new TokenRequest
            {
                AuthCode = authCodeResponse.AuthCode
            });

            _accessToken = tokenResponse.Data.AccessToken;
            _expiresAt = DateTime.UtcNow.AddMinutes(1);

            return _accessToken;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao chamar GetAuthCodeAsync");
            throw;
        }
    }
}