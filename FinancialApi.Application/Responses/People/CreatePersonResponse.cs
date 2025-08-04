namespace FinancialApi.Application.Responses.People;

public class CreatePersonResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public CreatePersonResponse(Domain.Entities.People person)
    {
        Id = person.Id;
        Name = person.Name;
        Document = person.Document;
        CreatedAt = person.CreatedAt;
        UpdatedAt = person.UpdatedAt;
    }
}