using FinancialApi.Application.Commands.Account;
using FinancialApi.Application.Responses.Account;
using FinancialApi.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinancialApi.Application.Handlers.Account;

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, CreateAccountResponse>
{
    private readonly IAccountRepository _repository;
    private readonly IPeopleRepository _peopleRepository;
    private readonly ILogger<CreateAccountHandler> _logger;

    public CreateAccountHandler(IAccountRepository repository, IPeopleRepository peopleRepository, ILogger<CreateAccountHandler> logger)
    {
        _repository = repository;
        _peopleRepository = peopleRepository;
        _logger = logger;
    }

    public async Task<CreateAccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var person = await _peopleRepository.GetByIdAsync(request.PersonId);
        if (person == null)
        {
            _logger.LogWarning("Pessoa não encontrada: {PersonId}", request.PersonId);
            throw new ArgumentException("Pessoa não encontrada.");
        }

        if (await _repository.ExistsByAccountNumberAsync(request.Account))
        {
            _logger.LogWarning("Tentativa de cadastro com número de conta já existente: {AccountNumber}", request.Account);
            throw new InvalidOperationException("Número da conta já cadastrado.");
        }

        var account = new Domain.Entities.Account
        {
            Id = Guid.NewGuid(),
            PersonId = request.PersonId,
            Branch = request.Branch,
            AccountNumber = request.Account,
            Balance = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(account);
        _logger.LogInformation("Conta criada com sucesso: {Id}", account.Id);

        return new CreateAccountResponse(account);
    }
}