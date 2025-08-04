using FinancialApi.Application.Responses.Transaction;
using MediatR;

namespace FinancialApi.Application.Queries.Transaction;

public class GetTransactionsQuery : PaginationQuery, IRequest<GetTransactionsResponse>
{
    public Guid AccountId { get; set; }
    public string? Type { get; set; }
}