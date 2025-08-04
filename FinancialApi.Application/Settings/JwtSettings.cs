namespace FinancialApi.Application.Settings;

public class JwtSettings
{
    public string JwtKey { get; set; } = null!;
    public string JwtIssuer { get; set; } = null!;
    public string JwtAudience { get; set; } = null!;
}