namespace FinancialApi.Application.Responses.Card;

public class GetCardsResponse
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Cvv { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public GetCardsResponse(Domain.Entities.Card card)
    {
        Id = card.Id;
        Type = card.Type;
        Number = card.Number;
        Cvv = card.Cvv;
        CreatedAt = card.CreatedAt;
        UpdatedAt = card.UpdatedAt;
    }
}