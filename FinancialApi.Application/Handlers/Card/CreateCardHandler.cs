using FinancialApi.Application.Commands.Card;
using FinancialApi.Application.Responses.Card;
using FinancialApi.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinancialApi.Application.Handlers.Card;

public class CreateCardHandler : IRequestHandler<CreateCardCommand, CreateCardResponse>
{
    private readonly ICardRepository _cardRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<CreateCardHandler> _logger;

    public CreateCardHandler(ICardRepository cardRepository, IAccountRepository accountRepository, ILogger<CreateCardHandler> logger)
    {
        _cardRepository = cardRepository;
        _accountRepository = accountRepository;
        _logger = logger;
    }

    public async Task<CreateCardResponse> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(request.AccountId);
        if (account == null)
        {
            _logger.LogWarning("Conta não encontrada: {AccountId}", request.AccountId);
            throw new ArgumentException("Conta não encontrada.");
        }

        if (request.Type == "physical" && await _cardRepository.ExistsPhysicalCardAsync(request.AccountId))
        {
            _logger.LogWarning("Tentativa de criar um segundo cartão físico para a conta: {AccountId}", request.AccountId);
            throw new InvalidOperationException("Já existe um cartão físico para esta conta.");
        }

        var card = new Domain.Entities.Card
        {
            Id = Guid.NewGuid(),
            AccountId = request.AccountId,
            Type = request.Type,
            Number = request.Number,
            Cvv = request.Cvv,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _cardRepository.AddAsync(card);
        _logger.LogInformation("Cartão criado com sucesso: {Id}", card.Id);

        return new CreateCardResponse(card);
    }
}