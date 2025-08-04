using FinancialApi.Application.Queries.Card;
using FinancialApi.Application.Responses.Card;
using FinancialApi.Domain.Interfaces.Repositories;
using MediatR;

namespace FinancialApi.Application.Handlers.Card;

public class GetCardsByAccountHandler : IRequestHandler<GetCardsByAccountQuery, List<GetCardsResponse>>
{
    private readonly ICardRepository _repository;

    public GetCardsByAccountHandler(ICardRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetCardsResponse>> Handle(GetCardsByAccountQuery request, CancellationToken cancellationToken)
    {
        var cards = await _repository.GetByAccountIdAsync(request.AccountId);
        return cards.Select(c => new GetCardsResponse(c)).ToList();
    }
}