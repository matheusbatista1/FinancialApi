namespace FinancialApi.Domain.ExternalServices.Compliance.Models;

public class TokenResponse
{
    public bool Success { get; set; }
    public TokenData Data { get; set; } = null!;
}

public class TokenData
{
    public string IdToken { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}