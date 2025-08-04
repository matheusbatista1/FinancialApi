namespace FinancialApi.Application.Responses.Card.Children;

public class CardResponse
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Cvv { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public CardResponse(Domain.Entities.Card card)
    {
        Id = card.Id;
        Type = card.Type;
        Number = card.Number[^4..];
        Cvv = card.Cvv;
        CreatedAt = card.CreatedAt;
        UpdatedAt = card.UpdatedAt;
    }
}