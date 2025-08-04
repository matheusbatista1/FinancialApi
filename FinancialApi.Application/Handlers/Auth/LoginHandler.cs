using FinancialApi.Application.Commands.Auth;
using FinancialApi.Application.Responses.Auth;
using FinancialApi.Domain.Interfaces.Repositories;
using FinancialApi.Domain.Interfaces.Services;
using MediatR;
using System.Text.RegularExpressions;

namespace FinancialApi.Application.Handlers.Auth;

public class LoginPersonHandler : IRequestHandler<LoginPersonCommand, LoginPersonResponse>
{
    private readonly IPeopleRepository _repo;
    private readonly ITokenService _tokenService;

    public LoginPersonHandler(IPeopleRepository repo, ITokenService tokenService)
    {
        _repo = repo;
        _tokenService = tokenService;
    }

    public async Task<LoginPersonResponse> Handle(LoginPersonCommand request, CancellationToken cancellationToken)
    {
        var cleanDoc = Regex.Replace(request.Document, "[^0-9]", "");
        var person = await _repo.GetByDocumentAsync(cleanDoc);

        if (person == null || !BCrypt.Net.BCrypt.Verify(request.Password, person.PasswordHash))
            throw new UnauthorizedAccessException("Credenciais inválidas.");

        var token = _tokenService.GenerateToken(person.Id, person.Document);
        return new LoginPersonResponse($"Bearer {token}");
    }
}