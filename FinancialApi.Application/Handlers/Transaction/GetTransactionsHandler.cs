using FinancialApi.Application.Queries.Transaction;
using FinancialApi.Application.Responses.Transaction.Children;
using FinancialApi.Application.Responses.Transaction;
using FinancialApi.Application.Responses;
using FinancialApi.Domain.Interfaces.Repositories;
using MediatR;

namespace FinancialApi.Application.Handlers.Transaction;

public class GetTransactionsHandler : IRequestHandler<GetTransactionsQuery, GetTransactionsResponse>
{
    private readonly ITransactionRepository _repository;

    public GetTransactionsHandler(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetTransactionsResponse> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _repository.GetByAccountIdAsync(request.AccountId, request.ItemsPerPage, request.CurrentPage, request.Type);
        var totalItems = await _repository.CountTransactionsAsync(request.AccountId, request.Type);

        return new GetTransactionsResponse
        {
            Transactions = transactions.Select(t => new TransactionResponse(t)).ToList(),
            Pagination = new PaginationResponse
            {
                ItemsPerPage = request.ItemsPerPage,
                CurrentPage = request.CurrentPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / request.ItemsPerPage)
            }
        };
    }
}