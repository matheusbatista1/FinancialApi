using FinancialApi.Application.Responses.Account;
using MediatR;

namespace FinancialApi.Application.Queries.Account;

public class GetAccountsQuery : IRequest<List<GetAccountsResponse>>
{
    public Guid PersonId { get; set; }
}