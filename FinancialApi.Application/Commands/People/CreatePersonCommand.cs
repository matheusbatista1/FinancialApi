using FinancialApi.Application.Responses.People;
using MediatR;

namespace FinancialApi.Application.Commands.People;

public record CreatePersonCommand(string Name, string Document, string Password) : IRequest<CreatePersonResponse>;