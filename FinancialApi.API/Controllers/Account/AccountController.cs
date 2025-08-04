using FinancialApi.Application.Commands.Account;
using FinancialApi.Application.Commands.Card;
using FinancialApi.Application.Commands.Transaction;
using FinancialApi.Application.Queries.Account;
using FinancialApi.Application.Queries.Balance;
using FinancialApi.Application.Queries.Card;
using FinancialApi.Application.Queries.Transaction;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinancialApi.API.Controllers.Account;

[ApiController]
[Route("api/accounts")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
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
    public async Task<IActionResult> GetAccounts()
    {
        var query = new GetAccountsQuery { PersonId = GetPersonId() };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{accountId}/cards")]
    public async Task<IActionResult> GetCardsByAccount(Guid accountId)
    {
        var query = new GetCardsByAccountQuery { AccountId = accountId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{accountId}/transactions")]
    public async Task<IActionResult> GetTransactions(Guid accountId, [FromQuery] int itemsPerPage = 10, [FromQuery] int currentPage = 1, [FromQuery] string? type = null)
    {
        var query = new GetTransactionsQuery
        {
            AccountId = accountId,
            ItemsPerPage = itemsPerPage,
            CurrentPage = currentPage,
            Type = type
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{accountId}/balance")]
    public async Task<IActionResult> GetBalance(Guid accountId)
    {
        var query = new GetBalanceQuery { AccountId = accountId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    #endregion

    #region POST

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
    {
        command.PersonId = GetPersonId();
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }

    [HttpPost("{accountId}/cards")]
    public async Task<IActionResult> CreateCard(Guid accountId, [FromBody] CreateCardCommand command)
    {
        command.AccountId = accountId;
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(CreateCard), new { accountId, id = result.Id }, result);
    }

    [HttpPost("{accountId}/transactions")]
    public async Task<IActionResult> CreateTransaction(Guid accountId, [FromBody] CreateTransactionCommand command)
    {
        command.AccountId = accountId;
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(CreateTransaction), new { accountId, id = result.Id }, result);
    }

    [HttpPost("{accountId}/transactions/internal")]
    public async Task<IActionResult> CreateInternalTransaction(Guid accountId, [FromBody] CreateInternalTransactionCommand command)
    {
        command.AccountId = accountId;
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(CreateInternalTransaction), new { accountId, id = result.Id }, result);
    }

    [HttpPost("{accountId}/transactions/{transactionId}/revert")]
    public async Task<IActionResult> RevertTransaction(Guid accountId, Guid transactionId)
    {
        var command = new RevertTransactionCommand { AccountId = accountId, TransactionId = transactionId };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    #endregion
}