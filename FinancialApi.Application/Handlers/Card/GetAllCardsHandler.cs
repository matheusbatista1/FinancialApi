using FinancialApi.Application.Queries.Card;
using FinancialApi.Application.Responses.Card.Children;
using FinancialApi.Application.Responses.Card;
using FinancialApi.Application.Responses;
using FinancialApi.Domain.Interfaces.Repositories;
using MediatR;

namespace FinancialApi.Application.Handlers.Card;

public class GetAllCardsHandler : IRequestHandler<GetAllCardsQuery, GetAllCardsResponse>
{
    private readonly ICardRepository _repository;

    public GetAllCardsHandler(ICardRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetAllCardsResponse> Handle(GetAllCardsQuery request, CancellationToken cancellationToken)
    {
        var cards = await _repository.GetByPersonIdAsync(request.PersonId, request.ItemsPerPage, request.CurrentPage);
        var totalItems = await _repository.CountCardsAsync(request.PersonId);

        return new GetAllCardsResponse
        {
            Cards = cards.Select(c => new CardResponse(c)).ToList(),
            Pagination = new PaginationResponse
            {
                ItemsPerPage = request.ItemsPerPage,
                CurrentPage = request.CurrentPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / request.ItemsPerPage)
            }
        };
    }
}