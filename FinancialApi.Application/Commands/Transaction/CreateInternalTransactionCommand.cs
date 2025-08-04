using FinancialApi.Application.Responses.Transaction;
using MediatR;
using System.Text.Json.Serialization;

namespace FinancialApi.Application.Commands.Transaction;

public class CreateInternalTransactionCommand : IRequest<CreateInternalTransactionResponse>
{
    [JsonIgnore]
    public Guid AccountId { get; set; }
    public Guid ReceiverAccountId { get; set; }
    public decimal Value { get; set; }
    public string Description { get; set; } = string.Empty;
}