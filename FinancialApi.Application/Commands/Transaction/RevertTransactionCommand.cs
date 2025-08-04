using FinancialApi.Application.Responses.Transaction;
using MediatR;

namespace FinancialApi.Application.Commands.Transaction;

public class RevertTransactionCommand : IRequest<RevertTransactionResponse>
{
    public Guid AccountId { get; set; }
    public Guid TransactionId { get; set; }
}