using FinancialApi.Application.Queries.Account;
using FinancialApi.Application.Responses.Account;
using FinancialApi.Domain.Interfaces.Repositories;
using MediatR;

namespace FinancialApi.Application.Handlers.Account;

public class GetAccountsHandler : IRequestHandler<GetAccountsQuery, List<GetAccountsResponse>>
{
    private readonly IAccountRepository _repository;

    public GetAccountsHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetAccountsResponse>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _repository.GetByPersonIdAsync(request.PersonId);
        return accounts.Select(a => new GetAccountsResponse(a)).ToList();
    }
}