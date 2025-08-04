namespace FinancialApi.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(Guid userId, string document);
}