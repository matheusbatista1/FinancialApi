namespace FinancialApi.Domain.ExternalServices.Compliance.Models;

public class AuthCodeResponse
{
    public bool Success { get; set; }
    public AuthCodeData Data { get; set; } = null!;

    public string UserId => Data.UserId;
    public string AuthCode => Data.AuthCode;
}

public class AuthCodeData
{
    public string UserId { get; set; } = null!;
    public string AuthCode { get; set; } = null!;
}