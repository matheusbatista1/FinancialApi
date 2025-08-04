using FinancialApi.Application.Responses.Card;
using MediatR;
using System.Text.Json.Serialization;

namespace FinancialApi.Application.Commands.Card;

public class CreateCardCommand : IRequest<CreateCardResponse>
{
    [JsonIgnore]
    public Guid AccountId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Cvv { get; set; } = string.Empty;
}