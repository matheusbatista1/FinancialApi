using FinancialApi.Application.Commands.Transaction;
using FinancialApi.Application.Responses.Transaction;
using FinancialApi.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinancialApi.Application.Handlers.Transaction;

public class RevertTransactionHandler : IRequestHandler<RevertTransactionCommand, RevertTransactionResponse>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<RevertTransactionHandler> _logger;

    public RevertTransactionHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, ILogger<RevertTransactionHandler> logger)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _logger = logger;
    }

    public async Task<RevertTransactionResponse> Handle(RevertTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(request.TransactionId);
        if (transaction == null || transaction.AccountId != request.AccountId)
        {
            _logger.LogWarning("Transação não encontrada ou não pertence à conta: {TransactionId}, {AccountId}", request.TransactionId, request.AccountId);
            throw new ArgumentException("Transação não encontrada ou não pertence à conta.");
        }

        if (transaction.IsReverted)
        {
            _logger.LogWarning("Tentativa de reverter uma transação já revertida: {TransactionId}", request.TransactionId);
            throw new InvalidOperationException("A transação já foi revertida.");
        }

        var account = await _accountRepository.GetByIdAsync(request.AccountId);
        if (account == null)
        {
            _logger.LogWarning("Conta não encontrada: {AccountId}", request.AccountId);
            throw new ArgumentException("Conta não encontrada.");
        }

        var reverseValue = -transaction.Value;
        if (account.Balance + reverseValue < 0)
        {
            _logger.LogWarning("Saldo insuficiente para reversão na conta: {AccountId}", request.AccountId);
            throw new InvalidOperationException("Saldo insuficiente para a reversão.");
        }

        account.Balance += reverseValue;
        transaction.IsReverted = true;

        var reverseTransaction = new Domain.Entities.Transaction
        {
            Id = Guid.NewGuid(),
            AccountId = request.AccountId,
            Value = reverseValue,
            Description = $"Estorno de: {transaction.Description}",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsReverted = false
        };

        await _transactionRepository.AddAsync(reverseTransaction);
        await _accountRepository.UpdateAsync(account);
        _logger.LogInformation("Transação revertida com sucesso: {Id}", reverseTransaction.Id);

        return new RevertTransactionResponse(reverseTransaction);
    }
}