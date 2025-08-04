using FinancialApi.Application.Responses.Transaction.Children;

namespace FinancialApi.Application.Responses.Transaction;

public class GetTransactionsResponse
{
    public List<TransactionResponse> Transactions { get; set; } = new();
    public PaginationResponse Pagination { get; set; } = null!;
}