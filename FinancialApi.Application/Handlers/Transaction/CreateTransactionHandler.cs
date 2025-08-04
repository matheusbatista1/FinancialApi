using FinancialApi.Application.Commands.Transaction;
using FinancialApi.Application.Responses.Transaction;
using FinancialApi.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinancialApi.Application.Handlers.Transaction;

public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, CreateTransactionResponse>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<CreateTransactionHandler> _logger;

    public CreateTransactionHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, ILogger<CreateTransactionHandler> logger)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _logger = logger;
    }

    public async Task<CreateTransactionResponse> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(request.AccountId);
        if (account == null)
        {
            _logger.LogWarning("Conta não encontrada: {AccountId}", request.AccountId);
            throw new ArgumentException("Conta não encontrada.");
        }

        if (request.Value < 0)
        {
            var balance = await _accountRepository.GetBalanceAsync(request.AccountId);
            if (balance + request.Value < 0)
            {
                _logger.LogWarning("Saldo insuficiente para transação de débito na conta: {AccountId}", request.AccountId);
                throw new InvalidOperationException("Saldo insuficiente para a transação.");
            }
            account.Balance += request.Value;
        }
        else
        {
            account.Balance += request.Value;
        }

        var transaction = new Domain.Entities.Transaction
        {
            Id = Guid.NewGuid(),
            AccountId = request.AccountId,
            Value = request.Value,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsReverted = false
        };

        await _transactionRepository.AddAsync(transaction);
        await _accountRepository.UpdateAsync(account); 
        _logger.LogInformation("Transação criada com sucesso: {Id}", transaction.Id);

        return new CreateTransactionResponse(transaction);
    }
}