using FinancialApi.Application.Commands.Account;
using FinancialApi.Application.Handlers.Account;
using FinancialApi.Domain.Interfaces.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace FinancialApi.UnitTests.Handlers;

public class CreateAccountHandlerTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly Mock<IPeopleRepository> _peopleRepositoryMock;
    private readonly Mock<ILogger<CreateAccountHandler>> _loggerMock;
    private readonly CreateAccountHandler _handler;

    public CreateAccountHandlerTests()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _peopleRepositoryMock = new Mock<IPeopleRepository>();
        _loggerMock = new Mock<ILogger<CreateAccountHandler>>();
        _handler = new CreateAccountHandler(_accountRepositoryMock.Object, _peopleRepositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreatesAccountSuccessfully()
    {
        var command = new CreateAccountCommand
        {
            Branch = "001",
            Account = "2033392-5",
            PersonId = Guid.NewGuid()
        };
        _peopleRepositoryMock.Setup(r => r.GetByIdAsync(command.PersonId)).ReturnsAsync(new Domain.Entities.People());
        _accountRepositoryMock.Setup(r => r.ExistsByAccountNumberAsync("2033392-5")).ReturnsAsync(false);
        _accountRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Domain.Entities.Account>())).Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Branch.Should().Be(command.Branch);
        result.Account.Should().Be(command.Account);
        result.Id.Should().NotBeEmpty();
        result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task Handle_DuplicateAccountNumber_ThrowsInvalidOperationException()
    {
        var command = new CreateAccountCommand
        {
            Branch = "001",
            Account = "2033392-5",
            PersonId = Guid.NewGuid()
        };
        _peopleRepositoryMock.Setup(r => r.GetByIdAsync(command.PersonId)).ReturnsAsync(new Domain.Entities.People());
        _accountRepositoryMock.Setup(r => r.ExistsByAccountNumberAsync("2033392-5")).ReturnsAsync(true);

        await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
    }
}