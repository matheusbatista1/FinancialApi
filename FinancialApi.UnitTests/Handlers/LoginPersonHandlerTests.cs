using FinancialApi.Application.Commands.Auth;
using FinancialApi.Application.Handlers.Auth;
using FinancialApi.Domain.Interfaces.Repositories;
using FinancialApi.Domain.Interfaces.Services;
using FluentAssertions;
using Moq;

namespace FinancialApi.UnitTests.Handlers;

public class LoginPersonHandlerTests
{
    private readonly Mock<IPeopleRepository> _peopleRepositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly LoginPersonHandler _handler;

    public LoginPersonHandlerTests()
    {
        _peopleRepositoryMock = new Mock<IPeopleRepository>();
        _tokenServiceMock = new Mock<ITokenService>();
        _handler = new LoginPersonHandler(_peopleRepositoryMock.Object, _tokenServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCredentials_ReturnsToken()
    {
        var command = new LoginPersonCommand("56967915576", "senhaforte");
        var person = new Domain.Entities.People
        {
            Id = Guid.NewGuid(),
            Document = "56967915576",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("senhaforte")
        };
        _peopleRepositoryMock.Setup(r => r.GetByDocumentAsync("56967915576")).ReturnsAsync(person);
        _tokenServiceMock.Setup(t => t.GenerateToken(person.Id, person.Document)).Returns("jwt-token");

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Token.Should().Be("Bearer jwt-token");
    }

    [Fact]
    public async Task Handle_InvalidCredentials_ThrowsUnauthorizedAccessException()
    {
        var command = new LoginPersonCommand("56967915576", "wrongpassword");
        var person = new Domain.Entities.People
        {
            Id = Guid.NewGuid(),
            Document = "56967915576",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("senhaforte")
        };
        _peopleRepositoryMock.Setup(r => r.GetByDocumentAsync("56967915576")).ReturnsAsync(person);

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(command, CancellationToken.None));
    }
}