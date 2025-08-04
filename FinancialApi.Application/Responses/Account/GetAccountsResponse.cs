namespace FinancialApi.Application.Responses.Account;

public class GetAccountsResponse
{
    public Guid Id { get; set; }
    public string Branch { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public GetAccountsResponse(Domain.Entities.Account account)
    {
        Id = account.Id;
        Branch = account.Branch;
        Account = account.AccountNumber;
        CreatedAt = account.CreatedAt;
        UpdatedAt = account.UpdatedAt;
    }
}