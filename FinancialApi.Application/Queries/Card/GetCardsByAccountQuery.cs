using FinancialApi.Application.Responses.Card;
using MediatR;

namespace FinancialApi.Application.Queries.Card;

public class GetCardsByAccountQuery : IRequest<List<GetCardsResponse>>
{
    public Guid AccountId { get; set; }
}