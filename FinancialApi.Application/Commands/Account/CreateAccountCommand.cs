using FinancialApi.Application.Responses.Account;
using MediatR;
using System.Text.Json.Serialization;

namespace FinancialApi.Application.Commands.Account;

public class CreateAccountCommand : IRequest<CreateAccountResponse>
{
    [JsonIgnore]
    public Guid PersonId { get; set; }
    public string Branch { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
}