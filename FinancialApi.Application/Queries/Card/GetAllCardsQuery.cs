using FinancialApi.Application.Responses.Card;
using MediatR;

namespace FinancialApi.Application.Queries.Card;

public class GetAllCardsQuery : PaginationQuery, IRequest<GetAllCardsResponse>
{
    public Guid PersonId { get; set; }
}