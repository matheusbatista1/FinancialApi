namespace FinancialApi.Domain.ExternalServices.Compliance.Models;

public class AuthCodeRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}