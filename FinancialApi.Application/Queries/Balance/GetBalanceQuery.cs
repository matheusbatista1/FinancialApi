using FinancialApi.Application.Responses.Balance;
using MediatR;

namespace FinancialApi.Application.Queries.Balance;

public class GetBalanceQuery : IRequest<GetBalanceResponse>
{
    public Guid AccountId { get; set; }
}