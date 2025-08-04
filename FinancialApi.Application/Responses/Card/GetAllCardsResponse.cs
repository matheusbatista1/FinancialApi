using FinancialApi.Application.Responses.Card.Children;

namespace FinancialApi.Application.Responses.Card;

public class GetAllCardsResponse
{
    public List<CardResponse> Cards { get; set; } = new();
    public PaginationResponse Pagination { get; set; } = null!;
}