using FinancialApi.Application.Commands.Transaction;
using FinancialApi.Application.Responses.Transaction;
using FinancialApi.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinancialApi.Application.Handlers.Transaction;

public class CreateInternalTransactionHandler : IRequestHandler<CreateInternalTransactionCommand, CreateInternalTransactionResponse>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<CreateInternalTransactionHandler> _logger;

    public CreateInternalTransactionHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository, ILogger<CreateInternalTransactionHandler> logger)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _logger = logger;
    }

    public async Task<CreateInternalTransactionResponse> Handle(CreateInternalTransactionCommand request, CancellationToken cancellationToken)
    {
        var senderAccount = await _accountRepository.GetByIdAsync(request.AccountId);
        var receiverAccount = await _accountRepository.GetByIdAsync(request.ReceiverAccountId);

        if (senderAccount == null || receiverAccount == null)
        {
            _logger.LogWarning("Conta de origem ou destino não encontrada: Origem={AccountId}, Destino={ReceiverAccountId}", request.AccountId, request.ReceiverAccountId);
            throw new ArgumentException("Conta de origem ou destino não encontrada.");
        }

        var balance = await _accountRepository.GetBalanceAsync(request.AccountId);
        if (balance < request.Value)
        {
            _logger.LogWarning("Saldo insuficiente para transferência na conta: {AccountId}", request.AccountId);
            throw new InvalidOperationException("Saldo insuficiente para a transferência.");
        }

        senderAccount.Balance -= request.Value;
        receiverAccount.Balance += request.Value;

        var senderTransaction = new Domain.Entities.Transaction
        {
            Id = Guid.NewGuid(),
            AccountId = request.AccountId,
            Value = -request.Value,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsReverted = false
        };

        var receiverTransaction = new Domain.Entities.Transaction
        {
            Id = Guid.NewGuid(),
            AccountId = request.ReceiverAccountId,
            Value = request.Value,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsReverted = false
        };

        await _transactionRepository.AddAsync(senderTransaction);
        await _transactionRepository.AddAsync(receiverTransaction);
        await _accountRepository.UpdateAsync(senderAccount);
        await _accountRepository.UpdateAsync(receiverAccount);
        _logger.LogInformation("Transferência interna criada com sucesso: {Id}", senderTransaction.Id);

        return new CreateInternalTransactionResponse(senderTransaction);
    }
}