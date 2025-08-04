namespace FinancialApi.Domain.ExternalServices.Compliance.Models;

public class DocumentResponse
{
    public bool Success { get; set; }
    public DocumentData Data { get; set; } = null!;
}

public class DocumentData
{
    public string Document { get; set; } = null!;
    public int Status { get; set; }
    public string Reason { get; set; } = null!;
}