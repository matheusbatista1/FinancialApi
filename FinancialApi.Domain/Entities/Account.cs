namespace FinancialApi.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public string Branch { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public People Person { get; set; } = null!;
    public List<Card> Cards { get; set; } = new();
    public List<Transaction> Transactions { get; set; } = new();
}