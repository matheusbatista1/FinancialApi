namespace FinancialApi.Application.Responses.Transaction.Children;

public class TransactionResponse
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public TransactionResponse(Domain.Entities.Transaction transaction)
    {
        Id = transaction.Id;
        Value = transaction.Value;
        Description = transaction.Description;
        CreatedAt = transaction.CreatedAt;
        UpdatedAt = transaction.UpdatedAt;
    }
}