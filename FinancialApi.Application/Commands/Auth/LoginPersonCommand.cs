using FinancialApi.Application.Responses.Auth;
using MediatR;

namespace FinancialApi.Application.Commands.Auth;

public record LoginPersonCommand(string Document, string Password) : IRequest<LoginPersonResponse>;