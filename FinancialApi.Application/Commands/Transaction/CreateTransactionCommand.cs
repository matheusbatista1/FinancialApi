using FinancialApi.Application.Responses.Transaction;
using MediatR;
using System.Text.Json.Serialization;

namespace FinancialApi.Application.Commands.Transaction;

public class CreateTransactionCommand : IRequest<CreateTransactionResponse>
{
    [JsonIgnore]
    public Guid AccountId { get; set; }
    public decimal Value { get; set; }
    public string Description { get; set; } = string.Empty;
}