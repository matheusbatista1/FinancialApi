using FinancialApi.Application.Queries.Card;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinancialApi.API.Controllers.Card;

[ApiController]
[Route("api/cards")]
[Authorize]
public class CardController : ControllerBase
{
    private readonly IMediator _mediator;

    public CardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private Guid GetPersonId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(userId, out var id) ? id : throw new UnauthorizedAccessException("Usuário não autenticado.");
    }

    #region GET

    [HttpGet]
    public async Task<IActionResult> GetAllCards([FromQuery] int itemsPerPage = 10, [FromQuery] int currentPage = 1)
    {
        var query = new GetAllCardsQuery
        {
            PersonId = GetPersonId(),
            ItemsPerPage = itemsPerPage,
            CurrentPage = currentPage
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    #endregion
}