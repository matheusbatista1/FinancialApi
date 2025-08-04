using FinancialApi.Application.Commands.People;
using FinancialApi.Application.Responses.People;
using FinancialApi.Domain.Interfaces.Repositories;
using MediatR;
using System.Text.RegularExpressions;

namespace FinancialApi.Application.Handlers.People;

public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, CreatePersonResponse>
{
    private readonly IPeopleRepository _repository;

    public CreatePersonHandler(IPeopleRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreatePersonResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var cleanDoc = Regex.Replace(request.Document, "[^0-9]", "");

        if (await _repository.ExistsByDocumentAsync(cleanDoc))
            throw new Exception("Documento já cadastrado.");

        var person = new Domain.Entities.People
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Document = cleanDoc,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        await _repository.AddAsync(person);

        return new CreatePersonResponse(person);
    }
}