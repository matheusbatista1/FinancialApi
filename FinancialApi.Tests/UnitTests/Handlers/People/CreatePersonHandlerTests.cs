using FinancialApi.Application.Commands.People;
using FinancialApi.Application.Handlers.People;
using FinancialApi.Domain.Interfaces.Repositories;
using FluentAssertions;
using Moq;

namespace FinancialApi.Tests.UnitTests.Handlers.People;

public class CreatePersonHandlerTests
{
    private readonly Mock<IPeopleRepository> _peopleRepositoryMock;
    private readonly CreatePersonHandler _handler;

    public CreatePersonHandlerTests()
    {
        _peopleRepositoryMock = new Mock<IPeopleRepository>();
        _handler = new CreatePersonHandler(_peopleRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_CreatesPersonSuccessfully()
    {
        var command = new CreatePersonCommand("Carolina Rosa Marina Barros", "569.679.155-76", "senhaforte");
        _peopleRepositoryMock.Setup(r => r.ExistsByDocumentAsync("56967915576")).ReturnsAsync(false);
        _peopleRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Domain.Entities.People>())).Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);
        result.Document.Should().Be("56967915576");
        result.Id.Should().NotBeEmpty();
        result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        _peopleRepositoryMock.Verify(r => r.AddAsync(It.Is<Domain.Entities.People>(p => p.PasswordHash != command.Password)), Times.Once());
    }

    [Fact]
    public async Task Handle_DuplicateDocument_ThrowsInvalidOperationException()
    {
        var command = new CreatePersonCommand("Carolina Rosa Marina Barros", "569.679.155-76", "senhaforte");
        _peopleRepositoryMock.Setup(r => r.ExistsByDocumentAsync("56967915576")).ReturnsAsync(true);

        var act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<Exception>().WithMessage("Documento já cadastrado.");
    }
}