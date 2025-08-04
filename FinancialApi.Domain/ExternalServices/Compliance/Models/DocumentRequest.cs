namespace FinancialApi.Domain.ExternalServices.Compliance.Models;

public class DocumentRequest
{
    public DocumentRequest(string doc)
    {
        Document = doc;
    }

    public string Document { get; init; }
}