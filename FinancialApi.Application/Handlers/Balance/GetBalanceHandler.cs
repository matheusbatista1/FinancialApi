using FinancialApi.Application.Queries.Balance;
using FinancialApi.Application.Responses.Balance;
using FinancialApi.Domain.Interfaces.Repositories;
using MediatR;

namespace FinancialApi.Application.Handlers.Balance;

public class GetBalanceHandler : IRequestHandler<GetBalanceQuery, GetBalanceResponse>
{
    private readonly IAccountRepository _repository;

    public GetBalanceHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetBalanceResponse> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
    {
        var balance = await _repository.GetBalanceAsync(request.AccountId);
        return new GetBalanceResponse(balance);
    }
}